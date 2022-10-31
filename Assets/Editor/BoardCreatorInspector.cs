using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(BoardCreator))]

public class BoardCreatorInspector : Editor
{
    public BoardCreator current{
        get{
            return (BoardCreator)target;
        }
    }

    public override void OnInspectorGUI (){
        if (GUILayout.Button("Clear"))
            current.Clear();

        if(GUILayout.Button("Grow"))
            current.Grow();
        
        if(GUILayout.Button("Shrink"))
            current.Shrink();
        
        if(GUILayout.Button("GrowArea"))
            current.GrowArea();
        
        if(GUILayout.Button("ShrinkArea"))
            current.ShrinkArea();
        
        if(GUILayout.Button("Load"))
            current.Load();
        
        if(GUILayout.Button("Save"))
            current.Save();

        if(GUI.changed)
            current.UpdateMarker();

    }

}
