using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyBox;
using UnityEditor;
using System.Threading.Tasks;

[Serializable]
public class GameEventData
{
    // value name
    public string valueName;
    public enum returnValueTypeEnum
    {
        Bool,
        String,
        Int,
        Float,
        Vector2,
        Vector3
    }

    // drop down for value type
    public returnValueTypeEnum valueType;


    // here we disable and enable fields acordingly

    [ConditionalField(nameof(valueType), false, returnValueTypeEnum.Bool)]
    public bool returnBool;
    [ConditionalField(nameof(valueType), false, returnValueTypeEnum.String)]
    public string returnString;
    [ConditionalField(nameof(valueType), false, returnValueTypeEnum.Int)]
    public int returnInt;
    [ConditionalField(nameof(valueType), false, returnValueTypeEnum.Float)]
    public float returnFloat;
    [ConditionalField(nameof(valueType), false, returnValueTypeEnum.Vector2)]
    public Vector2 returnVector2;
    [ConditionalField(nameof(valueType), false, returnValueTypeEnum.Vector3)]
    public Vector3 returnVector3;
}