using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StressRadar : MonoBehaviour
{
    private Image stressRadar;
    private float stressRadarValue;
    GameObject player;

    private void Start()
    {
        stressRadar= GetComponent<Image>();
        player = GameObject.FindWithTag("Player");
    }

    private void Update()
    {
        stressRadarValue = player.GetComponentInChildren<Weapon>().stressRadarValue;
        Debug.Log(stressRadarValue);
        stressRadar.fillAmount = stressRadarValue/100f;
    }
}
