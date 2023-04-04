using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dice3D.Controller
{
    public class DiceController3d : MonoBehaviour
    {
        private DiceModel _currentDiceModel;
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
            _diceView.GetComponent<MeshRenderer>().material = _currentDiceModel.allClassesMaterial[0];
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