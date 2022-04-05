using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "New Duck", fileName = "New Duck")]
public class ScriptableDucks : ScriptableObject
{
    public Color color;
    public int points;
    public bool isNormal;
    [Range(0,100)] public int spawnRate;
    public float timeToCross = 2f;
}
