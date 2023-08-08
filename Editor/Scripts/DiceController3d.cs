using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Dice3D.Physics;

namespace Dice3D.Controller
{
    public class DiceController3d : MonoBehaviour
    {
        public DiceModel _currentDiceModel;
        public AllDiceModel allDiceModels;
        public Transform diceSpawnPosition;
        public Light spotLight;
        public GameObject _diceView;
        public DicePhysics dicePhysics;
        public void Awake()
        {
            SetRandomDice();
            InstantiateDice(diceSpawnPosition);
        }
        private void OnEnable()
        {
            DiceEventManager.SetDiceLight += GetSpotLightObject;
        }
        public void SetRandomDice()
        {
            _currentDiceModel = allDiceModels.allDiceList[Random.Range(0, allDiceModels.allDiceList.Count)];
        }

        public void InstantiateDice(Transform _transForm)
        {
            _diceView = Instantiate(_currentDiceModel.dicePrefab, _transForm.position, _transForm.rotation);
            _diceView.transform.parent = _transForm;
            dicePhysics = _diceView.GetComponent<DicePhysics>();
            DiceEventManager.CreateTrailVfxEventCaller(_currentDiceModel.trailVfx);
            DiceEventManager.LoadDiceCollisionVfxEventCaller(_currentDiceModel.collisonVfx[0]);
            DiceEventManager.LoadDiceSpecialVfxEventCaller(_currentDiceModel.specialVfx);
        }

        public void DiceThrown(int value)
        {
            DiceEventManager.DiceThrowEventEventCaller(value);
        }

        public void ChangeDiceMaterial(Material _mat)
        {
            DiceEventManager.DiceMaterialChangeEventCaller(_mat);
        }

        public void ChangeDiceVisiblity(bool isVisible)
        {
            DiceEventManager.ChangeDiceVisibilityEventCaller(isVisible);
        }

        public void GetSpotLightObject(Light light)
        {
            spotLight = light;
        }
    }
}