using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : MonoBehaviour
{
    public float speed;
    public float distance;
    public AudioSource enemyNoises;
    public AudioClip enemyDeath;

    private bool movingRight = true;

    public Transform groundDetection;
    void Update()
    {

        transform.Translate(Vector2.right * speed * Time.deltaTime);

        RaycastHit2D groundInfo = Physics2D.Raycast(groundDetection.position, Vector2.down, 2f);
        if (groundInfo.collider == false)
        {
            if (movingRight == true)
            {
                transform.eulerAngles = new Vector3(0, -180, 0);
                movingRight = false;
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                movingRight = true;
            }
        }
    }
    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        //Debug.Log("Collision detected, sound should play");
       // deathNoise.Play();
       // Debug.Log("sound played");
        StartCoroutine(waitAndDie());
        //Destroy(gameObject);
    }
    IEnumerator waitAndDie()
    {
        Debug.Log("death detected");
        enemyNoises.clip = enemyDeath;
        enemyNoises.Play();
        yield return new WaitForSeconds(.3f);
        Debug.Log("Begin play Death");


        Destroy(gameObject);
        //my code here after 3 seconds
    }
}

