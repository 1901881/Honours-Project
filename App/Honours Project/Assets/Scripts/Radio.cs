using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Radio : MonoBehaviour
{
    /// The Wwise event to trigger a shoot sound.
    public AK.Wwise.Event radioMusicSound = new AK.Wwise.Event();


    // Start is called before the first frame update
    void Start()
    {
        radioMusicSound.Post(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
