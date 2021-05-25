using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public PlayerScript player;
    public Text scoreText;
    public GameObject enemy;
    public static int score;
    public Image hpBar;
    public static float hp = 1;
    public GameObject gameOverPanel;

    private bool enemyCheck = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = "Score : " + score;
        hpBar.fillAmount = hp;
        if (enemyCheck)
        {
            StartCoroutine("EnemyInstantiate");
        }

        if(hpBar.fillAmount <= 0)
        {
            gameOverPanel.SetActive(true);
            Time.timeScale = 0;
        }
    }

    IEnumerator EnemyInstantiate()
    {
        enemyCheck = false;
        Instantiate(enemy, new Vector3(Random.Range(-45, 45), 1, Random.Range(-45, 45)), Quaternion.identity);
        yield return new WaitForSeconds(1);
        enemyCheck = true;
    }

    public static void Hit()
    {
        hp -= 0.1f;
    }

    public void Restart()
    {
        hp = 1f;
        score = 0;
        gameOverPanel.SetActive(false);

        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
