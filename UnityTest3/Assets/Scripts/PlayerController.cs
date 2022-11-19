using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class PlayerController : MonoBehaviour
{
    [SerializeField] float speed = 10;
    [SerializeField] float jumpPower = 200;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] Text scoreText;
    [SerializeField] AudioClip jumpSound;
    [SerializeField] AudioClip deathsound;
    [SerializeField] AudioClip itemSound;


    int point;
    Animator animator;
    AudioSource audioPlayer;
    BoxCollider2D bc;
    SpriteRenderer sr;
    Rigidbody2D rb;
    
    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        bc = GetComponent<BoxCollider2D>();
        audioPlayer = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();


    }
    bool GroundCheck()
    {
        return Physics2D.CapsuleCast(bc.bounds.center, bc.bounds.size,
        0f, 0f, Vector2.down, 0.5f, groundLayer);
    }
    // Update is called once per frame
    void Update()
    {
        float dir = Input.GetAxisRaw("Horizontal");
        transform.Translate(dir * speed * Time.deltaTime, 0, 0);
        rb.velocity = new Vector2(0, rb.velocity.y);
        if (dir < 0)
            sr.flipX = true;
        if (dir > 0)
            sr.flipX = false;
        bool jump = Input.GetButtonDown("Jump");
        if (jump && GroundCheck()) { 
            rb.AddForce(Vector3.up * jumpPower);
            audioPlayer.PlayOneShot(jumpSound, 1);
        }
        if (dir == 0)
            animator.SetBool("Run", false);
        else
            animator.SetBool("Run", true);
        scoreText.text = "Score: " + point;
       

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Fruit"))
        {
            Destroy(collision.gameObject);
            audioPlayer.PlayOneShot(itemSound, 1);
        }
        if (collision.gameObject.CompareTag("Trap"))
        {
            audioPlayer.PlayOneShot(deathsound, 1);
            Invoke("RestartLevel", 1);

        }

        if (collision.gameObject.name.Contains("Cherries"))
            point += 20;
    }
    private void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Fan"))
        {
            float distX = collision.gameObject.transform.position.x - gameObject.transform.position.x;
                   
            if (Mathf.Abs(distX) < 0.5f)
                distX = Mathf.Sign(distX) * 0.5f;
            float distY = Mathf.Abs(collision.gameObject.transform.position.y - gameObject.transform.position.y);
            float forceY = jumpPower / (7 * distY * distY);
            float forceX = -jumpPower / (4 * distX);
            if ((distY < 3) && (rb.velocity.y < 0))
                forceY *= 3;
            if (Mathf.Abs(forceY) > 450)
                forceY = Mathf.Sign(forceY) * 450;
            rb.AddForce(new Vector2(forceX, forceY));
        }
    }

}
