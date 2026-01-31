using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor( typeof( Shooting_Script ) )]

public class Shooting_ScriptEditor : Editor {
    public override void OnInspectorGUI() {
        //base.OnInspectorGUI();
        Shooting_Script myTarget = ( Shooting_Script )target;
        EditorGUILayout.HelpBox( "Adjust the Target Score to how many targets need to be hit to end the run", MessageType.Info );
        myTarget.targetScore = EditorGUILayout.IntField( "Target Score", myTarget.targetScore );
        EditorGUILayout.LabelField( "Current Best Time", PlayerPrefs.GetFloat( "BestTime" ).ToString("F2" ));
        if ( GUILayout.Button( "Reset Best Time" ) ) {
            myTarget.ResetBestTime();
        }
    }
}
