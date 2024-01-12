using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameData", menuName = "ScriptableObject/GameData", order = 1)]
public class GameData : ScriptableObject
{
    public bool touchedByToxic = false;
    public bool touchedByThunder = false;
    public bool touchedByLevelEnd = false;
    public bool touchedByBorder = false;
}
