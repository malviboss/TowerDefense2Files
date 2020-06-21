using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SuperShotScript : MonoBehaviour
{
    public bool superShotReady;
    public static float fillSupershotBar;
    GameObject[] playersObjects, enemies;
    Image timerBar;
    public GameObject poster;
    public float maxTime = 20f;
    float score ;
    float timeLeft;
        public AudioSource upAudio;

    // Start is called before the first frame update
    void Start()
    {
        
        timerBar = GetComponent<Image>();
        superShotReady= false;
        timeLeft = maxTime;
        poster.gameObject.SetActive(false);

    
    }

    // Update is called once per frame
    void Update()
    {
        float score;
        playersObjects = GameObject.FindGameObjectsWithTag("Player");
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        if (playersObjects.Length < 2)
        {
            fillSupershotBar = 0f;

        }
        else
        {
            if (timerBar.fillAmount == 1 && ScoreScript.scoreValue!=0)
            {
                superShotReady = true;

            }
            if (Input.GetKeyDown(KeyCode.X) && superShotReady)
            {
                StartCoroutine(Flash());
              
           
                
                foreach (GameObject enemy in enemies)
                {
                    Debug.Log(enemy.name);
                    if (enemy.name == "GolemL(Clone)")
                    {
                        enemy.GetComponent<GolemL>().bySuperShot = true;
                        enemy.GetComponent<GolemL>().isDead = true;
                    }
                    else if (enemy.name == "GolemR(Clone)")
                    {
                        enemy.GetComponent<GolemR>().bySuperShot = true;
                        enemy.GetComponent<GolemR>().isDead = true;
                    }

                }
                
                superShotReady = false;
                fillSupershotBar = 0f;
                             
            }
            
            score = fillSupershotBar;

            timerBar.fillAmount = score / 10;

        }
    }

    IEnumerator Flash()
    {
        upAudio.Play();
        for (int i = 0 ; i < 3;i++){
            poster.gameObject.SetActive(true);
            yield return new WaitForSeconds(0.1f);
            poster.gameObject.SetActive(false);
            yield return new WaitForSeconds(0.1f);
        }




    }



 
}
