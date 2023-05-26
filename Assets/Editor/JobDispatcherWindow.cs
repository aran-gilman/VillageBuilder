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

    private Dictionary<Job, bool> jobFoldouts = new Dictionary<Job, bool>();

    public void OnGUI()
    {
        List<Job> currentJobs = new List<Job>(JobDispatcher.Get().AllJobs);
        foreach (Job job in currentJobs)
        {
            bool shown = false;
            if(!jobFoldouts.TryGetValue(job, out shown))
            {
                jobFoldouts.Add(job, false);
            }

            jobFoldouts[job] = EditorGUILayout.BeginFoldoutHeaderGroup(shown, job.DisplayName);
            if (jobFoldouts[job])
            {
                EditorGUI.indentLevel++;
                EditorGUILayout.LabelField("Status:", job.Status.ToString());
                EditorGUILayout.LabelField("Assignee:", job.Assignee == null ? "None" : job.Assignee.name);
                EditorGUI.indentLevel--;
                if (GUILayout.Button("Cancel"))
                {
                    job.Cancel();
                }
            }
            EditorGUILayout.EndFoldoutHeaderGroup();
        }
    }
}
