using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace CustomEditors
{
    public static class DrawUtils
    {
        public static void DrawHeadValueInLine(Rect rect, string head, float headWidth, SerializedProperty propValue)
        {
            var rectHead = rect;
            rectHead.width = headWidth;
            var rectProp = rect;
            rectProp.x += rectHead.width;
            rectProp.width = rect.width - rectHead.width;

            EditorGUI.LabelField(rectHead, head, new GUIStyle() { alignment = TextAnchor.MiddleLeft });
            EditorGUI.PropertyField(rectProp, propValue, GUIContent.none);
        }

        public static void DrawArray(Rect rect, string[] heads, float headWidth, 
            List<SerializedProperty> props, bool inLine)
        {
            if (inLine)
                rect.width /= props.Count;

            for (var i = 0; i < props.Count; i++)
            {
                DrawHeadValueInLine(rect, heads[i], headWidth, props[i]);

                if (inLine)
                    rect.x += rect.width;
                else
                    rect = LineDown(rect);
            }
        }
        /*---------------------------------------------*/
        /*---------------------------------------------*/
        public const float LINE_HEIGHT = 16;
        public const float LINE_SPACE = 3;

        public static Rect LineDown(Rect rect, int times = 1)
            => new Rect(rect.x, rect.y + (LINE_HEIGHT + LINE_SPACE) * times, rect.width, rect.height);

        /*---------------------------------------------*/
        /*---------------------------------------------*/

        public static List<SerializedProperty> GetPreparedArray(SerializedProperty propArray, int count)
        {
            var result = new List<SerializedProperty>();
            for (var i = 0; i < count; i++)
            {
                if (i == propArray.arraySize)
                    propArray.InsertArrayElementAtIndex(i);
                result.Add(propArray.GetArrayElementAtIndex(i));
            }
            return result;
        }
    }
}