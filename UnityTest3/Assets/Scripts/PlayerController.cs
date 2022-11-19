using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerController : MonoBehaviour
{
    [SerializeField] float speed = 10;
    [SerializeField] float jumpPower = 200;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] Text scoreText;


    BoxCollider2D bc;
    SpriteRenderer sr;
    Rigidbody2D rb;
    int point;
    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        bc = GetComponent<BoxCollider2D>();

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
        if (dir < 0)
            sr.flipX = true;
        if (dir > 0)
            sr.flipX = false;
        bool jump = Input.GetButtonDown("Jump");
        if (jump && GroundCheck())
            rb.AddForce(Vector3.up * jumpPower);
        scoreText.text = "Score: " + point;
        scoreText.text = "Score: ";

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Fruit"))
        {
            Destroy(collision.gameObject);
        }
        
        if (collision.gameObject.name.Contains("Cherries"))
            point += 20;
    }

}
