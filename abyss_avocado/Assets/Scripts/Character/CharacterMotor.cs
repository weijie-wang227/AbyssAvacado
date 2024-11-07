using System.Collections;
using Unity.VisualScripting;
using UnityEditor.Animations;
using UnityEngine;

public class CharacterMotor : MonoBehaviour
{
    private float movementSpeed = 3f;
    private float jumpSpeed = 500f;
    private bool isGrounded = false;
    private bool rightContact = false;
    private bool leftContact = false;
    private bool isSmashing;
    private float jumpDelay;
    private Vector2 currentPos;
    private LayerMask mask;
    private Animator animator;
    [SerializeField] private Rigidbody2D body;
    [SerializeField] private SpriteRenderer sprite;
    [SerializeField] private AudioSource smashAudio;
    [SerializeField] private ContactDamage contactDamage;
    [SerializeField] private HealthManager healthManager;

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        mask = LayerMask.GetMask("Platform");
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        currentPos = transform.position;
        jumpDelay += Time.deltaTime;
        animator.SetFloat("Speed", body.velocity.y);
        if (Physics2D.Linecast(currentPos - new Vector2(-0.25f, 0.5f), currentPos - new Vector2(0.25f, 0.5f), mask))
        {
            isGrounded = true;
            if (isSmashing)
            {
                smashAudio.Play();
            }
        }
        else
        {
            isGrounded = false;
        }

        if (body.velocity.y < -35f)
        {
            isSmashing = true;
            healthManager.ActivateInvul(); // invulnerable when smashing
        }
        else
        {
            isSmashing = false;
        }

        if (Physics2D.Linecast(currentPos - new Vector2(-0.35f, -0.5f), currentPos - new Vector2(-0.35f, 0.4f), mask))
        {
            rightContact = true;
        }
        else
        {
            rightContact = false;
        }

        if (Physics2D.Linecast(currentPos - new Vector2(0.35f, -0.5f), currentPos - new Vector2(0.35f, 0.4f), mask))
        {
            leftContact = true;
        }
        else
        {
            leftContact = false;
        }


        if (Input.GetAxis("Horizontal") < -0.1 && !leftContact)
        {
            transform.position += Vector3.left * movementSpeed * Time.deltaTime;
            sprite.flipX = true;
        }
        if (Input.GetAxis("Horizontal") > 0.1 && !rightContact)
        {
            transform.position += Vector3.right * movementSpeed * Time.deltaTime;
            sprite.flipX = false;
        }
        if (!isGrounded && Input.GetAxis("Vertical") < -0.1)
        {
            body.AddForce(Vector2.down * 40f);
        }
        if (isGrounded && Input.GetAxis("Vertical") > 0.1 && jumpDelay > 0.1f)
        {
            body.AddForce(Vector3.up * jumpSpeed);
            isGrounded = false;
            jumpDelay = 0f;
        }

        contactDamage.isActive = isSmashing;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.collider.CompareTag("Enemy"))
        {
            body.AddForce((col.transform.position - transform.position).normalized * -5f, ForceMode2D.Impulse);
            col.rigidbody.AddForce((col.transform.position - transform.position).normalized * 5f, ForceMode2D.Impulse);
        }
    }



}

