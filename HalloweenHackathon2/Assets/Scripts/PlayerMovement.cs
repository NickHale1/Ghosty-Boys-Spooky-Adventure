using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public Animator animator;
    public int playerSpeed = 10;
    private bool facingRight = true;
    public int playerJumpPower = 6;
    private float moveX;
    private bool isGrounded;
    public AudioSource playerNoises;
    public AudioClip jump;
    public AudioClip pickup;
    public AudioClip dieNoise;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetFloat("Speed", Mathf.Abs(moveX));
        if (Input.GetButtonDown("Fire1"))
        {
            animator.SetBool("Shoot", true);
        } else if (Input.GetButtonUp("Fire1"))
        {
            animator.SetBool("Shoot", false);
        }
        PlayerMove();
    }

    void PlayerMove()
    {
        //CONTROLS
        moveX = Input.GetAxis("Horizontal");
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            Jump();
        }
        //ANIMATIONS

        //PLAYERDIRECTION
        if(moveX < 0.0f && facingRight == true)
        {
            FlipPlayer();
        }
        else if(moveX > 0.0f && facingRight == false)
        {
            FlipPlayer(); 
        }
        //PHYSICS
        gameObject.GetComponent<Rigidbody2D>().velocity =
            new Vector2(moveX * playerSpeed, gameObject.GetComponent<Rigidbody2D>().velocity.y);
    }

    void Jump()
    {
        //JUMPING CODE
        GetComponent<Rigidbody2D>().AddForce(Vector2.up * playerJumpPower);
        
        isGrounded = false;
        playerNoises.clip = jump;
        playerNoises.Play();
    }

    void FlipPlayer()
    {
        facingRight = !facingRight;
        transform.Rotate(0f, 180f, 0f);
        //Vector2 localScale = gameObject.transform.localScale;
        //localScale.x *= -1;
        //transform.localScale = localScale;
    }

    

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("player has collided with " + collision.collider.name);
        if(collision.gameObject.tag == "ground")
        {
            isGrounded = true;

        }
        if(collision.gameObject.tag == "deathZone")
        {
            Debug.Log("player has died");

            StartCoroutine(WaitAndDie());
            
        }
        
    }

    IEnumerator WaitAndDie()
    {
        Debug.Log("death detected");
        playerNoises.clip = dieNoise;
        playerNoises.Play();
        yield return new WaitForSeconds(2f);
        Debug.Log("Begin play Death");
        Scene thisScene = SceneManager.GetActiveScene();


        SceneManager.LoadScene(thisScene.name);

        //Destroy(gameObject);
        //my code here after 3 seconds
    }

}
