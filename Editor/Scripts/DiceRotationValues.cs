using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Dice Data", menuName = "Scriptable Object/Dice Data")]

public class DiceRotationValues : ScriptableObject
{
    public List<FaceRelativeRotation> faceRelativeRotation;

    [System.Serializable]
    public struct FaceRelativeRotation
    {
        public string element;
        public List<Quaternion> rotation;
    }
}
