using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerScript : MonoBehaviour
{
    Animator towerAnim;
    public static bool isDead;
    AnimatorClipInfo[] m_CurrentClipInfo;
    SpriteRenderer sr;
    Rigidbody2D rb;
    public GameObject heart1T, heart2T, heart3T, gameOverText, restartButton, torretta1, torretta2, fire;
    public static int lives;
    float m_CurrentClipLength;
    public AudioSource hit, gameOverAudio;
 public float delay = 0f;
    // Start is called before the first frame update
    void Start()
    {        
        lives = 3;
        sr = GetComponent<SpriteRenderer>();
        heart1T = GameObject.Find("heart1T");
        heart2T = GameObject.Find("heart2T");
        heart3T = GameObject.Find("heart3T");
        fire = GameObject.Find("fire");
        heart1T.gameObject.SetActive(true);
        heart2T.gameObject.SetActive(true);
        heart3T.gameObject.SetActive(true);
        fire.gameObject.SetActive(false);
        hit = GetComponent<AudioSource>();
        towerAnim = gameObject.GetComponent<Animator>();
        m_CurrentClipInfo = towerAnim.GetNextAnimatorClipInfo(0);
        PauseMenu.GameIsOver = false;


        isDead = false;

        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
                //prove varie di debug
                /*
        if (Input.GetKeyDown(KeyCode.G))
        {
           
            lives = lives - 1;
            if (lives > 0)
            {
                hit.Play();
            
            } else if (lives == 0){
                gameOverAudio.Play();
            }

        
            sr.color = new Color(2, 0, 0);//set this object's red color to 200 percent
        }

        */
        
        switch (lives)
        {
            case 2:
                sr.color = Color.Lerp(sr.color, Color.white, Time.deltaTime / 0.3f);//slowly linear interpolate. takes about 3 seconds to return to white
                heart3T.gameObject.SetActive(false);
                torretta1.SetActive(false);
                break;
            case 1:
                sr.color = Color.Lerp(sr.color, Color.white, Time.deltaTime / 0.3f);//slowly linear interpolate. takes about 3 seconds to return to white
                heart2T.gameObject.SetActive(false);
                torretta2.SetActive(false);
                break;
            case 0:
                 //al prossimo frame aggiornerà animazione
                heart1T.gameObject.SetActive(false);
                fire.SetActive(true);
                isDead = true;
                //Debug.Log("Morto");
                break;
        }


    }



  void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag.Equals("Enemy"))
        {

            Destroy(col.gameObject);
            sr.color = new Color(2, 0, 0);//set this object's red color to 200 percent
            lives = lives - 1;
            if (lives != 0)
            {
                hit.Play();
            } else {
                gameOverAudio.Play();
            }
            
        }
    }

}