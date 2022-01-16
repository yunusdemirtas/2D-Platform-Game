using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D rgb;
    Vector3 velocity;
    public Animator animator;
    public TextMeshProUGUI playerScoreText;
    public float health;
    public Transform Bullet, Muzzle;
    public bool dead = false;
    public float BulletSpeed;
    public int score;
    float speedAmount = 10f;
    float jumpAmount = 9f;
    // Start is called before the first frame update
    void Start()
    {
        rgb = GetComponent<Rigidbody2D>();
        score = 0;
        
    }

    // Update is called once per frame
    void Update()
    {
        playerScoreText.text = score.ToString();
        velocity = new Vector3(Input.GetAxis("Horizontal"), 0f);
        transform.position += velocity * speedAmount * Time.deltaTime;
        animator.SetFloat("Speed", Mathf.Abs(Input.GetAxis("Horizontal")));

        if (Input.GetMouseButtonDown(0))
        {
            shootBullet();
        }


        if (Input.GetButtonDown("Jump")&& Mathf.Approximately(rgb.velocity.y,0))
        {
            rgb.AddForce(Vector3.up * jumpAmount, ForceMode2D.Impulse);
            animator.SetBool("IsJumping", true);
        }
        if(animator.GetBool("IsJumping")&& Mathf.Approximately(rgb.velocity.y, 0))
        {
            animator.SetBool("IsJumping", false);
        }

        if (Input.GetAxisRaw("Horizontal") == -1)
        {
            transform.rotation = Quaternion.Euler(0f, 180f, 0f);
        }
        else if(Input.GetAxisRaw("Horizontal") == 1)
        {
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        }
    }
    public void getDamage(float damage)
    {
        if (health-damage >= 0)
        {
            health -= damage;
        }
        else
        {
            health = 0;
        }
        amIdead();
    }
    void amIdead()
    {
        if (health == 0)
        {
            
            dead = true;
            Destroy(gameObject);
        }
    }

    void shootBullet()
    {
        Transform tempBullet;
        tempBullet = Instantiate(Bullet, Muzzle.position, Quaternion.identity);
        tempBullet.GetComponent<Rigidbody2D>().AddForce(Muzzle.forward * BulletSpeed);
        
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag=="hazine")
        {
            SceneManager.LoadScene(2);
        }
        if (collision.tag == "Respawn")
        {
            SceneManager.LoadScene(0);
        }
    }

    


}
