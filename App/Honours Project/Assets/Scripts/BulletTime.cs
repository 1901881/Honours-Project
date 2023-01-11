using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletTime : MonoBehaviour
{
    public float minTimeScale = 0.5f;
    public float transitionSpeed = 1.0f;
    public float bulletTimeDuration = 2.0f;
    public float bulletTimeReloadDuration = 1.0f;
    public bool isBulletTime = false;

    public AK.Wwise.Event EMPPlay = new AK.Wwise.Event();
    public AK.Wwise.Event EMPPause = new AK.Wwise.Event();

    [SerializeField]
    private AK.Wwise.RTPC rtpc = null;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //set rtpc value with time scale
        rtpc.SetValue(gameObject, Time.timeScale);

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            //Debug.Log("Left shift hit");            
            if(!isBulletTime)
            {
                isBulletTime = !isBulletTime;
                StartCoroutine("ToggleBulletTime");
            }
        }
        
    }

    IEnumerator ToggleBulletTime()
    {
        EMPPlay.Post(gameObject);

        float t = 0f;
        float startScale = Time.timeScale;
        float targetScale = (isBulletTime) ? minTimeScale : 1;
        float lastTick = Time.realtimeSinceStartup;
        while(t <= 1f)
        {
            
            t += (Time.realtimeSinceStartup - lastTick) * (1f / transitionSpeed);
            lastTick = Time.realtimeSinceStartup;
            Time.timeScale = Mathf.Lerp(startScale, targetScale, t);
            Time.fixedDeltaTime = 0.02f * Time.timeScale;
            yield return null;
        }

        if (isBulletTime)
        {
            yield return new WaitForSeconds(bulletTimeDuration);
            isBulletTime = !isBulletTime;
            Debug.Log("ending bullet time");
            StartCoroutine("ToggleBulletTime");
        }
        else if(!isBulletTime)
        {
            EMPPause.Post(gameObject);
        }
    }
}
