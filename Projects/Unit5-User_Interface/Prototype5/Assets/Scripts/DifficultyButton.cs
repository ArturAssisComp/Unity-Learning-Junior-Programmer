using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DifficultyButton : MonoBehaviour
{
    //Attributes:
    private Button button;
    private GameManager gameManager;
    public int difficulty;

    // Start is called before the first frame update
    void Start()
    {
        //Get the components:
        this.button = this.gameObject.GetComponent<Button>();
        this.gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();


        this.button.onClick.AddListener(this.SetDifficulty);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SetDifficulty()
    {
        this.gameManager.StartGame(this.difficulty);
    }
}
