using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public Camera menuCam;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.GetRayIntersection(Camera.main.ScreenPointToRay(Input.mousePosition));

            if (hit.collider != null)
            {
                if (hit.collider.gameObject.name == "MenuButton")
                {
                    Debug.Log("Hit Menu Button");
                }

                if (hit.collider.gameObject.name == "PlayButton")
                {
                    Debug.Log("Hit Play Button");
                }

                if (hit.collider.gameObject.name == "QuitButton")
                {
                    Debug.Log("Hit Quit Button");
                }
                Debug.Log("Target Position: " + hit.collider.gameObject.transform.position);
            }
            
        } 
    }
}
