using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BerryCopter : MonoBehaviour
{
    private float cooldown = 2.0f;
    private float dashDuration = 0.5f;
    private float dashTimer = 0f;
    private float timer = 0f;
    [SerializeField] private float movementSpeed = 50.0f;
    private Vector3 playerPosition;
    private Rigidbody2D body;
    private Coroutine dash;

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
            dash = StartCoroutine(Dash());
        }
    }

    private IEnumerator Dash()
    {
        print("startdash");
        playerPosition = Player.Instance.transform.position;
        Vector2 force = (playerPosition - transform.position).normalized * movementSpeed;
        body.AddForce(force);
        yield return new WaitForSeconds(dashDuration);
        while (dashTimer < 0.3f)
        {
            body.velocity = Vector2.Lerp(body.velocity, Vector2.zero, 1 - timer / 0.3f);
            dashTimer += Time.deltaTime;
            yield return null;
        }
        dashTimer = 0;
        print("dash");
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        StopAllCoroutines();
        if (collision.gameObject.layer != 6)
        {
            body.AddForce((collision.relativeVelocity.normalized + 0.2f * Vector2.up) * 100, ForceMode2D.Impulse);
        }
    }
}
