using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class BadApple : MonoBehaviour
{
    public float speed;
    [SerializeField] private float jumpSpeed;

    private Rigidbody2D body;
    private int direction = 1;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        body.velocity = new Vector2(speed * direction, body.velocity.y);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer != 6)
        {
            direction *= -1;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 6)
        {
            body.AddForce(Vector3.up * jumpSpeed);
        }
    }
}