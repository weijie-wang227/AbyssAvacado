using UnityEngine;

public class CharacterMotor : MonoBehaviour
{
    private float movementSpeed = 3f;
    private float jumpSpeed = 500f;
    private bool isGrounded = false;
    private bool rightContact = false;
    private bool leftContact = false;
    private bool isSmashing;
    private Vector2 currentPos;
    private LayerMask mask;
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
    }

    void Update()
    {
        currentPos = transform.position;

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

        if (isGrounded)
        {
            body.gravityScale = 2.1f;
        }

        if (Input.GetKey(KeyCode.LeftArrow) && !leftContact)
        {
            transform.position += Vector3.left * movementSpeed * Time.deltaTime;
            sprite.flipX = true;
        }
        if (Input.GetKey(KeyCode.RightArrow) && !rightContact)
        {
            transform.position += Vector3.right * movementSpeed * Time.deltaTime;
            sprite.flipX = false;
        }
        if (!isGrounded && Input.GetKey(KeyCode.DownArrow))
        {
            body.AddForce(Vector2.down * 40f);
        }
        if (isGrounded && Input.GetKeyDown(KeyCode.UpArrow))
        {
            body.AddForce(Vector3.up * jumpSpeed);
            isGrounded = false;   
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

