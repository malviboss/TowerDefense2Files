using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightScriptR : MonoBehaviour
{


    Animator player_anim;

    AnimatorClipInfo[] m_CurrentClipInfo;
    Rigidbody2D rb;
public AudioSource jumpSource, hitSource, deadSource;
    Selected sel;
    float m_CurrentClipLength;
    bool isGrounded;
    public static bool isAttacking, isDead,  isActive, isJumping, isHurt;
    public bool isMoving;
    public Transform isGroundedChecker;
    public float checkGroundRadius;
    public LayerMask groundLayer;
    public float moveSpeed;
    public GameObject heart1R, heart2R, heart3R, bar;
    public int lives = 3;
    public float jump;
    int playerLayer, enemyLayer;
    float lowJumpMultiplier = 40;
    float fallMultiplier = 100f;
  
    Color color;
     SpriteRenderer sr, sh1, sh2, sh3;
    Renderer rend;

    void Start()
    {
        heart1R = GameObject.Find("heart1R");
        heart2R = GameObject.Find("heart2R");
        heart3R = GameObject.Find("heart3R");
        bar = GameObject.Find("activeR");
        heart1R.gameObject.SetActive(true);
        heart2R.gameObject.SetActive(true);
        heart3R.gameObject.SetActive(true);
        bar.gameObject.SetActive(true);

        sr = GetComponent<SpriteRenderer>();
        isGrounded = true;
        sel = gameObject.GetComponent<Selected>();
        player_anim = gameObject.GetComponent<Animator>();
        m_CurrentClipInfo = player_anim.GetCurrentAnimatorClipInfo(0);
        m_CurrentClipLength = m_CurrentClipInfo[0].clip.length;
        // Debug.Log(m_CurrentClipInfo);
        //  Debug.Log(m_CurrentClipLength);
        isAttacking = false;
        isDead = false;
        isMoving = false;
        isJumping = false;
        isHurt = false;
        rb = gameObject.GetComponent<Rigidbody2D>();

        sh1 = heart1R.gameObject.GetComponent<SpriteRenderer>();
        sh2 = heart2R.gameObject.GetComponent<SpriteRenderer>();
        sh3 = heart3R.gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
      switch (lives)
        {
            case 2:
                heart3R.gameObject.SetActive(false);
                if (isActive)
                {
                    sr.color = Color.Lerp(sr.color, Color.white, Time.deltaTime / 0.3f);//slowly linear interpolate. takes about 3 seconds to return to white

                }
                else
                {
                    sr.color = Color.Lerp(sr.color, Color.gray, Time.deltaTime / 0.3f);//slowly linear interpolate. takes about 3 seconds to return to white

                }

                isHurt = false;
                break;
            case 1:
                heart2R.gameObject.SetActive(false);
                if (isActive)
                {
                    sr.color = Color.Lerp(sr.color, Color.white, Time.deltaTime / 0.3f);//slowly linear interpolate. takes about 3 seconds to return to white

                }
                else
                {
                    sr.color = Color.Lerp(sr.color, Color.gray, Time.deltaTime / 0.3f);//slowly linear interpolate. takes about 3 seconds to return to white

                }
                isHurt = false;
                break;
            case 0:
                heart1R.gameObject.SetActive(false);
                isHurt = false;    //cambia parametro in animator per attivare animazione di morte
                isDead = true; //al prossimo frame aggiornerà animazione

                Debug.Log("Morto");
                sel.isDead = true;

                break;    
        }
        isActive = sel.isActive;
        //queste azioni avvengono solo se isActive= true
        if (isActive == true)
        {
            //controllo cambio giocatore

            //solo per testare
            GetComponent<Renderer>().material.color = Color.white;
            sh1.material.color = Color.white;
            sh2.material.color = Color.white;
            sh3.material.color = Color.white;
            bar.gameObject.SetActive(true);
            //movimento
            Move();
            //salto;
            JumpPolish(fallMultiplier, lowJumpMultiplier);
            Jump(280);
            //Controlla se si sta muovendo o no
            if (isMoving == true)
                player_anim.SetBool("isMoving", true);
            else
                player_anim.SetBool("isMoving", false);

            //Controlla se sta attaccando o no
            if (isAttacking == true)
                player_anim.SetBool("isAttacking", true);
            else
                player_anim.SetBool("isAttacking", false);
            if (isJumping == true)
                player_anim.SetBool("isJumping", true);
            else
                player_anim.SetBool("isJumping", false);
        }
        else
        {

            GetComponent<Renderer>().material.color = Color.grey;
            sh1.material.color = Color.grey;
            sh2.material.color = Color.grey;
            sh3.material.color = Color.grey;
            bar.gameObject.SetActive(false);
            JumpPolish(fallMultiplier, lowJumpMultiplier);
            rb.velocity = new Vector2(0, rb.velocity.y);
            player_anim.SetBool("isMoving", false);
            player_anim.SetBool("isJumping", false);
            
            isMoving = false;

        }

        if (isDead == true)
        {
            isHurt = false;
           
            sel.isActive = false;
            player_anim.SetBool("isDead", true);
 
            Destroy(gameObject, m_CurrentClipLength);
             
        }


        //prove varie di debug
        if (Input.GetKeyDown(KeyCode.P))
        {
            lives=lives-1;
            Debug.Log("LIVES " + lives);
            if (lives != 0)
                hitSource.Play();
            else
                deadSource.Play();
            
            sr.color = new Color(2,0,0);//set this object's red color to 200 percent
            Debug.Log("Vite scese a " + lives + "");
        }


    }

    void Move()
    {
        moveSpeed = 80f;

        float x = Input.GetAxisRaw("Horizontal");
        float move = x * moveSpeed;
        rb.velocity = new Vector2(move, rb.velocity.y);

        if ((x != 0))
        {
            isMoving = true;
            if (x < 0)
            {
                //se maggiore di 0 mi sto muovendo verso destra e devo girare lo sprite
                sr.flipX = false;
            }
            else
            {
                //mi sto muovendo verso sinistra
                sr.flipX = true;
            }
        }
        else
        {
            isMoving = false;
             rb.velocity = new Vector2(0, rb.velocity.y);
        }
    }

    void JumpPolish(float fallMult, float lowJump)
    {
        if (rb.velocity.y < 0)
        {
            //se ho velocità y negativa vuol dire mi trovo nella fase di discesa del salto
            rb.velocity += Vector2.up * Physics2D.gravity * (fallMultiplier - 1) * Time.deltaTime;
        }
        else if (rb.velocity.y > 0 && !Input.GetKeyDown(KeyCode.Space))
        {
            //mi trovo nella fase di salita del salto
            rb.velocity += Vector2.up * Physics2D.gravity * (lowJumpMultiplier - 1) * Time.deltaTime;
           
        }
    }
    void Jump(int jumpVelocity)
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            jumpSource.Play();
            Debug.Log("ENTRA IN SALTO");
            jump = jumpVelocity;
            rb.velocity = new Vector2(rb.velocity.x, jump);

            isGrounded = false;
            isJumping = true;
        }
       
    }


    void OnCollisionEnter2D(Collision2D col)
    {
        if ((col.gameObject.name == "ground") || (col.gameObject.name == "Tower"))
        {
            //ho toccato il terreno, posso saltare di nuovo
            isGrounded = true;
            isJumping = false;
        }

        if (col.gameObject.tag == "Enemy")
        {
            if (isGrounded) 
            {
                sr.color = new Color(2, 0, 0);//set this object's red color to 200 percent
                lives = lives - 1;
                if (lives != 0)
                    hitSource.Play();
                else
                    deadSource.Play();
            }
            else
            {

                    JumpPolish(10, 10);

                    jump = 200;
                    rb.velocity = new Vector2(rb.velocity.x, jump);              
                    isGrounded = false;
                    isJumping = true;
                  
                    if (col.gameObject.name.Equals("GolemL(Clone)"))
                    {
                        col.gameObject.GetComponent<GolemL>().isDead = true;
                    }
                    else
                    {
                        col.gameObject.GetComponent<GolemR>().isDead = true;
                    }
                
            }
        }
     //   }


        if (col.gameObject.name == "Tower")
          {
              Debug.Log("Hit Tower");
              //fermati
              isMoving=false;
              moveSpeed = 0f;

          }


    }



}
