using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(JobDispatcher))]
public class JobDispatcherEditor : Editor
{
    private bool showJobs = false;

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        showJobs = EditorGUILayout.BeginFoldoutHeaderGroup(showJobs, "Current Jobs");
        if (showJobs)
        {
            JobDispatcher jobDispatcher = serializedObject.targetObject as JobDispatcher;
            foreach(IJob job in jobDispatcher.OpenJobs)
            {
                EditorGUILayout.LabelField(job.DisplayName);
            }
        }
        EditorGUILayout.EndFoldoutHeaderGroup();
    }
}
