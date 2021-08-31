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
    public TextMeshProUGUI livesText;
    public TextMeshProUGUI gameOverText;
    public Button restartButton;
    public GameObject titleScreen;
    public GameObject pauseScreen;
    public GameObject mainCamera;
    public Slider volumeSlider;
    private float spawnPeriod;
    private float maxSpawnPeriod = 2f;
    private int score = 0;
    private int lives = 3;
    public bool isGamePaused = false;
    private  bool isGameActive = false;
    public bool IsGameActive { get{ return this.isGameActive; } }

    // Start is called before the first frame update
    void Start()
    {
        this.volumeSlider.onValueChanged.AddListener(delegate { this.ChangeVolume(); });
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P) && this.IsGameActive)
        {
            if (this.isGamePaused)
                this.ResumeGame();
            else
                this.PauseGame();
        }
        
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

    private void GameOver()
    {
        this.isGameActive = false;
        this.gameOverText.gameObject.SetActive(true);
        this.restartButton.gameObject.SetActive(true);
    }

    public void LoseLife()
    {
        if(this.IsGameActive)
        {
            this.lives--;
            if(this.lives >= 0)
                this.livesText.text = "Lives: " + this.lives;
        }
        if (this.IsGameActive && this.lives < 0)
            this.GameOver();
    }

    public void RestartGame()
    {
        this.titleScreen.gameObject.SetActive(true);
        this.scoreText.gameObject.SetActive(false);
        this.livesText.gameObject.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    }

    public void StartGame(int difficulty)
    {
        this.isGameActive = true;

        this.titleScreen.gameObject.SetActive(false);
        this.scoreText.gameObject.SetActive(true);
        this.livesText.gameObject.SetActive(true);
        this.spawnPeriod = this.maxSpawnPeriod / difficulty;
        StartCoroutine(this.SpawnTargets());
        this.AddToScore(0);
        this.livesText.text = "Lives: " + this.lives;
    }


    public void ChangeVolume()
    {
        this.mainCamera.GetComponent<AudioSource>().volume = this.volumeSlider.value;
    }

    private void PauseGame()
    {
        this.isGamePaused = true;
        Time.timeScale = 0;
        this.pauseScreen.SetActive(true);
    }

    private void ResumeGame()
    {
        this.isGamePaused = false;
        Time.timeScale = 1;
        this.pauseScreen.SetActive(false);
    }
}
