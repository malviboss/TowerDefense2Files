using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolemR : MonoBehaviour
{

    AnimatorClipInfo[] m_CurrentClipInfo;
    public float moveSpeed = 50f;
    public bool isAttacking, isDead;
 public float spriteBlinkingTimer = 0.0f;
 public float spriteBlinkingMiniDuration = 0.1f;
 public float spriteBlinkingTotalTimer = 0.0f;
 public float spriteBlinkingTotalDuration = 0.2f;
    Animator anim;
 public bool startBlinking = false;
    public float m_CurrentClipLength;
    public AudioSource hitSource;
        public bool bySuperShot;

SpriteRenderer sr;

    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        m_CurrentClipInfo = anim.GetCurrentAnimatorClipInfo(0);
        m_CurrentClipLength = m_CurrentClipInfo[0].clip.length;
 
        isAttacking = false;
        isDead = false;
        sr = GetComponent<SpriteRenderer>();
        hitSource = GetComponent<AudioSource>();
    }


    void Update()
    {
        moveSpeed = levelSpeed(ScoreScript.scoreValue);

        if (startBlinking == true)
        {
            SpriteBlinkingEffect();
        }

        if (isAttacking == true)
        {
            //cambia parametro del corrispondente Animator
            anim.SetBool("isAttacking", true);
            //fai cose per infliggere danno al player o alla torre
        }
        //muoviti(c'è un modo migliore per farlo senza sdoppiare script(?)... ma per ora va bene)
        transform.Translate(Vector2.left * moveSpeed * Time.deltaTime);

        if (isDead)
        {

            dead();
            isDead= false;

        }

             if (Input.GetKeyDown(KeyCode.B))
        {
            isAttacking = false;
            isDead = true;

        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {

        if (col.gameObject.name == "Tower")
        {
           
            //fermati
            moveSpeed = 0f;
            isAttacking = true;

        }
        if ((col.gameObject.tag.Equals("Player")))
        {
           
            moveSpeed = 0f;
           
            isAttacking = true;
           
            
                
              //  KnightScriptL.lives = KnightScriptL.lives - 1; 
                if (KnightScriptR.isDead == true){
                    isAttacking = false;
                       moveSpeed = levelSpeed(ScoreScript.scoreValue);
                }
        
            
            
        }
    }


    public void dead()
    {
        anim.SetBool("isDead", true);
       
    
        startBlinking = true;
        moveSpeed = 0f;
        if (!bySuperShot)
        {
            hitSource.Play();
            bySuperShot = false;
        }
        hitSource.Play();
        ScoreScript.scoreValue++;
        if (!bySuperShot)
            SuperShotScript.fillSupershotBar++;
        Destroy(gameObject, 0.3f);


    }



    private void SpriteBlinkingEffect()
    {
        spriteBlinkingTotalTimer += Time.deltaTime;
        if (spriteBlinkingTotalTimer >= spriteBlinkingTotalDuration)
        {
            startBlinking = false;
            spriteBlinkingTotalTimer = 0.0f;
            this.gameObject.GetComponent<SpriteRenderer>().enabled = true;   // according to 
                                                                             //your sprite
            return;
        }

        spriteBlinkingTimer += Time.deltaTime;
        if (spriteBlinkingTimer >= spriteBlinkingMiniDuration)
        {
            spriteBlinkingTimer = 0.0f;
            if (this.gameObject.GetComponent<SpriteRenderer>().enabled == true)
            {
                this.gameObject.GetComponent<SpriteRenderer>().enabled = false;  //make changes
            }
            else
            {
                this.gameObject.GetComponent<SpriteRenderer>().enabled = true;   //make changes
            }
        }
    }

    private float levelSpeed(int val){
        float speed =0.0f;
        if (val <5)
        {
            //livello 1
            speed = 70f;
        } else
        if (val >= 5 && val < 10)
        {
            //livello 1
            speed = 90f;
        }
        else if (val >= 10 && val < 30)
        {
            //livello 2
            speed = 110f;
        }
        else if (val >= 30 && val < 60)
        {
            //livello 3
             speed = 130f;
        }
        else
        {
            speed = 200f;
            //livello finale
        }
        return speed;
    }
}
