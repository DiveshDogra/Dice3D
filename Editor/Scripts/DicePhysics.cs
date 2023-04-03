
namespace Dice3D.Physics
{
    using UnityEngine;
    using System.Collections.Generic;
    using Dice3D.Variables;

    public class DicePhysics : MonoBehaviour
    {

        private Vector3 _velocity;
        private Vector3 angleForce;
        private float _drag;

        private Quaternion _startRotation;
        private Vector3 _startPos;

        [SerializeField]
        private int roll;

        [SerializeField]
        private List<GameObject> child;

        private Rigidbody _rigidbody;

        [SerializeField]
        private DiceRotationValues _diceData;

        private int PreRollValue;

        private void OnEnable()
        {
            DiceEventManager.DiceThrowEvent += OnDiceThrow;
        }
        private void Start()
        {
            for (int i = 0; i <= transform.childCount - 1; i++)
            {
                child.Add(transform.GetChild(i).gameObject);
            }
            _startRotation = transform.rotation;
            _startPos = transform.position;

            Physics.autoSimulation = false;
            _rigidbody = GetComponent<Rigidbody>();
        }


        public void OnDiceThrow(int rollValue)
        {

            roll = rollValue;
            ResetDice();
            Physics.autoSimulation = false;
            SimlulateDice();
        }

        private void RandomThrowForce()
        {
            SelectRandomForce();

            _rigidbody.velocity = _velocity;
            _rigidbody.angularDrag = _drag;
            _rigidbody.angularVelocity = angleForce;
        }

        private void SelectRandomForce()
        {
            int randomIndex = Random.Range(0, 9);
            if (randomIndex >= (int)PhysicsVariable.OneBounceFlip.probablity)
            {
                MakeOneBoucneFlip();
            }
            else
            {
                MakeOneBounceNoFlip();
            }
        }

        private void MakeOneBounceNoFlip()
        {
            _velocity = new Vector3(0, (float)PhysicsVariable.OneBounceNoFlip.velocity, 0);
            _drag = (float)PhysicsVariable.OneBounceNoFlip.angularDrag;
            angleForce.x = Random.Range((float)PhysicsVariable.OneBounceNoFlip.angleForceMin,
                                                    (float)PhysicsVariable.OneBounceNoFlip.angleForceMax);
            angleForce.y = Random.Range((float)PhysicsVariable.OneBounceNoFlip.angleForceMin,
                                                    (float)PhysicsVariable.OneBounceNoFlip.angleForceMax);
            angleForce.z = Random.Range((float)PhysicsVariable.OneBounceNoFlip.angleForceMin,
                                                    (float)PhysicsVariable.OneBounceNoFlip.angleForceMax);
        }

        private void MakeOneBoucneFlip()
        {
            _velocity = new Vector3(0, (float)PhysicsVariable.OneBounceFlip.velocity, 0);
            _drag = (float)PhysicsVariable.OneBounceFlip.angularDrag;
            angleForce.x = Random.Range((float)PhysicsVariable.OneBounceFlip.angleForceMin,
                                                    (float)PhysicsVariable.OneBounceFlip.angleForceMax);
            angleForce.y = Random.Range((float)PhysicsVariable.OneBounceFlip.angleForceMin,
                                                    (float)PhysicsVariable.OneBounceFlip.angleForceMax);
            angleForce.z = Random.Range((float)PhysicsVariable.OneBounceFlip.angleForceMin,
                                                    (float)PhysicsVariable.OneBounceFlip.angleForceMax);
        }

        private void SimlulateDice()
        {
            RandomThrowForce();
            for (int i = 0; i < 500; i++)
            {
                Physics.Simulate(Time.fixedDeltaTime);
            }

            CheckDiceFace();

            ResetDice();

            ChangeIntialRotation();

            ThrowDice();
        }

        private void CheckDiceFace()
        {
            foreach (GameObject thischild in child)
            {
                if (thischild.transform.position.y > 1.5)
                {
                    PreRollValue = int.Parse(thischild.name);
                    Debug.Log(PreRollValue);
                }
            }
        }
        private void ResetDice()
        {
            _rigidbody.velocity = Vector3.zero;
            transform.position = _startPos;
            transform.rotation = _startRotation;
        }

        private void ChangeIntialRotation()
        {
            transform.rotation = _diceData.faceRelativeRotation[roll].rotation[PreRollValue];
        }

        private void ThrowDice()
        {
            Physics.autoSimulation = true;
            _rigidbody.velocity = _velocity;
            _rigidbody.angularDrag = _drag;
            _rigidbody.angularVelocity = angleForce;
        }
    }
}