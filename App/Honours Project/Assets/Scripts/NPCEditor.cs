using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(NavAgentAI))]
public class NPCEditor : Editor
{
    bool showStressCalculation = true;
    bool showStressWeightings = true;

    NavAgentAI navAgentAI;

    [MenuItem("Examples/Foldout Usage")]
    static void Init()
    {
        /* NPCEditor window = (NPCEditor)GetWindow(typeof(NPCEditor));
         window.Show();*/
    }

    public override void OnInspectorGUI()
    {
        navAgentAI = (NavAgentAI)target;

        StressWeightingsUpdate();
        StressCalculationUpdate();
    }

    public void OnInspectorUpdate()
    {
        this.Repaint();
    }

    private void StressWeightingsUpdate()
    {
        showStressWeightings = EditorGUILayout.Foldout(showStressWeightings, "Stress Weightings");
        if (showStressWeightings)
            if (Selection.activeTransform)
            {
                navAgentAI.fightWeighting = EditorGUILayout.Slider("Fight Weighting", navAgentAI.fightWeighting, 0, 100);
                navAgentAI.flightWeighting = EditorGUILayout.Slider("Flight Weighting", navAgentAI.flightWeighting, 0, 100);
                navAgentAI.freezeWeighting = EditorGUILayout.Slider("Freeze Weighting", navAgentAI.freezeWeighting, 0, 100);
                navAgentAI.flopWeighting = EditorGUILayout.Slider("Flop Weighting", navAgentAI.flopWeighting, 0, 100);
                navAgentAI.fawnWeighting = EditorGUILayout.Slider("Fawn Weighting", navAgentAI.fawnWeighting, 0, 100);

                //learn how to just show a variable
            }

        if (!Selection.activeTransform)
        {
            showStressWeightings = false;
        }
    }

    private void StressCalculationUpdate()
    {
        showStressCalculation = EditorGUILayout.Foldout(showStressCalculation, "Stress Calculation");
        if (showStressCalculation)
            if (Selection.activeTransform)
            {
                Selection.activeTransform.position =
                    EditorGUILayout.Vector3Field("Position", Selection.activeTransform.position);


            }
        
        if (!Selection.activeTransform)
        {
            showStressCalculation = false;
        }
    }
}
