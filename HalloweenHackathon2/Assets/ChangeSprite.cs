using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSprite : MonoBehaviour
{
    public Sprite spGhostboyShoot;
    public Sprite Ghostboy;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            this.GetComponent<SpriteRenderer>().sprite = spGhostboyShoot;
        }
        else
        {
            this.GetComponent<SpriteRenderer>().sprite = Ghostboy;
        }
    }
}
