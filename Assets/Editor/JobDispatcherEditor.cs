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
            List<Job> currentJobs = new List<Job>(jobDispatcher.AllJobs);
            foreach(Job job in currentJobs)
            {
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField(job.DisplayName);
                if(GUILayout.Button("Cancel"))
                {
                    jobDispatcher.Cancel(job);
                }
                EditorGUILayout.EndHorizontal();
            }
        }
        EditorGUILayout.EndFoldoutHeaderGroup();
    }
}
