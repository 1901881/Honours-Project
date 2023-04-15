using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HUDManager : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject overlayHUD;
    [SerializeField] GameObject levelSelect;

    // Start is called before the first frame update
    void Start()
    {
        pauseMenu.SetActive(false);
        overlayHUD.SetActive(true);
        levelSelect.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Pause();
        }
    }

    public void Pause()
    {
        pauseMenu.SetActive(true);
        overlayHUD.SetActive(false);
        levelSelect.SetActive(false);
        Time.timeScale = 0;
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
        overlayHUD.SetActive(true);
        levelSelect.SetActive(false);
        Time.timeScale = 1;
    }

    public void HomeClicked()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
    }

    public void LevelSelectClicked()
    {
        pauseMenu.SetActive(false);
        levelSelect.SetActive(true);
    }

    public void BackArrowClicked()
    {
        pauseMenu.SetActive(true);
        levelSelect.SetActive(false);
    }

    public void LevelLoader(int level)
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(level);
    }

    public void QuitClicked()
    {

    }

    //play clicked
    //level select clicked
    //home clicked
    //quit clciked

    //level numms
}
