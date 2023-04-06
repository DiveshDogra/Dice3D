using Dice3D.Physics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dice3D.Variables;


[RequireComponent(typeof(DicePhysics), (typeof(BoxCollider)), (typeof(Rigidbody)))]
public class DiceView : MonoBehaviour
{
    private MeshRenderer _mesh;
    public ParticleSystem _collisonVfx;
    public Transform trailTransform;
    private DicePhysics _dicePhysics;
    private void Start()
    {
        _mesh = GetComponent<MeshRenderer>();
        _dicePhysics = GetComponent<DicePhysics>();
    }
    private void OnEnable()
    {
        DiceEventManager.ChangeDiceMaterialEvent += ChangeDiceMaterial;
        DiceEventManager.CreateTrailVfxEvent += CreateTrailParticle;
        DiceEventManager.SetTrailVfxEvent += SetCollisionVfx;
    }

    private void OnDisable()
    {
        DiceEventManager.ChangeDiceMaterialEvent -= ChangeDiceMaterial;
        DiceEventManager.CreateTrailVfxEvent -= CreateTrailParticle;
        DiceEventManager.SetTrailVfxEvent -= SetCollisionVfx;
    }

    public void ChangeDiceMaterial(Material _mat)
    {
        _mesh.material = _mat;
    }

    public void CreateTrailParticle(ParticleSystem _vfxObject)
    {
        var vfx = Instantiate(_vfxObject, transform.position, Quaternion.identity);
        vfx.transform.parent = transform;
    }

    public void SetCollisionVfx(ParticleSystem _vfxObject)
    {
        _collisonVfx = _vfxObject;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(!_dicePhysics._isInSimulation)
        {
            Vector3 collisionPoint = collision.GetContact(DiceConstVariable.VAL_ZERO).point;
            Instantiate(_collisonVfx, collisionPoint, Quaternion.identity);
        }
    }
}
