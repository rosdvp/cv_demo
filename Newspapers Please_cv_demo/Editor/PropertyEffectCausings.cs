using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Logic.Results;

namespace CustomEditors
{
    [CustomPropertyDrawer(typeof(EffectCausings))]
    public class PropertyEffectCausings : PropertyDrawer
    {
        public override void OnGUI(Rect rect, SerializedProperty property, GUIContent label)
        {
            rect.height = DrawUtils.LINE_HEIGHT;

            var rectHead = new Rect(rect.x, rect.y, rect.width, rect.height);
            var rectTask = DrawUtils.LineDown(rectHead);

            EditorGUI.LabelField(rectHead, "Causings:", new GUIStyle() { fontStyle = FontStyle.Bold });
            EditorGUI.indentLevel += 1;
            DrawUtils.DrawHeadValueInLine(rectTask, "Task:", 60, property.FindPropertyRelative("task"));
            EditorGUI.indentLevel -= 1;

            EditorUtility.SetDirty(property.serializedObject.targetObject);
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
            => (DrawUtils.LINE_HEIGHT + DrawUtils.LINE_SPACE) * 2;
    }
}
