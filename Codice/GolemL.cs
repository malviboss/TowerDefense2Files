using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolemL : MonoBehaviour
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
    public bool bySuperShot;
    public AudioSource hitSource;

SpriteRenderer sr;
    // Start is called before the first frame update
    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        m_CurrentClipInfo = anim.GetCurrentAnimatorClipInfo(0);
        m_CurrentClipLength = m_CurrentClipInfo[0].clip.length;
      //  Debug.Log(m_CurrentClipInfo);
       // Debug.Log(m_CurrentClipLength);
        isAttacking = false;
        isDead = false;
        sr = GetComponent<SpriteRenderer>();
        hitSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
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
        transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);

        if (isDead)
        {
            
            
            dead();
            isDead = false;
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
            //quando colpisci il giocatore
        //    Debug.Log("Hit player");
            //fermati
            moveSpeed = 0f;
            //fai cose per avviare animazione di attacco
            isAttacking = true;
           
            //quando player muore deve ricominciare a muoversi
            //settando isAttacking a false

                
              //  KnightScriptL.lives = KnightScriptL.lives - 1; 
                if (KnightScriptL.isDead == true){
                    isAttacking = false;
                       moveSpeed = levelSpeed(ScoreScript.scoreValue);
                }
        
            
            
        }
    }


    public void dead()
    {
        anim.SetBool("isDead", true);
        //distrugge l'oggetto dopo aver aspettato il numero di secondi
        //necessari a mostrare l'animazione
        Debug.Log("MORTO L");
        startBlinking = true;
        moveSpeed = 0f;
        if (!bySuperShot)
        {
            hitSource.Play();
            bySuperShot = false;
        }
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
