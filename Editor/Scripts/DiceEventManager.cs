using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class DiceEventManager : MonoBehaviour
{
    private static DiceEventManager _diceEventManager;
    public static DiceEventManager diceEventManager;

    private void Start()
    {
        diceEventManager = this;
        DontDestroyOnLoad(this);
    }

    public static event Action <int> DiceThrowEvent;

    public static event Action<Material> ChangeDiceMaterialEvent;

    public static event Action<ParticleSystem> CreateTrailVfxEvent;

    public static event Action<ParticleSystem> SetTrailVfxEvent;
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

    public static void SetDiceCollisionVfxEventCaller(ParticleSystem vfx)
    {
        SetTrailVfxEvent?.Invoke(vfx);
    }
}
