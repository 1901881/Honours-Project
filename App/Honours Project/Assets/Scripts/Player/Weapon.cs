using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{

    public GameObject bulletPrefab;
    public Transform firePoint;
    public Animator gunAnimator;
    public float fireForce = 20.0f;
    public int bulletNum = 3;
    public FollowPlayer PlayerAudio;


    private bool reloaded = true;
    public float reloadTime = 1.5f;
    public float stressRadarValue;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (reloaded)
            {
                StartCoroutine(FireWait());
            }
        }

        StressValueRaycast();
    }

    public void Fire()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        bullet.GetComponent<Rigidbody2D>().AddForce(firePoint.up * fireForce, ForceMode2D.Impulse);
        //bullet.GetComponent<Rigidbody2D>().drag = 20;
        gunAnimator.SetTrigger("Shoot");
        PlayerAudio.PlayShoot();
    }

    IEnumerator FireWait()
    {
        Camera Cam = Camera.main;
    

        reloaded = false;
        for (int i = 0; i < bulletNum; i++)
        {
            Fire();
            Cam.GetComponent<ScreenShake>().ShakeCam(0.14f, 0.6f);

            yield return new WaitForSeconds(0.05f);
        }
        yield return new WaitForSeconds(reloadTime);
        reloaded = true;
    }

    private void StressValueRaycast()
    {
        // Cast a ray straight down.
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.up);
        // If it hits something...
        if (hit.collider != null)
        {
            if (hit.collider.gameObject.tag == "Enemy")
            {
                stressRadarValue = hit.collider.gameObject.GetComponent<NavAgentAI>().stressValue;
            }
        }
    }
}
