using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //Attributes:
    public List<GameObject> targets;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI gameOverText;
    public Button restartButton;
    public GameObject titleScreen;
    private float spawnPeriod;
    private float maxSpawnPeriod = 2f;
    private int score = 0;
    private bool isGameActive = true;
    public bool IsGameActive { get{ return this.isGameActive; } }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator SpawnTargets()
    {
        while(this.isGameActive)
        {
            yield return new WaitForSeconds(this.spawnPeriod);
            int targetIndex = Random.Range(0, targets.Count);
            Instantiate(targets[targetIndex]);
        }
    }

    public void AddToScore(int amount)
    {
        this.score += amount;
        if (this.score < 0) this.score = 0;
        this.scoreText.text = "Score: " + this.score;
    }

    public void GameOver()
    {
        this.isGameActive = false;
        this.gameOverText.gameObject.SetActive(true);
        this.restartButton.gameObject.SetActive(true);
    }

    public void RestartGame()
    {
        this.titleScreen.gameObject.SetActive(true);
        this.scoreText.gameObject.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    }

    public void StartGame(int difficulty)
    {
        this.isGameActive = true;

        this.titleScreen.gameObject.SetActive(false);
        this.scoreText.gameObject.SetActive(true);
        this.spawnPeriod = this.maxSpawnPeriod / difficulty;
        StartCoroutine(this.SpawnTargets());
        this.AddToScore(0);
    }
}
