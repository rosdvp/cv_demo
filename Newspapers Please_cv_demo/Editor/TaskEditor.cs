using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Datas.Tasks;

namespace CustomEditors
{
    [CustomEditor(typeof(DataTask))]
    public class TaskEditor : Editor
    {
        DataTask Task { get; set; }

        private void OnEnable()
            => Task = (DataTask)target;


        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            EditorGUILayout.BeginHorizontal();
            Task.successesCount = EditorGUILayout.IntPopup(Task.successesCount, new string[] { "1 success", "2 successes" }, new int[] { 1, 2 }, GUILayout.MaxWidth(100));
            Task.isReversed = EditorGUILayout.ToggleLeft("Reversed", Task.isReversed, GUILayout.MaxWidth(100));
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            Task.withRating = EditorGUILayout.ToggleLeft("Min Rating", Task.withRating, GUILayout.MaxWidth(100));
            if (Task.withRating)
                Task.minRating = (NewsRating)EditorGUILayout.EnumPopup(Task.minRating);
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            Task.withType = EditorGUILayout.ToggleLeft("Require Type", Task.withType, GUILayout.MaxWidth(100));
            if (Task.withType)
                Task.newsType = (NewsType)EditorGUILayout.EnumPopup(Task.newsType);
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            Task.withIdea = EditorGUILayout.ToggleLeft("Require Idea", Task.withIdea, GUILayout.MaxWidth(100));
            if (Task.withIdea)
                Task.idea = (Idea)EditorGUILayout.EnumPopup(Task.idea);
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            Task.withInterests = EditorGUILayout.ToggleLeft("Min Interests", Task.withInterests, GUILayout.MaxWidth(100));
            if (Task.withInterests)
            {
                var interestsHeads = System.Enum.GetNames(typeof(AuditoryType));
                var interestsProps = DrawUtils.GetPreparedArray(serializedObject.FindProperty("minInterests"), interestsHeads.Length);
                EditorGUILayout.BeginVertical();
                for (var i = 0; i < interestsProps.Count; i++)
                    EditorGUILayout.PropertyField(interestsProps[i], new GUIContent(interestsHeads[i]), true);
                EditorGUILayout.EndVertical();
            }
            EditorGUILayout.EndHorizontal();

            serializedObject.ApplyModifiedProperties();
            EditorUtility.SetDirty(serializedObject.targetObject);
        }
    }
}