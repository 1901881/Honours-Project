using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShelfScript : MonoBehaviour
{
    public ShelfAudio shelfAudioScript;

    private Rigidbody2D rb;//References rigidbody applied to prefab

    string dir;

    bool Played = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();//sets rigid body to variable
    }

    private void Update()
    {
        CheckDirection();
    }

    void CheckDirection()
    {
        //If object is moving
        if (rb.velocity.magnitude > 0)
        {
            float dotPRight = Vector3.Dot(transform.right.normalized, rb.velocity.normalized);
            if (dotPRight > 0.5f || dotPRight < -0.5f)//Check if moving right or left
            {
                //Horizontal Movement
                shelfAudioScript.SwitchShelfSound(false);
                
            }

            float dotPUp = Vector3.Dot(transform.up.normalized, rb.velocity.normalized);
            if (dotPUp > 0.5f || dotPUp < -0.5f)//Check if moving up or down
            {
                //Lateral Movement
                shelfAudioScript.SwitchShelfSound(true);

            }

            /*            Debug.Log(dir);
                        Debug.Log(dotPRight);
                        Debug.Log(dotPUp);*/
            
            if(!Played)
            {
                shelfAudioScript.PlayShelfSound();
                Played = true;
            }
            
        }
        else
        {
            //pause
            shelfAudioScript.PauseShelfSound();
            Played = false;
        }
       
    }
}



/*
think . right is for rghta left
.up is up and down

 */