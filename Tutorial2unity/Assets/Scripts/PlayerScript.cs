using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerScript : MonoBehaviour
{

    private Rigidbody2D rd2d;
    public float speed;
    private int scoreValue = 0;
    private int livesValue;

    float inputHorizontal;
    float inputVertical;
    bool facingRight = true;
    
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI livesText;
    public GameObject WinTextObject;
    public GameObject LoseTextObject;

    public AudioSource musicSource;
    public AudioClip musicClipOne;
    public AudioClip winMusic;
    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        rd2d = GetComponent<Rigidbody2D>();
        scoreText.text = "Score:" + scoreValue.ToString();

        rd2d = GetComponent<Rigidbody2D>();
        livesValue = 3;

        musicSource.clip = musicClipOne;
        musicSource.Play();

        SetCountText();
        WinTextObject.SetActive(false);

        SetCountText();
        LoseTextObject.SetActive(false);

        anim = GetComponent<Animator>();
    }

    void SetCountText()
    {
        scoreText.text = "Score:" + scoreValue.ToString();
        if (scoreValue >=8)
        {
            WinTextObject.SetActive(true);
            musicSource.clip = winMusic;
            musicSource.Play();
        
            
        }

        scoreText.text = "Score: " + scoreValue.ToString();
        if (scoreValue == 4)
        {
            livesValue = 3;
            transform.position = new Vector2(42f, 0.5f);
        }

        livesText.text = "Lives: " + livesValue.ToString();
        if (livesValue == 0)
        {
            LoseTextObject.SetActive(true);
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float hozMovement = Input.GetAxis("Horizontal");   
        float verMovement = Input.GetAxis("Vertical");

        inputHorizontal = Input.GetAxisRaw("Horizontal");
        inputVertical = Input.GetAxisRaw("Vertical");

        rd2d.AddForce(new Vector2(hozMovement * speed, verMovement * speed));


        if (inputHorizontal > 0 && !facingRight)
        {
            Flip();
        }
        
        if (inputHorizontal < 0 && facingRight)
        {
            Flip();
        }

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            anim.SetInteger("State", 2);
            
        }
        if (Input.GetKeyUp(KeyCode.W))
        {
            anim.SetInteger("State", 0);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            anim.SetInteger("State", 1);
            
        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            anim.SetInteger("State", 0);
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            anim.SetInteger("State", 1);
            
        }
        if (Input.GetKeyUp(KeyCode.A))
        {
            anim.SetInteger("State", 0);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "Coin")
        {
            scoreValue += 1;
            SetCountText();
            Destroy(collision.collider.gameObject);
        }

        if (collision.collider.tag == "Enemy")
        {
            Destroy(collision.collider.gameObject);
            livesValue = livesValue - 1;

            SetCountText();
        }
    }
    
    private void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.collider.tag == "Ground")
        {
            if(Input.GetKey(KeyCode.W))
            {
                rd2d.AddForce(new Vector2(0, 3), ForceMode2D.Impulse);
            }
        }
    }


    void Flip()
    {
        Vector3 currentScale = gameObject.transform.localScale;
        currentScale.x *= -1;
        gameObject.transform.localScale = currentScale;

        facingRight = !facingRight;
    }
}

