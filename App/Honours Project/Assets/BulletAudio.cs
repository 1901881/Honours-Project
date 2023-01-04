using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletAudio : MonoBehaviour
{
    public AK.Wwise.Event bulletCollisionSound = new AK.Wwise.Event();

    [SerializeField]
    private AK.Wwise.RTPC rtpc = null;

    // Start is called before the first frame update
    void Start()
    {
        AkSoundEngine.SetSwitch("Material", "Concrete", gameObject);
    }

/*    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("Bullet Collided");

        if (other.gameObject.tag == "Concrete")
        {
            AkSoundEngine.SetSwitch("Material", other.gameObject.tag, gameObject);
            Debug.Log("Bullet Hit Concrete");
        }
        else if (other.gameObject.tag == "Metal")
        {
            AkSoundEngine.SetSwitch("Material", other.gameObject.tag, gameObject);
        }
    }*/

    public void PlayBulletCollisionSound(float bulletHitCounter, string tag)
    {
        if (tag == "Metal" || tag == "Concrete")
        {
            AkSoundEngine.SetSwitch("Material", tag, gameObject);
        }
        rtpc.SetValue(gameObject, bulletHitCounter);
        bulletCollisionSound.Post(gameObject);
    }

    public void SetBulletCollisionSoundSwitch(string tag)
    {
        AkSoundEngine.SetSwitch("Material", tag, gameObject);
    }
}
