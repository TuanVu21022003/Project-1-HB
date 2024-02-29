using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="LevelOS", menuName = "ScriptableObjects/LevelOS")]

public class LevelOS : ScriptableObject
{
    public List<LevelData> list = new List<LevelData>();
}
