using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BadApple : MonoBehaviour
{
    public float speed;
    [SerializeField] private float jumpSpeed;
    private float jumpTimer = 0f;

    private Rigidbody2D body;
    [SerializeField] private int direction = 1;
    private Vector2 currentPos;
    private LayerMask mask;
    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        mask = LayerMask.GetMask("Platform");
    }

    // Update is called once per frame
    void Update()
    {
        currentPos = transform.position;
        body.velocity = new Vector2(speed * direction, body.velocity.y);
        jumpTimer += Time.deltaTime;
        if (Physics2D.Linecast(currentPos - new Vector2(-0.25f, 0.45f), currentPos - new Vector2(0.25f, 0.45f), mask) && jumpTimer > 0.1f)
        {
            body.AddForce(Vector3.up * jumpSpeed, ForceMode2D.Impulse);
            jumpTimer = 0f;
        }

        if (Physics2D.Linecast(currentPos - new Vector2(-0.45f, -0.3f), currentPos - new Vector2(-0.45f, 0.3f), mask))
        {
            direction = -1;
        }
        else if (Physics2D.Linecast(currentPos - new Vector2(0.45f, -0.3f), currentPos - new Vector2(0.45f, 0.3f), mask))
        {
            direction = 1;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer != 6)
        {
            body.AddForce((collision.relativeVelocity.normalized + 0.2f * Vector2.up) * 200, ForceMode2D.Impulse);
            direction *= -1;
        }
    }
}