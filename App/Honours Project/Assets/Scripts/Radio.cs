using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Radio : MonoBehaviour
{
    /// The Wwise event to trigger a shoot sound.
    public AK.Wwise.Event radioMusicSound = new AK.Wwise.Event();

    public float radioHealth = 3;

    public ChangeMaterialColour changeMaterialColourScript;

    public RadioAudio radioAudioScript;

    private void Start()
    {
       
    }

    void Update()
    {
        if (radioHealth <= 0)
        {
           Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            StartCoroutine(Hit());
            Debug.Log("Hit Radio");
        }
    }

    IEnumerator Hit()
    {
        changeMaterialColourScript.ChangeMaterialColourFunc(true);
        Time.timeScale = 0;
        yield return new WaitForSecondsRealtime(0.08f);
        Time.timeScale = 1;
        changeMaterialColourScript.ChangeMaterialColourFunc(false);
        radioHealth--;
        //AkSoundEngine.SetRTPCValue("radioVolume", radioHealth, AK_INVALID_GAME_OBJECT);
        radioAudioScript.SetRadioAudioHealth(radioHealth);

    }
}
