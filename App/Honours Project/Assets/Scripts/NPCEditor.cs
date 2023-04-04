using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(NavAgentAI))]
public class NPCEditor : Editor
{
    bool showPosition = true;
    string status = "Select a GameObject";

    [MenuItem("Examples/Foldout Usage")]
    static void Init()
    {
       /* NPCEditor window = (NPCEditor)GetWindow(typeof(NPCEditor));
        window.Show();*/
    }

    public override void OnInspectorGUI()
    {
        showPosition = EditorGUILayout.Foldout(showPosition, status);
        if (showPosition)
            if (Selection.activeTransform)
            {
                Selection.activeTransform.position =
                    EditorGUILayout.Vector3Field("Position", Selection.activeTransform.position);
                status = Selection.activeTransform.name;
            }

        if (!Selection.activeTransform)
        {
            status = "Select a GameObject";
            showPosition = false;
        }
    }

    public void OnInspectorUpdate()
    {
        this.Repaint();
    }
}
