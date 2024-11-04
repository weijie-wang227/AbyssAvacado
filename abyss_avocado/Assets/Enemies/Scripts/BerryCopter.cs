using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BerryCopter : MonoBehaviour
{
    private float cooldown = 3.0f;
    private float dashDuration = 0.5f;
    private float dashTimer = 0f;
    private float timer = 0f;
    private float movementSpeed = 5.0f;
    private Vector3 playerPosition;
    private Rigidbody2D body;

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > cooldown)
        {
            timer = 0;
            StartCoroutine(dash());
        }
    }

    private IEnumerator dash()
    {
        playerPosition = Player.Instance.transform.position;
        while (dashTimer < dashDuration)
        {
            transform.position += (playerPosition - transform.position).normalized * movementSpeed * Time.deltaTime;
            dashTimer += Time.deltaTime;
            yield return null;
        }
        dashTimer = 0;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer != 6)
        {
            body.AddForce(Vector3.up * 20);
        }
    }
}
