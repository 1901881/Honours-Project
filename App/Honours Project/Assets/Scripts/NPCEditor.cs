using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(NavAgentAI))]
public class NPCEditor : Editor
{
    bool showNPCInfoGroup, showStressWeightingsGroup, showStressCalculationGroup = true;
    

    NavAgentAI navAgentAI;

    /*
         [HideInInspector]
    public BehaviorTree behaviorTree;
    [HideInInspector]
    public Sprite hitSprite;

    public float fleeRadius = 2;

    [HideInInspector]
    public GameObject explosionPrefab;
    //Changing Enemy Color
    private SpriteRenderer SpriteRend;
    private UnityEngine.Color originalColor;


    public float maxHealth = 3;
    public float health = 0;

    public float bulletRadius;
    


    //public stress variables
    [Header("Stress Value Calculation")]
    public float distanceToTarget;
    public int recentlyHit = 0;
    public int bulletCounter = 0;
    public float healthFactor = 0;
    public float distance_stress;
    public float bullet_stress;
    public float stressValue;
    public  int stressResponseIndex = -1; //maybe change using switch to equal string
    public bool stressResponseRunning = false;

    public float stressFortitudeDecrease = 0;

     */

    #region SerializedProperties
    //NPC Info
    SerializedProperty npcType;
    SerializedProperty maxHealth;
    SerializedProperty health;

    //stress sliders
    SerializedProperty stressFortitude;
    SerializedProperty fightWeighting;
    SerializedProperty flightWeighting;
    SerializedProperty freezeWeighting;
    SerializedProperty flopWeighting;
    SerializedProperty fawnWeighting;

    //Stress Calculation
    SerializedProperty distanceToTarget;
    SerializedProperty recentlyHit;
    SerializedProperty bulletCounter;
    SerializedProperty healthFactor;
    SerializedProperty distance_stress;
    SerializedProperty bullet_stress;
    SerializedProperty stressFortitudeDecrease;
    SerializedProperty stressValue;
    SerializedProperty stressResponseIndex;
    SerializedProperty stressResponseRunning;

    #endregion

    private void OnEnable()
    {
        npcType = serializedObject.FindProperty("npcType");
        maxHealth = serializedObject.FindProperty("maxHealth");
        health = serializedObject.FindProperty("health");

        stressFortitude = serializedObject.FindProperty("stressFortitude");
        fightWeighting = serializedObject.FindProperty("fightWeighting");
        flightWeighting = serializedObject.FindProperty("flightWeighting");
        freezeWeighting = serializedObject.FindProperty("freezeWeighting");
        flopWeighting = serializedObject.FindProperty("flopWeighting");
        fawnWeighting = serializedObject.FindProperty("fawnWeighting");

        distanceToTarget = serializedObject.FindProperty("distanceToTarget");
        recentlyHit = serializedObject.FindProperty("recentlyHit");
        bulletCounter = serializedObject.FindProperty("bulletCounter");
        healthFactor = serializedObject.FindProperty("healthFactor");
        distance_stress = serializedObject.FindProperty("distance_stress");
        bullet_stress = serializedObject.FindProperty("bullet_stress");
        stressFortitudeDecrease = serializedObject.FindProperty("stressFortitudeDecrease");
        stressValue = serializedObject.FindProperty("stressValue");
        stressResponseIndex = serializedObject.FindProperty("stressResponseIndex");
        stressResponseRunning = serializedObject.FindProperty("stressResponseRunning");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        navAgentAI = (NavAgentAI)target;

        EditorGUILayout.PropertyField(npcType);

        NPCInfoUpdate();
        StressWeightingsUpdate();
        StressCalculationUpdate();

        NPCTypeUpdate();

        serializedObject.ApplyModifiedProperties();
    }

    public void OnInspectorUpdate()
    {
        this.Repaint();
    }

    private void NPCInfoUpdate()
    {
        showNPCInfoGroup = EditorGUILayout.BeginFoldoutHeaderGroup(showNPCInfoGroup, "NPC Info");
        if (showNPCInfoGroup)
        {
            EditorGUILayout.PropertyField(maxHealth);
            EditorGUILayout.PropertyField(health);
        }
        EditorGUILayout.EndFoldoutHeaderGroup();
    }

    private void StressWeightingsUpdate()
    {
        showStressWeightingsGroup = EditorGUILayout.BeginFoldoutHeaderGroup(showStressWeightingsGroup, "Stress Weightings");
        if (showStressWeightingsGroup)
        {
            EditorGUILayout.PropertyField(stressFortitude);
            EditorGUILayout.PropertyField(fightWeighting);
            EditorGUILayout.PropertyField(flightWeighting);
            EditorGUILayout.PropertyField(freezeWeighting);
            EditorGUILayout.PropertyField(flopWeighting);
            EditorGUILayout.PropertyField(fawnWeighting);
        }
        EditorGUILayout.EndFoldoutHeaderGroup();
    }

    private void StressCalculationUpdate()
    {
        showStressCalculationGroup = EditorGUILayout.BeginFoldoutHeaderGroup(showStressCalculationGroup, "Stress Calculation");
        if (showStressCalculationGroup)
        {
            EditorGUILayout.PropertyField(distanceToTarget);
            EditorGUILayout.PropertyField(recentlyHit);
            EditorGUILayout.PropertyField(bulletCounter);
            EditorGUILayout.PropertyField(healthFactor);
            EditorGUILayout.PropertyField(distance_stress);
            EditorGUILayout.PropertyField(bullet_stress);
            EditorGUILayout.PropertyField(stressFortitudeDecrease);
            EditorGUILayout.PropertyField(stressValue);
            EditorGUILayout.PropertyField(stressResponseIndex);
            EditorGUILayout.PropertyField(stressResponseRunning);
        }
        EditorGUILayout.EndFoldoutHeaderGroup();
     
    }

    private bool[] typeUpdateVariables = new bool[5];
    // index 0: brawlerTypeUpdate
    // index 1: scaredycatTypeUpdate
    // index 2: freezeTypeUpdate
    // index 3: flopTypeUpdate
    // index 4: danselTypeUpdate
    private void NPCTypeUpdate()
    {
        switch (navAgentAI.npcType)
        {
            case NavAgentAI.NPCType.Brawler:
                if (!typeUpdateVariables[0])
                {
                    SetStressWeightings(30, 70, 0, 10, 5, 0);
                    SetTypeUpdateVariables(0);
                }
                break;
            case NavAgentAI.NPCType.ScaredyCat:
                if (!typeUpdateVariables[1])
                {
                    SetStressWeightings(30, 35, 5, 70, 0, 0);
                    SetTypeUpdateVariables(1);
                }
                break;
            case NavAgentAI.NPCType.freeze:
                if (!typeUpdateVariables[2])
                {
                    SetStressWeightings(30, 0, 0, 90, 5, 0);
                    SetTypeUpdateVariables(2);
                }   
                break;
            case NavAgentAI.NPCType.flop:
                if (!typeUpdateVariables[3])
                {
                    SetStressWeightings(30, 0, 0, 10, 80, 0);
                    SetTypeUpdateVariables(3);
                } 
                break;
            case NavAgentAI.NPCType.Dansel:
                if (!typeUpdateVariables[4])
                {
                    SetStressWeightings(60, 0, 0, 10, 5, 80);
                    SetTypeUpdateVariables(4);
                }
                break;
            case NavAgentAI.NPCType.Custom:
                SetAllTypeUpdateVariables(false);
                SetStressWeightings(0, 0, 0, 0, 0, 0);
                break;
            default:
                SetAllTypeUpdateVariables(false);
                SetStressWeightings(0, 0, 0, 0, 0, 0);
                break;
        }
    }

    private void SetTypeUpdateVariables(int typeIndex)
    {
        for (int i = 0; i < typeUpdateVariables.Length; i++)
        {
            typeUpdateVariables[i] = false;
        }
        typeUpdateVariables[typeIndex] = true;
    }

    private void SetAllTypeUpdateVariables(bool value)
    {
        for (int i = 0; i < typeUpdateVariables.Length; i++)
        {
            typeUpdateVariables[i] = false;
        }
    }

    private void SetStressWeightings(float stressFortitude, float fightWeighting, float flightWeighting, float freezeWeighting, float flopWeighting, float fawnWeighting)
    {
        this.stressFortitude.floatValue = stressFortitude;
        this.fightWeighting.floatValue = fightWeighting;
        this.flightWeighting.floatValue = flightWeighting;
        this.freezeWeighting.floatValue = freezeWeighting;
        this.flopWeighting.floatValue = flopWeighting;
        this.fawnWeighting.floatValue = fawnWeighting;
    }                                                       
}                                                           
