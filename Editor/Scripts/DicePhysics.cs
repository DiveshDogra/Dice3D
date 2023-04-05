
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

        private void OnDisable()
        {
            DiceEventManager.DiceThrowEvent -= OnDiceThrow;
        }
        private void Start()
        {
            /*for (int i = DiceConstVariable.VAL_ZERO; i <= transform.childCount - 1; i++)
            {
                child.Add(transform.GetChild(i).gameObject);
            }*/
            _startRotation = transform.rotation;
            _startPos = transform.position;

            Physics.autoSimulation = false;
            _rigidbody = GetComponent<Rigidbody>();
            _physMaterial = GetComponent<BoxCollider>().material;
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
           int randomIndex = Random.Range(DiceConstVariable.VAL_ZERO, DiceConstVariable.VAL_TEN);
           if(randomIndex <= _diceThrowForce._DiceForceList[DiceConstVariable.VAL_ZERO].probability)
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
            var _forceIndex = DiceConstVariable.VAL_ZERO;
            Debug.Log("Make One Bouce and No Flip");
            _velocity = _diceThrowForce._DiceForceList[_forceIndex].velocity;
            _drag = _diceThrowForce._DiceForceList[_forceIndex].angularDrag;
            angleForce.x = Random.Range(_diceThrowForce._DiceForceList[_forceIndex].angleForceMin.x,
                                                    _diceThrowForce._DiceForceList[_forceIndex].angleForceMax.x);
            angleForce.y = Random.Range(_diceThrowForce._DiceForceList[_forceIndex].angleForceMin.y,
                                                    _diceThrowForce._DiceForceList[_forceIndex].angleForceMax.y);
            angleForce.z = Random.Range(_diceThrowForce._DiceForceList[_forceIndex].angleForceMin.z,
                                                    _diceThrowForce._DiceForceList[_forceIndex].angleForceMax.z);
            _mass = _diceThrowForce._DiceForceList[_forceIndex].mass;
            _friction = _diceThrowForce._DiceForceList[_forceIndex].friction;
        }

        private void MakeOneBoucneFlip()
        {
            Debug.Log("Make One Bouce and Flip");
            var _forceIndex = DiceConstVariable.VAL_ONE;
            _velocity = _diceThrowForce._DiceForceList[_forceIndex].velocity;
            _drag = _diceThrowForce._DiceForceList[_forceIndex].angularDrag;
            angleForce.x = Random.Range(_diceThrowForce._DiceForceList[_forceIndex].angleForceMin.x,
                                                    _diceThrowForce._DiceForceList[_forceIndex].angleForceMax.x);
            angleForce.y = Random.Range(_diceThrowForce._DiceForceList[_forceIndex].angleForceMin.y,
                                                    _diceThrowForce._DiceForceList[_forceIndex].angleForceMax.y);
            angleForce.z = Random.Range(_diceThrowForce._DiceForceList[_forceIndex].angleForceMin.z,
                                                    _diceThrowForce._DiceForceList[_forceIndex].angleForceMax.z);
            _mass = _diceThrowForce._DiceForceList[_forceIndex].mass;
            _friction = _diceThrowForce._DiceForceList[_forceIndex].friction;

        }

        private void SimlulateDice()
        {
            RandomThrowForce();
            for (int i = DiceConstVariable.VAL_ZERO ; i < DiceConstVariable.DICE_SIM_LENGTH; i++)
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
                if (thischild.transform.position.y > transform.localScale.x * DiceConstVariable.VAL_TEN/DiceConstVariable.VAL_HUNDRED)
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