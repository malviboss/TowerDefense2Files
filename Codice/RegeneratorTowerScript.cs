using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RegeneratorTowerScript : MonoBehaviour
{
    Image timerBar;
    public float maxTime;
    float timeLeft;
     public GameObject heart1T, heart2T, heart3T, torretta1, torretta2;
     public AudioSource regAudio;
    
    // Start is called before the first frame update
    void Start()
    {
        timerBar = GetComponent<Image>();
        timeLeft = maxTime;

        

    }

    // Update is called once per frame
    void Update()
    {
        if (TowerScript.lives < 3)
        {

            if (timeLeft > 0)
            {
                timeLeft -= Time.deltaTime;
                timerBar.fillAmount = timeLeft / maxTime;
            }
            else
            {

                if (TowerScript.lives == 2)
                {
                    heart3T.SetActive(true);
                    torretta1.SetActive(true);

                }
                else if (TowerScript.lives == 1)
                {
                    heart2T.SetActive(true);
                    torretta2.SetActive(true);
                }
                regAudio.Play();

                TowerScript.lives++;

                timeLeft = maxTime;

            }
        }

    }
}
