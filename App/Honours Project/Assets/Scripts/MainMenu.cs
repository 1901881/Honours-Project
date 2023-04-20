using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public Camera menuCam;
    Transform[] allChildren;

    private bool test1Update = false;
    private bool test2Update = false;

    private void Start()
    {
        allChildren = GetComponentsInChildren<Transform>();
        foreach (Transform child in allChildren)
        {
            if(child.tag == "Test1Numbers" || child.tag == "Test2Numbers" || child.tag == "Test1Buttons" || child.tag == "Test2Buttons")//|| child.tag == "MenuButtons"
            {
                child.gameObject.SetActive(false); 
            }
            if(child.name == "Test 1" || child.name == "Test 2")
            {
                child.gameObject.SetActive(true);
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
                if (hit.collider.gameObject.name == "Test1")
                {
                    test1Update = true;
                    Debug.Log(hit.collider.gameObject.name.ToString());
                    foreach (Transform child in allChildren)
                    {
                        //hit.collider.gameObject.SetActive(false);
                        if (child.tag == "Test1Buttons")
                        {
                            child.gameObject.SetActive(true);
                        }
                        if (child.gameObject.name == "Test1" || child.gameObject.name == "Test2")
                        {
                            child.gameObject.SetActive(false);
                        }

                    }
                }

                if (hit.collider.gameObject.name == "Test2")
                {
                    test2Update = true;
                    Debug.Log(hit.collider.gameObject.name.ToString());
                    foreach (Transform child in allChildren)
                    {
                        //hit.collider.gameObject.SetActive(false);
                        if (child.tag == "Test2Buttons")
                        {
                            child.gameObject.SetActive(true);
                        }
                        if (child.gameObject.name == "Test1" || child.gameObject.name == "Test2")
                        {
                            child.gameObject.SetActive(false);
                        }

                    }
                }

                if (test1Update)
                {
                    MenuButtonUpdate(hit, 2, "Test1Buttons", "Test1Numbers");
                    LevelButtonUpdate(hit, "Test1Buttons", "Test1Numbers", 1, 2, 3, 4, 5);
                }
                if (test2Update)
                {
                    MenuButtonUpdate(hit, 2, "Test2Buttons", "Test2Numbers");
                    LevelButtonUpdate(hit, "Test2Buttons", "Test2Numbers", 1, 2, 3, 4, 5);
                }
            }
            
        } 
    }

    void MenuButtonUpdate(RaycastHit2D hit, int firstLevel, string testButtonsTag, string testNumbersTag)
    {
        if (hit.collider.gameObject.name == "MenuButton")
        {
            Debug.Log("Hit Menu Button");
            foreach (Transform child in allChildren)
            {
                if (child.tag == testButtonsTag)
                {
                    child.gameObject.SetActive(false);
                }
                if (child.tag == testNumbersTag)
                {
                    child.gameObject.SetActive(true);
                }
            }
        }

        if (hit.collider.gameObject.name == "PlayButton")
        {
            Debug.Log("Hit Play Button");
            SceneManager.LoadScene(firstLevel);
        }

        if (hit.collider.gameObject.name == "QuitButton")
        {
            Debug.Log("Hit Quit Button");
            #if UNITY_EDITOR
                        UnityEditor.EditorApplication.isPlaying = false;
            #endif
                        Application.Quit();
        }

        if (hit.collider.gameObject.name == "BackArrow")
        {
            Debug.Log("Hit Back Arrow");
            foreach (Transform child in allChildren)
            {
                if (child.tag == testButtonsTag)
                {
                    child.gameObject.SetActive(false);
                }
                if (child.tag == testNumbersTag)
                {
                    child.gameObject.SetActive(false);
                }
            }
        }
    }

    void LevelButtonUpdate(RaycastHit2D hit, string testButtonsTag, string testNumbersTag, int level1, int level2, int level3, int level4, int level5)
    {
        if (hit.collider.gameObject.name == "BackArrow")
        {
            test1Update = false;
            test2Update = false;
            Debug.Log("Hit Back Arrow");
            foreach (Transform child in allChildren)
            {
                if (child.tag == testButtonsTag)
                {
                    child.gameObject.SetActive(false);
                }
                if (child.gameObject.name == "Test1" || child.gameObject.name == "Test2")
                {
                    child.gameObject.SetActive(true);
                }
            }
        }

        //Levels
        if (hit.collider.gameObject.name == "1")
        {
            Debug.Log("Load Level 1");
            //Lvl_1_StressBot
            SceneManager.LoadScene(level1);
        }
        if (hit.collider.gameObject.name == "2")
        {
            Debug.Log("Load Level 2");
            //Lvl_1_StressBot
            SceneManager.LoadScene(level2);
        }
        if (hit.collider.gameObject.name == "3")
        {
            Debug.Log("Load Level 3");
            //Lvl_1_StressBot
            SceneManager.LoadScene(level3);
        }
        if (hit.collider.gameObject.name == "4")
        {
            Debug.Log("Load Level 4");
            //Lvl_1_StressBot
            SceneManager.LoadScene(level4);
        }
        if (hit.collider.gameObject.name == "5")
        {
            Debug.Log("Load Level 5");
            //Lvl_1_StressBot
            SceneManager.LoadScene(level5);
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

