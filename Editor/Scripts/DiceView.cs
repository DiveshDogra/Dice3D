using Dice3D.Physics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(DicePhysics))]
public class DiceView : MonoBehaviour
{
    private MeshRenderer _mesh;

    private void Start()
    {
        _mesh = GetComponent<MeshRenderer>();
    }
    private void OnEnable()
    {
        DiceEventManager.ChangeDiceMaterialEvent += ChangeDiceMaterial;
    }

    public void ChangeDiceMaterial(Material _mat)
    {
        _mesh.material = _mat;
    }
}
