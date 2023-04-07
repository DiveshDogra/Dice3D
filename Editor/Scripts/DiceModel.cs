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

    [System.Serializable]
    public class DiceMaterials
    {
        public string colorName;
        public Material material;
    }

    public List<DiceMaterials> AllDiceMaterials;

    public ParticleSystem trailVfx;
    public List<ParticleSystem> collisonVfx;
    public ParticleSystem specialVfx; // for dice value 6
    // TODO Add more properties for dice
}