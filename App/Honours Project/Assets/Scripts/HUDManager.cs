using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HUDManager : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject overlayHUD;
    [SerializeField] GameObject levelSelect;
    [SerializeField] GameObject gameOver;
    [SerializeField] GameObject gameOverText;
    [SerializeField] GameObject youWinText;

    // Start is called before the first frame update
    void Start()
    {
        pauseMenu.SetActive(false);
        overlayHUD.SetActive(true);
        levelSelect.SetActive(false);
        gameOver.SetActive(false);
        gameOverText.SetActive(false);
        youWinText.SetActive(false);
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
        gameOver.SetActive(false);
        Time.timeScale = 0;
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
        overlayHUD.SetActive(true);
        levelSelect.SetActive(false);
        gameOver.SetActive(false);
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
        gameOver.SetActive(false);
    }

    public void BackArrowClicked()
    {
        pauseMenu.SetActive(true);
        levelSelect.SetActive(false);
        gameOver.SetActive(false);
    }

    public void LevelLoader(int level)
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(level);
    }

    public void QuitClicked()
    {
        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #endif
                Application.Quit();
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public IEnumerator GameOver()
    {
        yield return new WaitForSeconds(0.4f);
        gameOverText.SetActive(true);
        yield return new WaitForSeconds(1.75f);
        gameOverText.SetActive(false);

        pauseMenu.SetActive(false);
        overlayHUD.SetActive(false);
        levelSelect.SetActive(false);
        gameOver.SetActive(true);
    }

    public IEnumerator YouWin()
    {
        yield return new WaitForSeconds(0.4f);
        youWinText.SetActive(true);
        yield return new WaitForSeconds(1.75f);
        youWinText.SetActive(false);

        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(1);
    }
}
