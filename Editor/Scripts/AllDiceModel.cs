using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Container", menuName = "Scriptable Object/Dice List")]
public class AllDiceModel : ScriptableObject
{
  public List<DiceModel> allDiceList;
}
