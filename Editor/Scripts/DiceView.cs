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
    public ParticleSystem _specialVfx;
    private DicePhysics _dicePhysics;
    private bool _iscollided;
    [SerializeField]
    private GameObject _spotLightObject;
    private GameObject _spotLightInstance;
    private Light _spotLight;
    private void Start()
    {
        _mesh = GetComponent<MeshRenderer>();
        _dicePhysics = GetComponent<DicePhysics>();
        CreateLight();
    }

    private void CreateLight()
    {
        _spotLightInstance = Instantiate(_spotLightObject, transform.position, Quaternion.identity);
        _spotLightInstance.transform.parent = transform.parent;
        _spotLight = _spotLightInstance.GetComponent<Light>();
        DiceEventManager.GetSpotLightEventCaller(_spotLight);
    }

  
    private void OnEnable()
    {
        DiceEventManager.ChangeDiceMaterialEvent += ChangeDiceMaterial;
        DiceEventManager.CreateTrailVfxEvent += CreateTrailParticle;
        DiceEventManager.LoadCollisionVfxEvent += SetCollisionVfx;
        DiceEventManager.LoadSpecialVfxEvent += SetSpecialVfx;
        DiceEventManager.ShowSpecialVfxEvent += ShowSpecialVfx;
        DiceEventManager.ChangeDiceVisibility += DiceVisibility;
    }

    private void OnDisable()
    {
        DiceEventManager.ChangeDiceMaterialEvent -= ChangeDiceMaterial;
        DiceEventManager.CreateTrailVfxEvent -= CreateTrailParticle;
        DiceEventManager.LoadCollisionVfxEvent -= SetCollisionVfx;
        DiceEventManager.LoadSpecialVfxEvent -= SetSpecialVfx;
        DiceEventManager.ShowSpecialVfxEvent -= ShowSpecialVfx;
        DiceEventManager.ChangeDiceVisibility -= DiceVisibility;
    }

    public void ChangeDiceMaterial(Material _mat)
    {
        Debug.Log("Mat Change Triggered");
        _mesh.material = _mat;
       

    }

    public void CreateTrailParticle(ParticleSystem _vfxObject)
    {
        var vfx = Instantiate(_vfxObject, transform.position, Quaternion.identity);
        vfx.transform.parent = transform;
    }

    public void SetCollisionVfx(ParticleSystem _vfxObject)
    {
        _collisonVfx = Instantiate(_vfxObject, transform.position, Quaternion.identity);
        _collisonVfx.transform.parent = transform;

    }
    private void OnCollisionEnter(Collision collision)
    {
        if(_dicePhysics._isInSimulation)
        {
            _iscollided = false;
            return;
        }
        if(!_iscollided)
        {
            Vector3 collisionPoint = collision.GetContact(DiceConstVariable.VAL_ZERO).point;
            _collisonVfx.transform.position = collisionPoint;
            _collisonVfx.Play();
            _iscollided = true;
        }
    }

    public void SetSpecialVfx(ParticleSystem vfxObject)
    {
        _specialVfx = Instantiate(vfxObject, transform.position, Quaternion.identity);
        _specialVfx.transform.parent = transform;
    }

    public void ShowSpecialVfx()
    {
        _specialVfx.Play();
    }

    public void DiceVisibility(bool isVisible)
    {
        _mesh.enabled = isVisible;
        if (!isVisible)
        {
            _spotLight.enabled = isVisible;
        }

    }

    public void FlashLightOnDice()
    {
        _spotLight.enabled = true;
        _spotLightInstance.transform.rotation = Quaternion.Euler(90f, 0, 0);
        _spotLightInstance.transform.position = gameObject.transform.position + Vector3.up;
    }
}
