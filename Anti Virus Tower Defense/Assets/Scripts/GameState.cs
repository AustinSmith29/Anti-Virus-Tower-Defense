using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameState : MonoBehaviour {

    public static int towerLives = 10;
    public static int score = 0;
    public static int currency = 100;
    public static int wave = 1;
    public static bool wave_over = false;

    public Text livesText;
    public Text scoreText;
    public Text currencyText;
    public Text waveText;
    public Button nextWaveButton;
    public GameObject GameOverMenu;
    public GameObject victoryMessage;
    public Button mainMenuButton;

    EnemyManager enemyManagerScript;
	// Use this for initialization
	void Start () {
        enemyManagerScript = GameObject.Find("EnemyManager").GetComponent<EnemyManager>();
        enemyManagerScript.Init();

        Button btn_nextWave = nextWaveButton.GetComponent<Button>();
        btn_nextWave.onClick.AddListener(clickedNextWave);

        Button btn_mainMenu = mainMenuButton.GetComponent<Button>();
        btn_mainMenu.onClick.AddListener(goToMainMenu);

        GameOverMenu.SetActive(false);
        victoryMessage.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {

        if (towerLives <= 0)
        {
            GameOverMenu.SetActive(true);
        }

        if (enemyManagerScript.isOutOfWaves() && towerLives > 0)
        {
            victoryMessage.SetActive(true);
        }

        livesText.text = towerLives.ToString();
        scoreText.text = string.Format("<color=cyan>Score:</color> {0}", score);
        currencyText.text = string.Format("<color=lime>$</color> {0}", currency);
        waveText.text = string.Format("Wave: <color=cyan>{0}</color>", wave);
    }

    void clickedNextWave()
    {
        if (enemyManagerScript.waveOver())
        {
            enemyManagerScript.nextWave();
            wave++;
            Debug.Log("Wave Over. Starting Next Wave.");
        }
    }

    void goToMainMenu()
    {
        GameOverMenu.SetActive(false);
        victoryMessage.SetActive(false);
        SceneManager.LoadScene("Menu");
    }
}
