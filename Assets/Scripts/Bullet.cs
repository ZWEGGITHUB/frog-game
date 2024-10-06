using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float bulletSpeed;

    private Rigidbody2D theRB;

    public bool inverse;

    public GameObject bulletEffect;

    // Start is called before the first frame update
    void Start()
    {
        theRB = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(inverse == true)
        {
            theRB.velocity = new Vector2(-bulletSpeed * transform.localScale.x, -4);
        } else {
            theRB.velocity = new Vector2(bulletSpeed * transform.localScale.x, -4);
        }

    }

    void OnTriggerEnter2D(Collider2D other) 
    {      
        //if(other.tag == "Bullet")
        //{
        //    BulletCollisionSound
        //}

        if(other.tag == "Inverse")
        {
           inverse = true;
        }
        if(other.tag == "Reinverse")
        {
            inverse = false;
        }
    }
}
