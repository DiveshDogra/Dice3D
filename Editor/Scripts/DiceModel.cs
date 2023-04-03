using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[CreateAssetMenu(fileName = "Container", menuName = "Scriptable Object/Dice Container")]
public class DiceModel : ScriptableObject
{
    public string diceName;
    public GameObject dicePrefab;
    public MeshRenderer meshRenderer;
    public Rigidbody rigidbody;
    public Sprite image;

    public List<Material> allClassesMaterial;


    // TODO Add more properties for dice
}