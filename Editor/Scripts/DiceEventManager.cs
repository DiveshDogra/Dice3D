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
    public static void DiceThrowEventEventCaller(int _diceValue)
    {
        DiceThrowEvent?.Invoke(_diceValue);
    }

    public static void DiceMaterialChangeEventCaller(Material mat)
    {
        ChangeDiceMaterialEvent?.Invoke(mat);
    }
}
