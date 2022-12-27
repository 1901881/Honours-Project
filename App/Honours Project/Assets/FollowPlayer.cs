using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public GameObject Player;

    public AK.Wwise.Event footstepSound = new AK.Wwise.Event();

    /// The Wwise event to trigger a dash sound.
    public AK.Wwise.Event dashSound = new AK.Wwise.Event();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position = Player.transform.position;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "ReverbZone")
        {
            Debug.Log("in reverb zone");
        }
    }

    public void PlayFootstep()
    {
        footstepSound.Post(gameObject);
    }

    public void PlayDash()
    {
        dashSound.Post(gameObject);
    }
}
