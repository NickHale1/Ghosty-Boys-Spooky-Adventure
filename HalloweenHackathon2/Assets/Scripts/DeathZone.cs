using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathZone : MonoBehaviour
{
    private void OnCollisionEnter(Collision dead)
    {
        if (dead.gameObject.name == "DeathZone")
        {
            Scene thisScene = SceneManager.GetActiveScene();

            SceneManager.LoadScene(thisScene.name);
        }
    }



}
