using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

[CustomEditor(typeof(Bar))]
public class BarEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        Bar script = (Bar)target;

        // Anchor Type Dropdown
        GUIContent typeArrayLabel = new GUIContent("Anchor Position");
        script.anchorIdx = EditorGUILayout.Popup(typeArrayLabel, script.anchorIdx, script.anchorArray);
        script.anchor = script.anchorArray[script.anchorIdx];

        // Text Display Dropdown
        GUIContent textDisplayLabel = new GUIContent("Text Display");
        script.textDisplay = EditorGUILayout.Toggle(textDisplayLabel, script.textDisplay);

        if (script.textDisplay)
        {
            GUIContent textObjectLabel = new GUIContent("Text Display Object");
            script.textObject = (Text)EditorGUILayout.ObjectField(script.textObject, typeof(Text), true);

            // Text Style Dropdown
            GUIContent textStyleLabel = new GUIContent("Text Style");
            script.textStyleIdx = EditorGUILayout.Popup(textStyleLabel, script.textStyleIdx, script.textStyleArray);
            script.textStyle = script.textStyleArray[script.textStyleIdx];

            if (script.textStyle == "Custom")
            {
                GUIContent customStyleLabel = new GUIContent("Custom Style");
                script.actualStyle = EditorGUILayout.TextField(customStyleLabel, script.actualStyle);
            }
            else if (script.textStyle == "[Cur][Max]")
            {
                script.actualStyle = "[Cur]/[Max]";
            }
            else
            {
                script.actualStyle = script.textStyle;
            }

            if (script.actualStyle != null)
            {
                if (script.actualStyle.Contains("[Perc]"))
                {
                    GUIContent decimalCountLabel = new GUIContent("Number of Decimals");
                    script.decimalCount = Mathf.Clamp(EditorGUILayout.IntField(decimalCountLabel, script.decimalCount), 0, 4);
                }
            }
        }
        else
        {
            script.textObject = null;
        }

        // Set Values Dropdown
        GUIContent setValuesLabel = new GUIContent("Manual Values");
        script.manualValues = EditorGUILayout.Toggle(setValuesLabel, script.manualValues);

        if (script.manualValues)
        {
            
            GUIContent currentValueLabel = new GUIContent("Current Value");
            script.current = EditorGUILayout.FloatField(currentValueLabel, script.current);

            script.current = Mathf.Clamp(script.current, 0, script.max);

            GUIContent maxValueLabel = new GUIContent("Max Value");
            script.max = EditorGUILayout.FloatField(maxValueLabel, script.max);

            // Update Button
            if (GUILayout.Button("Reload Bar"))
            {
                script.UpdateBar(script.current);
            }
        }

    }
    
}