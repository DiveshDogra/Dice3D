using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Dice3D.Controller
{
    public class DiceController3d : MonoBehaviour
    {
        public DiceModel _currentDiceModel;
        public AllDiceModel allDiceModels;
        public Transform diceSpawnPosition;

        public void Start()
        {
            SetRandomDice();
            InstantiateDice(diceSpawnPosition);
        }

        public void SetRandomDice()
        {
            _currentDiceModel = allDiceModels.allDiceList[Random.Range(0, allDiceModels.allDiceList.Count)];
        }

        public void InstantiateDice(Transform _transForm)
        {
            var _diceView = Instantiate(_currentDiceModel.dicePrefab, _transForm.position, _transForm.rotation);
            _diceView.transform.parent = _transForm;
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
    }
}