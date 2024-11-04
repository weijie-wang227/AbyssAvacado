using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Rendering;

public class BadApple : MonoBehaviour
{
    public float speed;
    [SerializeField] private float jumpSpeed;

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

        if (Physics2D.Linecast(currentPos - new Vector2(-0.25f, 0.45f), currentPos - new Vector2(0.25f, 0.45f), mask))
        {
            body.AddForce(Vector3.up * jumpSpeed);
        }

        if (Physics2D.Linecast(currentPos - new Vector2(-0.45f, -0.3f), currentPos - new Vector2(-0.45f, 0.3f)))
        {
            direction = -1;
        }
        else if (Physics2D.Linecast(currentPos - new Vector2(0.45f, -0.3f), currentPos - new Vector2(0.45f, 0.3f)))
        {
            direction = 1;
        }
    }
}