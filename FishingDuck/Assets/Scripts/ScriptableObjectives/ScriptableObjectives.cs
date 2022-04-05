using UnityEngine;

[CreateAssetMenu(menuName = "New Objective", fileName = "New Objective")]
public class ScriptableObjectives : ScriptableObject
{
    public string objectiveName;
    public Sprite objectiveIconee;
    public Color color;
    public WhatToDo whatToDoWith;
    public int duckNum;
}

public enum DuckColors
{
    Red,
    Blue,
    Green,
    Yellow,
    Purple,
    White
}

public enum WhatToDo
{
    GetThisDuck,
    DontGetThisDuck,
}