using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Logic.Results;

namespace CustomEditors
{
    [CustomPropertyDrawer(typeof(EffectDeltas))]
    public class PropertyEffectDeltas : PropertyDrawer
    {
        public override void OnGUI(Rect rect, SerializedProperty property, GUIContent label)
        {
            rect.height = DrawUtils.LINE_HEIGHT;

            var rectHead = new Rect(rect.x, rect.y, rect.width, rect.height);
            var rectMoney = DrawUtils.LineDown(rectHead);
            rectMoney.width /= 2;
            var rectRep = new Rect(rectMoney.x + rectMoney.width, rectMoney.y, rectMoney.width, rect.height);
            var rectMood = DrawUtils.LineDown(rect, 2);

            EditorGUI.LabelField(rectHead, "Deltas:", new GUIStyle() { fontStyle = FontStyle.Bold });
            EditorGUI.indentLevel += 1;
            DrawUtils.DrawHeadValueInLine(rectMoney, "Money:", 70, property.FindPropertyRelative("moneyDelta"));
            DrawUtils.DrawHeadValueInLine(rectRep, "Rep:", 70, property.FindPropertyRelative("repDelta"));

            var moodHeads = System.Enum.GetNames(typeof(AuditoryType));
            var moodProps = DrawUtils.GetPreparedArray(property.FindPropertyRelative("moodsDeltas"), moodHeads.Length);
            DrawUtils.DrawArray(rectMood, moodHeads, 60, moodProps, inLine: true);
            EditorGUI.indentLevel -= 1;

            EditorUtility.SetDirty(property.serializedObject.targetObject);
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
            => (DrawUtils.LINE_HEIGHT + DrawUtils.LINE_SPACE) * 3;
    }
}