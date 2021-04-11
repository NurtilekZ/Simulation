using _src.Scripts.Formulas;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Formula))]
public class FomulaEditor : Editor
{
    public override void OnInspectorGUI()
    {
        Formula formula = (Formula)target;
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Add Field"))
        {
            formula.AddField();
        }
        if (GUILayout.Button("Add Sign"))
        {
            formula.AddSign();
        }
        GUILayout.EndHorizontal();
            
        if (GUILayout.Button("Remove Variable"))
        {
            formula.RemoveItem();
        }
        if (GUILayout.Button("Update Formula"))
        {
            formula.UpdateFormula();
        }
            
        base.OnInspectorGUI();
    }
}