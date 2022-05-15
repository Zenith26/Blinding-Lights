using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Score : MonoBehaviour
{

    public  static int scoreValue = 0;
    public TextMeshProUGUI score;

    // Start is called before the first frame update
    void Start()
    {
        score.text = "Score: " + scoreValue.ToString();
        
    }
   
    private void Update()
    {
        score.text = "Score: " + scoreValue.ToString();
    }
}
