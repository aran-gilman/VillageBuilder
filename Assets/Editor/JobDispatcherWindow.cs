using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class JobDispatcherWindow : EditorWindow
{
    [MenuItem("Window/Village Builder/Job Dispatcher")]
    public static void ShowWindow()
    {
        GetWindow(typeof(JobDispatcherWindow));
    }

    public void OnGUI()
    {
        List<Job> currentJobs = new List<Job>(JobDispatcher.Get().AllJobs);
        foreach (Job job in currentJobs)
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField(job.DisplayName);
            if (GUILayout.Button("Cancel"))
            {
                job.Cancel();
            }
            EditorGUILayout.EndHorizontal();
        }
    }
}
