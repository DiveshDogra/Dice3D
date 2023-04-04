
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
        private float _mass;
        private float _friction;

        private Quaternion _startRotation;
        private Vector3 _startPos;

        [SerializeField]
        private int roll;

        [SerializeField]
        private List<GameObject> child;

        private Rigidbody _rigidbody;
        private PhysicMaterial _physMaterial;

        [SerializeField]
        private DiceRotationValues _diceData;

        [SerializeField]
        private ForceValuesSO _diceThrowForce;

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
            _physMaterial = GetComponent<MeshCollider>().material;
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
            _rigidbody.mass = _mass;
            _physMaterial.dynamicFriction = _friction;
        }

        private void SelectRandomForce()
        {
            float randomIndex = Random.Range(0, 1);
           if(randomIndex <= _diceThrowForce._DiceForceList[0].probability)
           {
              MakeOneBounceNoFlip();
           }
           else
           {
              MakeOneBoucneFlip();
           }
        }

        private void MakeOneBounceNoFlip()
        {
            Debug.Log("Make One Bouce and No Flip");
            _velocity = _diceThrowForce._DiceForceList[0].velocity;
            _drag = _diceThrowForce._DiceForceList[0].angularDrag;
            angleForce.x = Random.Range(_diceThrowForce._DiceForceList[0].angleForceMin.x,
                                                    _diceThrowForce._DiceForceList[0].angleForceMax.x);
            angleForce.y = Random.Range(_diceThrowForce._DiceForceList[0].angleForceMin.y,
                                                    _diceThrowForce._DiceForceList[0].angleForceMax.y);
            angleForce.z = Random.Range(_diceThrowForce._DiceForceList[0].angleForceMin.z,
                                                    _diceThrowForce._DiceForceList[0].angleForceMax.z);
            _mass = _diceThrowForce._DiceForceList[0].mass;
            _friction = _diceThrowForce._DiceForceList[0].friction;
        }

        private void MakeOneBoucneFlip()
        {
            Debug.Log("Make One Bouce and Flip");
            _velocity = _diceThrowForce._DiceForceList[1].velocity;
            _drag = _diceThrowForce._DiceForceList[1].angularDrag;
            angleForce.x = Random.Range(_diceThrowForce._DiceForceList[1].angleForceMin.x,
                                                    _diceThrowForce._DiceForceList[1].angleForceMax.x);
            angleForce.y = Random.Range(_diceThrowForce._DiceForceList[1].angleForceMin.y,
                                                    _diceThrowForce._DiceForceList[1].angleForceMax.y);
            angleForce.z = Random.Range(_diceThrowForce._DiceForceList[1].angleForceMin.z,
                                                    _diceThrowForce._DiceForceList[1].angleForceMax.z);
            _mass = _diceThrowForce._DiceForceList[1].mass;
            _friction = _diceThrowForce._DiceForceList[1].friction;

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
                if (thischild.transform.position.y > 0.15f)
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
            _rigidbody.mass = _mass;
            _physMaterial.dynamicFriction = _friction;
        }
    }
}