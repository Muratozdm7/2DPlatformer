using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public float speed = 5f;
    public float jumpSpeed = 5f;
    private Rigidbody2D rb2;
    private bool isGrounded = false;

    private Animator anim;
    private Vector3 rotation;

    private CoinManager m;

    public GameObject gameOverPanel;
    [SerializeField] private GameObject cam;
    
    void Start()
    {
        rb2 = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        rotation = transform.eulerAngles;
        m = GameObject.FindGameObjectWithTag("Text").GetComponent<CoinManager>();
    }

    
    void Update()
    {
        float yatay = Input.GetAxis("Horizontal");

        if (yatay != 0)
        {
            anim.SetBool("isRunning", true);
        }
        else
        {
            anim.SetBool("isRunning", false);
        }

        if (yatay < 0)
        {
            transform.eulerAngles = rotation - new Vector3(0, 180, 0);
            transform.Translate(Vector2.right * -yatay * speed * Time.deltaTime);
        }
        if (yatay > 0)
        {
            transform.eulerAngles = rotation;
            transform.Translate(Vector2.right * yatay * speed * Time.deltaTime);
        }

        if (isGrounded == false)
        {
            anim.SetBool("isJumping", true);
        }
        else
        {
            anim.SetBool("isJumping", false);
        }

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb2.AddForce(Vector2.up * jumpSpeed, ForceMode2D.Impulse);
            isGrounded = false;
        }
        cam.transform.position = new Vector3(transform.position.x, 0, -10);
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(gameObject);
            gameOverPanel.SetActive(true);
        }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Coin"))
        {
            m.AddMoney();
            Destroy(other.gameObject);
        }
        if (other.gameObject.CompareTag("Spike"))
        {
            gameOverPanel.SetActive(true);
            Destroy(gameObject);
        }
        if (other.gameObject.CompareTag("Finish"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
