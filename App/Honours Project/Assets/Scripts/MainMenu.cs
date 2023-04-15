using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public Camera menuCam;
    Transform[] allChildren;
    private void Start()
    {
        allChildren = GetComponentsInChildren<Transform>();
        foreach (Transform child in allChildren)
        {
            if(child.tag == "Numbers")
            {
                child.gameObject.SetActive(false);
            }
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.GetRayIntersection(menuCam.ScreenPointToRay(Input.mousePosition));

            if (hit.collider != null)
            {
                MenuButtonUpdate(hit);
                LevelButtonUpdate(hit);

                
                //Debug.Log("Target Position: " + hit.collider.gameObject.transform.position);
            }
            
        } 
    }

    void MenuButtonUpdate(RaycastHit2D hit)
    {
        if (hit.collider.gameObject.name == "MenuButton")
        {
            Debug.Log("Hit Menu Button");
            foreach (Transform child in allChildren)
            {
                if (child.tag == "MenuButtons")
                {
                    child.gameObject.SetActive(false);
                }
                if (child.tag == "Numbers")
                {
                    child.gameObject.SetActive(true);
                }
            }
        }

        if (hit.collider.gameObject.name == "PlayButton")
        {
            Debug.Log("Hit Play Button");
        }

        if (hit.collider.gameObject.name == "QuitButton")
        {
            Debug.Log("Hit Quit Button");
        }
    }

    void LevelButtonUpdate(RaycastHit2D hit)
    {
        if (hit.collider.gameObject.name == "BackArrow")
        {
            Debug.Log("Hit Back Arrow");
            foreach (Transform child in allChildren)
            {
                if (child.tag == "MenuButtons")
                {
                    child.gameObject.SetActive(true);
                }
                if (child.tag == "Numbers")
                {
                    child.gameObject.SetActive(false);
                }
            }
        }

        //Levels
        if (hit.collider.gameObject.name == "1")
        {
            Debug.Log("Load Level 1");
        }
    }
}
/*
 To DO:

    get children of game object camera

    get children of gameobject, numbers, buttons
    turn off numbers on start

need a back button
    
 */

