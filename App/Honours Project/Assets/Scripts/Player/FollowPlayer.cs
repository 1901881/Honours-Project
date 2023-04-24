using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public GameObject Player;

    public AK.Wwise.Event footstepSound = new AK.Wwise.Event();

    /// The Wwise event to trigger a dash sound.
    public AK.Wwise.Event dashSound = new AK.Wwise.Event();

    /// The Wwise event to trigger a shoot sound.
    public AK.Wwise.Event shootSound = new AK.Wwise.Event();

    private string FloorMaterial;
    private string LastFloorMaterial;

    // Start is called before the first frame update
    void Start()
    {
        AkSoundEngine.SetSwitch("Material", "Concrete", gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position = Player.transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "ReverbZone")
        {
           // Debug.Log("in reverb zone");
        }

        LastFloorMaterial = FloorMaterial;
        FloorMaterial = other.gameObject.tag;

        if (FloorMaterial != LastFloorMaterial)
        {
            AkSoundEngine.SetSwitch("Material", FloorMaterial, gameObject);
            //Debug.Log(FloorMaterial);
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

    public void PlayShoot()
    {
        shootSound.Post(gameObject);
    }


}
