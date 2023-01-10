using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadioAudio : MonoBehaviour
{
    [SerializeField]
    private AK.Wwise.RTPC rtpc = null;

    [SerializeField]
    private AK.Wwise.RTPC playerHealthRTPC = null;

    [SerializeField]
    private AK.Wwise.RTPC isRadioInWaterRTPC = null;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Water")
        {
            SetIsRadioInWater(1);
            Debug.Log(other.gameObject.tag);
        }
        else if (other.gameObject.tag != "Water" && other.gameObject.tag != "Untagged")
        {
            SetIsRadioInWater(0);
            Debug.Log(other.gameObject.tag);
        }
    }

    public void SetRadioAudioHealth(float radioHealth)
    {
        rtpc.SetValue(gameObject, radioHealth);
    }

    public void SetPlayerAudioHealth(float playerHealth)
    {
        Debug.Log(playerHealth);
        playerHealthRTPC.SetValue(gameObject, playerHealth);
        //playerHealthRTPC.SetGlobalValue(playerHealth);
        //AkSoundEngine.SetRTPCValue("playerHealth", playerHealth);
    }

    public void SetIsRadioInWater(float i)
    {
        
        isRadioInWaterRTPC.SetValue(gameObject, i);
        //playerHealthRTPC.SetGlobalValue(playerHealth);
        //AkSoundEngine.SetRTPCValue("playerHealth", playerHealth);
    }

}

