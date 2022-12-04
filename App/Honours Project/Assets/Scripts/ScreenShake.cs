using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShake : MonoBehaviour
{
    float shakeTime;
    float shakeMag;

    Vector3 BasePos;

    // Start is called before the first frame update
    void Start()
    {
        BasePos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(shakeTime > 0)
        {
            transform.localPosition = (Vector2)BasePos + (Random.insideUnitCircle * shakeMag);

            shakeTime -= Time.deltaTime;
        }
        else
        {
            transform.position = BasePos;
        }
    }

    public void ShakeCam(float Timer, float Magnitude)
    {
        shakeTime += Timer;
        shakeMag = Magnitude;
    }

    public void ResetShake()
    {
        shakeTime = 0f;
    }

}
