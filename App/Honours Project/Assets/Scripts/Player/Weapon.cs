using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{

    public GameObject bulletPrefab;
    public Transform firePoint;
    public float fireForce = 20.0f;
    public int bulletNum = 3;

    private bool reloaded = true;
    private float reloadTime = 1.5f;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (reloaded)
            {
                StartCoroutine(FireWait());
            }

        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Bullet1Up")
        {
            bulletNum++;
            Destroy(collision.gameObject);
        }

    }

    public void Fire()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        bullet.GetComponent<Rigidbody2D>().AddForce(firePoint.up * fireForce, ForceMode2D.Impulse);
        //bullet.GetComponent<Rigidbody2D>().drag = 20;
    }

    IEnumerator FireWait()
    {
        reloaded = false;
        for (int i = 0; i < bulletNum; i++)
        {
            Fire();
            yield return new WaitForSeconds(0.05f);
        }
        yield return new WaitForSeconds(reloadTime);
        reloaded = true;
    }
}
