using System.Collections;
using System.Collections.Generic;
using BarthaSzabolcs.Tutorial_SpriteFlash;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float moveSpeed;
    public float jumpForce;
    //public float maxFallVelocity = 35f;

    public KeyCode left;
    public KeyCode right;
    public KeyCode jump;
    public KeyCode throwTongue;

    private Rigidbody2D theRB;
    public Transform groundCheckPoint;
    public LayerMask whatIsGround;
    public float goundCheckRadius;
    public bool isGrounded;
    public bool isHit;

    private Animator anim;

    public AudioSource eatSound;
    public AudioSource tongueSound;
    public AudioSource jumpSound;
    public AudioSource hitSound;

    private Collider2D coll;

    public float attackRate = 5f;
    private float nextAttackTime = 0f;

    private float horizontal;

    [SerializeField] private FlashScript fl;
    public GameObject powerupEffect;
    public GameObject hitEffect;

    private Vector3 originalScale;


    // Start is called before the first frame update
    void Start()
    {
        isGrounded = false;
        theRB = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        originalScale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        if(isHit)
        {
            return;
        }

        isGrounded = Physics2D.OverlapCircle(groundCheckPoint.position, goundCheckRadius, whatIsGround);

        if(Input.GetKey(left))
        {
            theRB.velocity = new Vector2(-moveSpeed, theRB.velocity.y);
        }
        else if(Input.GetKey(right))
        {
            theRB.velocity = new Vector2(moveSpeed, theRB.velocity.y);
        }
        else
        {
            theRB.velocity = new Vector2(0, theRB.velocity.y);
        }

        if(Input.GetKeyDown(jump) && isGrounded)
        {
            theRB.velocity = new Vector2(theRB.velocity.x, jumpForce);
            jumpSound.Play();
        } else {
            //theRB.velocity = new Vector2(theRB.velocity.x, 0);
        }

        if (theRB.velocity.x > 0)
        {
            transform.localScale = new Vector3(originalScale.x, originalScale.y, originalScale.z);
        }
        else if (theRB.velocity.x < 0)
        {
            transform.localScale = new Vector3(-originalScale.x, originalScale.y, originalScale.z);
        }
        
        horizontal = Input.GetAxisRaw("Horizontal");

        if(Time.time >= nextAttackTime)
        {
            if(Input.GetKeyDown(throwTongue))
                {
                    //GameObject bulletClone = (GameObject)Instantiate(bullet, throwPoint.position, throwPoint.rotation);
                    //bulletClone.transform.localScale = transform.localScale;       
                    anim.SetTrigger("Eat");
                    tongueSound.Play();
                    CinemachineShake.Instance.ShakeCamera(0.4f, 0.1f);
                    nextAttackTime = Time.time + 1f / attackRate;
                }
        }

        anim.SetFloat("Speed", Mathf.Abs(theRB.velocity.x));
        anim.SetBool("Grounded", isGrounded);

    }

    private void FixedUpdate()
    {
        //theRB.velocity = Vector3.ClampMagnitude(theRB.velocity, maxFallVelocity);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Ball"))
        {
            fl.Flash();
            hitSound.Play();
            CinemachineShake.Instance.ShakeCamera(2f, 0.5f);
            GameObject effectClone = (GameObject)Instantiate(hitEffect, transform.position, transform.rotation);
            effectClone.transform.parent = transform;
        }

        if(collision.gameObject.CompareTag("Fly"))
        {
            eatSound.Play();
            CinemachineShake.Instance.ShakeCamera(1f, 0.15f);
            GameObject effectClone = (GameObject)Instantiate(powerupEffect, transform.position, transform.rotation);
            effectClone.transform.parent = transform;
        }
    }

}
