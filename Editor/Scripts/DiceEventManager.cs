using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class DiceEventManager : MonoBehaviour
{
    public static DiceEventManager diceEventManager;

    private void Start()
    {
        diceEventManager = this;
        DontDestroyOnLoad(this);
    }

    public static event Action <int> DiceThrowEvent;

    public static event Action<Material> ChangeDiceMaterialEvent;

    public static event Action<ParticleSystem> CreateTrailVfxEvent;

    public static event Action<ParticleSystem> LoadCollisionVfxEvent;

    public static event Action<ParticleSystem> LoadSpecialVfxEvent;

    public static event Action ShowSpecialVfxEvent;

    public static event Action<bool> ChangeDiceVisibility;

    public static event Action<Light> SetDiceLight;

    public static void ChangeDiceVisibilityEventCaller(bool isVisible)
    {
        ChangeDiceVisibility?.Invoke(isVisible);
    }

    public static void DiceThrowEventEventCaller(int _diceValue)
    {
        DiceThrowEvent?.Invoke(_diceValue);
    }

    public static void DiceMaterialChangeEventCaller(Material mat)
    {
        ChangeDiceMaterialEvent?.Invoke(mat);
    }

    public static void CreateTrailVfxEventCaller(ParticleSystem vfx)
    {
        CreateTrailVfxEvent?.Invoke(vfx);
    }

    public static void LoadDiceCollisionVfxEventCaller(ParticleSystem vfx)
    {
        LoadCollisionVfxEvent?.Invoke(vfx);
    }

    public static void LoadDiceSpecialVfxEventCaller(ParticleSystem vfx)
    {
        LoadSpecialVfxEvent?.Invoke(vfx);
    }

    public static void ShowSpecialVfxEventCaller()
    {
        ShowSpecialVfxEvent?.Invoke();
    }

    public static void GetSpotLightEventCaller(Light light)
    {
        SetDiceLight?.Invoke(light);
    }
}
