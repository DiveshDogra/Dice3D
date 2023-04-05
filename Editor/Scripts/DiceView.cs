using Dice3D.Physics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(DicePhysics), (typeof(BoxCollider)), (typeof(Rigidbody)))]
public class DiceView : MonoBehaviour
{
    private MeshRenderer _mesh;
    public ParticleSystem _collisonVfx;
    public Transform trailTransform;
    private void Start()
    {
        _mesh = GetComponent<MeshRenderer>();
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
        Vector3 collisionPoint = collision.contacts[0].point;
        Instantiate(_collisonVfx, collisionPoint, Quaternion.identity);
    }
}
