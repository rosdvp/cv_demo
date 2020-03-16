using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Datas.Auditories;

namespace CustomEditors
{
    [CustomEditor(typeof(DataAuditory))]
    public class AuditoryEditor : Editor
    {
        DataAuditory Auditory { get; set; }
        private void OnEnable()
            => Auditory = (DataAuditory)target;


        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            EditorGUILayout.LabelField("Recognized type: " + Auditory.Type.ToString());
            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Opinions on news types:");

            var newsTypesHeads = System.Enum.GetNames(typeof(NewsType));
            var newsTypesProps = DrawUtils.GetPreparedArray(serializedObject.FindProperty("opinionsNewsTypes"), newsTypesHeads.Length);
            for (var i = 0; i < newsTypesProps.Count; i++)
                EditorGUILayout.PropertyField(newsTypesProps[i], new GUIContent(newsTypesHeads[i]), true);

            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Dynamic opinions on news params:");

            EditorGUI.BeginDisabledGroup(true);
            var newsParamsHeads = System.Enum.GetNames(typeof(NewsParamType));
            var newsParamsProps = DrawUtils.GetPreparedArray(serializedObject.FindProperty("opinionsNewsParams"), newsParamsHeads.Length);
            for (var i = 0; i < newsParamsProps.Count; i++)
                EditorGUILayout.PropertyField(newsParamsProps[i], new GUIContent(newsParamsHeads[i]), true);
            EditorGUI.EndDisabledGroup();

            serializedObject.ApplyModifiedProperties();
            EditorUtility.SetDirty(serializedObject.targetObject);
        }
    }
}
