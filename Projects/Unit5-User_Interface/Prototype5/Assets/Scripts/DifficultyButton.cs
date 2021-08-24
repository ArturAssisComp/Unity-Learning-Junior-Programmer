using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DifficultyButton : MonoBehaviour
{
    //Attributes:
    private Button button;
    // Start is called before the first frame update
    void Start()
    {
        this.button = this.gameObject.GetComponent<Button>();
        this.button.onClick.AddListener(this.SetDifficulty);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SetDifficulty()
    {
        Debug.Log(this.gameObject.name + " was clicked!");
    }
}
