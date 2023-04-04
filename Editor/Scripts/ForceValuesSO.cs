using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dice3D
{
    [CreateAssetMenu(fileName = "Force Values", menuName = "Scriptable Object/Dice Physics")]

    public class ForceValuesSO : ScriptableObject
    {
        [System.Serializable]
        public struct DiceThrowForce
        {
             public string name;
             public float probability;
             public  Vector3 velocity;
             public float angularDrag;
             public float mass;
             public float friction;
             public Vector3 angleForceMin;
             public Vector3 angleForceMax;
        }

        public List<DiceThrowForce> _DiceForceList;
    }
}
