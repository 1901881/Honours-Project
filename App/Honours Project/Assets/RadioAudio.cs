using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadioAudio : MonoBehaviour
{
    [SerializeField]
    private AK.Wwise.RTPC rtpc = null;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void SetRadioAudioHealth(float radioHealth)
    {
        rtpc.SetValue(gameObject, radioHealth);
    }


}
