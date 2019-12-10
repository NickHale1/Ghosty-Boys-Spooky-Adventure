using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerScore : MonoBehaviour
{

    private float timeLeft = 120;
    public int GummyBearsLeft = 3;
    public GameObject bearsLeftUI;
    public AudioSource playerNoises;
    public AudioClip pickupGummyNoise;
    public AudioClip winLevel;

    // Start is called before the first frame update
    void Start()
    {
        bearsLeftUI.gameObject.GetComponent<Text>().text = ("Bears Left: " + GummyBearsLeft);

    }

    // Update is called once per frame
    void Update()
    {
       // Debug.Log(timeLeft);
        timeLeft -= Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D trig)
    {
        if( trig.gameObject.tag == "gummyChildren")
        {
            CountScore();
            playerNoises.clip = pickupGummyNoise;
            playerNoises.Play();
            Destroy(trig.gameObject);
        }
        if (trig.gameObject.tag == "endZone")
        {
            Debug.Log("Reached endZone");
            if (this.GetComponent<PlayerScore>().GummyBearsLeft == 0)
            {

                StartCoroutine(PlayTuneAndNextLevel());
                
                    

            }
        }

        //trig.GetComponent<Rigidbody2D>().delete;

    }

    void CountScore()
    {
        GummyBearsLeft = GummyBearsLeft - 1;
        Debug.Log(GummyBearsLeft);
        bearsLeftUI.gameObject.GetComponent<Text>().text = ("Bears Left: " + GummyBearsLeft);
    }

    IEnumerator PlayTuneAndNextLevel()
    {
        
        playerNoises.clip = winLevel;
        playerNoises.Play();
        yield return new WaitForSeconds(2f);
        string sceneName = SceneManager.GetActiveScene().name;
        Debug.Log("Player is trying to leave " + sceneName);

        switch (sceneName)
        {
            case "Level_1":
                SceneManager.LoadScene("Level_2");
                break;
            case "Level_2":
                SceneManager.LoadScene("Level_3");
                break;
            case "Level_3":
                SceneManager.LoadScene("Game_Win");
                break;
        }

    }
}
