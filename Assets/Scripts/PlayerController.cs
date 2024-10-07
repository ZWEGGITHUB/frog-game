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
    public GameObject win;
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
            anim.SetTrigger("Jump");
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
                    CinemachineShake.Instance.ShakeCamera(0.5f, 0.25f);
                    nextAttackTime = Time.time + 1f / attackRate;
                }
        }
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
            ScoreManager scoreManager = FindObjectOfType<ScoreManager>();
            if (scoreManager != null)
            {
                scoreManager.MinusScore(100); 
            }
            CinemachineShake.Instance.ShakeCamera(2f, 0.75f);
            GameObject effectClone = (GameObject)Instantiate(hitEffect, transform.position, transform.rotation);
            effectClone.transform.parent = transform;
        }
    }
    private void OnTriggerEnter2D(Collider2D trigger)
    {
        if (trigger.CompareTag("FlyBlack"))
        {
            eatSound.Play();
            CinemachineShake.Instance.ShakeCamera(1.5f, 0.5f);
            ScoreManager scoreManager = FindObjectOfType<ScoreManager>();
            if (scoreManager != null)
            {
                scoreManager.AddScore(100); 
            }
            StartCoroutine(ChangeSpeedCoroutine());
            GameObject effectClone = (GameObject)Instantiate(powerupEffect, transform.position, transform.rotation);
            effectClone.transform.parent = transform;
        }
        if (trigger.CompareTag("FlyGreen"))
        {
            eatSound.Play();
            CinemachineShake.Instance.ShakeCamera(1.5f, 0.5f);
            ScoreManager scoreManager = FindObjectOfType<ScoreManager>();
            if (scoreManager != null)
            {
                scoreManager.AddScore(100); 
            }
            StartCoroutine(ChangeJumpCoroutine());
            GameObject effectClone = (GameObject)Instantiate(powerupEffect, transform.position, transform.rotation);
            effectClone.transform.parent = transform;
        }
        if (trigger.CompareTag("Win"))
        {
            win.gameObject.SetActive(true);
            Time.timeScale = 0;
        }
    }

    private IEnumerator ChangeSpeedCoroutine()
    {
        moveSpeed = 17f;

        yield return new WaitForSeconds(10f);

        moveSpeed = 10f;
    }
    private IEnumerator ChangeJumpCoroutine()
    {
        jumpForce = 37f;

        yield return new WaitForSeconds(10f);

        jumpForce = 25f;
    }

}
