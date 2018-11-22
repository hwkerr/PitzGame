using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(ASprite))]
public class ASpriteDrawer : PropertyDrawer {

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);

        // Draw label
        position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), GUIContent.none);
        Rect newpos = new Rect(position.x + 105, position.y, position.width, position.height);
        Rect position1 = EditorGUI.PrefixLabel(newpos, GUIUtility.GetControlID(FocusType.Passive), new GUIContent("Duration"));

        // Don't make child fields be indented
        var indent = EditorGUI.indentLevel;
        EditorGUI.indentLevel = 0;

        // Calculate rects
        var spriteRect = new Rect(position.x, position.y, 145, position.height);
        var durationRect = new Rect(position1.x - 15, position1.y, position1.width-90, position1.height);

        // Draw fields - passs GUIContent.none to each so they are drawn without labels
        EditorGUI.PropertyField(spriteRect, property.FindPropertyRelative("sprite"), GUIContent.none);
        EditorGUI.PropertyField(durationRect, property.FindPropertyRelative("duration"), GUIContent.none);

        // Set indent back to what it was
        EditorGUI.indentLevel = indent;

        EditorGUI.EndProperty();
    }
}