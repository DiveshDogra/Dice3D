
namespace Dice3D.Physics
{
    using UnityEngine;
    using System.Collections.Generic;
    using System.Collections;
    using Dice3D.Variables;

    //@Divesh - Controls the physics simulation of dice
    public class DicePhysics : MonoBehaviour
    {
        [Header("Variables to control dice physics")]
        private Vector3 _velocity;
        private Vector3 angleForce;
        private float _drag;
        private float _mass;
        private float _friction;
        private bool _isDiceStopped;

        [Header("Variables to keep starting position of dice")]
        private Quaternion _startRotation;
        private Vector3 _startPos;
        private Vector3 _startSize;

        [Header("Value that is needed for dice")]
        [SerializeField]
        private int roll;

        [Header("All child objects which are used to detect the face of dice")]
        [SerializeField]
        private List<GameObject> child;

        [Header("Chached Rigidbody and physics material of dice")]
        private Rigidbody _rigidbody;
        private PhysicMaterial _physMaterial;

        [Header("SO which contains dice's rotation value when changing dice's face")]
        [SerializeField]
        private DiceRotationValues _diceData;

        [Header("List of Force values which will/can be applied on dice")]
        [SerializeField]
        private ForceValuesSO _diceThrowForce;

        //@Divesh - Inital value which was rolled
        private int PreRollValue;
        //@Divesh - Checks if dice was rolled in Simlate or realTime
        public bool _isInSimulation = true;

        //@Divesh - Subscribe to EventManger the dice throw function
        private void OnEnable()
        {
            DiceEventManager.DiceThrowEvent += OnDiceThrow;
        }

        //@Divesh - UnSubscribe to EventManger the dice throw function
        private void OnDisable()
        {
            DiceEventManager.DiceThrowEvent -= OnDiceThrow;
        }

        //@Divesh - Get Dice's start position and chache rigidbody and physics material
        private void Start()
        {
            SetStartPosition();

            Physics.autoSimulation = false;
            _rigidbody = GetComponent<Rigidbody>();
            _physMaterial = GetComponent<BoxCollider>().material;
        }

        public void SetStartPosition()
        {
            _startRotation = Quaternion.Euler(Vector3.zero);
            _startPos = transform.position;
            _startSize = transform.localScale;
        }
        public void ResetStartPosition()
        {
            transform.rotation = _startRotation;
            transform.position = _startPos;
        }

        //Divesh - Start of Dice physics process (roll value is the value which is needed from dice)
        public void OnDiceThrow(int rollValue)
        {
            roll = rollValue;
            ResetDice();
            //@Divesh - Turn off autosimulation 
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
            for (int i = 0; i < _diceThrowForce._DiceForceList.Count; i++)
            {
                if (randomIndex <= _diceThrowForce._DiceForceList[i].probability)
                {
                    SetDiceForce(i);
                    return;
                }
            }

        }

        private void SetDiceForce(int forceVAl)
        {

            _velocity = _diceThrowForce._DiceForceList[forceVAl].velocity;
            _drag = _diceThrowForce._DiceForceList[forceVAl].angularDrag;
            angleForce.x = Random.Range(_diceThrowForce._DiceForceList[forceVAl].angleForceMin.x,
                                                    _diceThrowForce._DiceForceList[forceVAl].angleForceMax.x);
            angleForce.y = Random.Range(_diceThrowForce._DiceForceList[forceVAl].angleForceMin.y,
                                                    _diceThrowForce._DiceForceList[forceVAl].angleForceMax.y);
            angleForce.z = Random.Range(_diceThrowForce._DiceForceList[forceVAl].angleForceMin.z,
                                                    _diceThrowForce._DiceForceList[forceVAl].angleForceMax.z);
            _mass = _diceThrowForce._DiceForceList[forceVAl].mass;
            _friction = _diceThrowForce._DiceForceList[forceVAl].friction;
        }
        private void SimlulateDice()
        {
            RandomThrowForce();

            _isInSimulation = true;

            for (int i = DiceConstVariable.VAL_ZERO; i < DiceConstVariable.DICE_SIM_LENGTH; i++)
            {
                Physics.Simulate(Time.fixedDeltaTime);
            }

            _isInSimulation = false;

            CheckDiceFace();

            ResetDice();

            ChangeIntialRotation();

            ThrowDice();
        }

        private void CheckDiceFace()
        {
            foreach (GameObject thischild in child)
            {
                if (thischild.transform.position.y > transform.localScale.x * DiceConstVariable.VAL_TEN / DiceConstVariable.VAL_HUNDRED)
                {
                    PreRollValue = 0;
                }
            }
        }
        public void ResetDice()
        {
            DiceEventManager.ChangeDiceVisibilityEventCaller(true);
            _rigidbody.velocity = Vector3.zero;
            _rigidbody.angularVelocity = Vector3.zero;
            ResetStartPosition();
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
            _isDiceStopped = false;
        }

        private void FixedUpdate()
        {

            if (_isInSimulation)
            {
                _isDiceStopped = false;
                return;
            }
            if (_rigidbody.velocity == Vector3.zero && !_isDiceStopped)
            {
                if (roll == 5)
                {
                    DiceEventManager.ShowSpecialVfxEventCaller();
                    _isDiceStopped = true;
                    GetComponent<DiceView>().SetLightPositionOnDice();
                }
                else
                {
                    _isDiceStopped = true;
                    GetComponent<DiceView>().SetLightPositionOnDice();
                }

            }
        }
    }
}