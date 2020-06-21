using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreScript : MonoBehaviour
{

    public static int scoreValue;
    public Text score;
    public Text highScore;

    void Start()
    {
        score = GetComponent<Text> ();
   
scoreValue = 0;

    }

    void Update()
    {

        score.text = "Score :" + scoreValue.ToString();

       // highScore.text = scoreValue.ToString();

        /*

                    if (Input.GetKeyDown(KeyCode.Y))
                {
                    scoreValue++;

                    SuperShotScript.fillSupershotBar++;

                }
                */
    }

    public int getNumber()
    {
        return scoreValue;
    }
}
