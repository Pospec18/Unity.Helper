using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Pospec.Helper.Editor
{
    [CustomPropertyDrawer(typeof(SelectableList<>))]
    public class SelectableListEditor : PropertyDrawer
    {   
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            var indexProperty = property.FindPropertyRelative("selectedIdx");
            var listProperty = property.FindPropertyRelative("items");
            return EditorGUI.GetPropertyHeight(indexProperty) + EditorGUI.GetPropertyHeight(listProperty) + 5;
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var indexProperty = property.FindPropertyRelative("selectedIdx");
            var enabledProperty = property.FindPropertyRelative("someItemSelected");
            var listProperty = property.FindPropertyRelative("items");

            int listCount = listProperty.arraySize;
            int index = indexProperty.intValue;

            if(listCount == 0)
            {
                enabledProperty.boolValue = false;
            }
            else
            {
                if (index > listCount - 1)
                    indexProperty.intValue = listCount - 1;
            }

            EditorGUI.BeginProperty(position, label, property);
            EditorGUI.PropertyField(position, listProperty, label, true);
            
            EditorGUI.BeginDisabledGroup(!enabledProperty.boolValue);
            position.y += EditorGUI.GetPropertyHeight(listProperty) + 5;
            position.width -= 24;
            position.height = EditorGUI.GetPropertyHeight(indexProperty);
            GUIContent indexContent = new GUIContent("Selected Item Index", "Index of selected item. When disabled no item will be sellected");
            indexProperty.intValue = (int)EditorGUI.Slider(position, indexContent, indexProperty.intValue, 0, listCount - 1);
            EditorGUI.EndDisabledGroup();

            int indent = EditorGUI.indentLevel;
            EditorGUI.indentLevel = 0;
            position.x += position.width + 24;
            position.width = position.height = EditorGUI.GetPropertyHeight(enabledProperty);
            position.x -= position.width;
            EditorGUI.PropertyField(position, enabledProperty, GUIContent.none);
            EditorGUI.indentLevel = indent;
            EditorGUI.EndProperty();
        }
    }
}
