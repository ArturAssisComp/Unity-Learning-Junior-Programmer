using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    //Attributes:
    public List<GameObject> targets;
    private float spawnPeriod = 1.5f;
    public TextMeshProUGUI scoreText;
    private int score = 0;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(this.SpawnTargets());
        this.AddToScore(0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator SpawnTargets()
    {
        while(true)
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

}
