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
    private float spawnPeriod = 1.5f;
    private int score = 0;
    private bool isGameActive = true;
    public bool IsGameActive { get{ return this.isGameActive; } }

    // Start is called before the first frame update
    void Start()
    {
        this.isGameActive = true;

        StartCoroutine(this.SpawnTargets());
        this.AddToScore(0);
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
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    }

}
