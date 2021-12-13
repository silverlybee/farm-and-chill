using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Farm))]
public class FarmEditor : Editor
{
    public override void OnInspectorGUI()
    {

        DrawDefaultInspector();
        Farm farm = (Farm)target;
        if (GUILayout.Button("Generate Farm"))
        {
            farm.GenerateFarm();
        }
    }
}
