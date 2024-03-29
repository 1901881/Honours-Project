using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class EnemyCounter : MonoBehaviour
{
    GameObject[] enemies;
    public TextMeshProUGUI enemyCountText;
    public TextMeshProUGUI enemyCountMaxText;
    float maxEnemies;

    // Start is called before the first frame update
    void Start()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        maxEnemies = enemies.Length;
    }

    // Update is called once per frame
    void Update()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");

        enemyCountText.text = enemies.Length.ToString();
        enemyCountMaxText.text = maxEnemies.ToString();

        if(enemies.Length == 0)
        {
            if(SceneManager.GetActiveScene().name == "Lvl_5_StressBot" || SceneManager.GetActiveScene().name == "Lvl_5_BasicBot")
            {
                StartCoroutine(GameObject.FindObjectOfType<HUDManager>().YouWin());
            }
            else
            {
                StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
            }
        }
    }

    IEnumerator LoadLevel(int levelNum)
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(levelNum);
    }
}
