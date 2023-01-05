using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShelfAudio : MonoBehaviour
{
    public AK.Wwise.Event shelfMoveSoundPlay = new AK.Wwise.Event();
    public AK.Wwise.Event shelfMoveSoundPause = new AK.Wwise.Event();

    public void SwitchShelfSound(bool isMovingLateral)
    {
        if(isMovingLateral)
        {
            AkSoundEngine.SetSwitch("ShelfDirection", "Lateral", gameObject);
        }
        else
        {
            AkSoundEngine.SetSwitch("ShelfDirection", "Horizontal", gameObject);
        }
    }

    public void PlayShelfSound()
    {
        shelfMoveSoundPlay.Post(gameObject);
    }

    public void PauseShelfSound()
    {
        shelfMoveSoundPause.Post(gameObject);
    }

}

