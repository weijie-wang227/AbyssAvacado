using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BerryCopter : EnemyAI
{
    private readonly float dashDuration = 0.5f;
    private float dashTimer = 0f;
    private readonly float attackInterval = 2.0f;
    private float attackTimer = 0f;
    [SerializeField] private float movementSpeed = 50.0f;
    [SerializeField] private Rigidbody2D body;

    // Roaming 
    private Vector2 roamStart;
    private Vector2 roamDest;
    [SerializeField] private float roamSpeed = 2;
    [SerializeField] private float minRoamDistance = 2;
    [SerializeField] private float maxRoamDistance = 10;
    private readonly float minWaitTime = 0.5f;
    private readonly float maxWaitTime = 1f;
    private bool isWaiting = false;

    private void Start()
    {
        roamStart = transform.position;
        roamDest = GetRoamDestination();
    }

    void Update()
    {
        switch (state)
        {
            case State.Idle:
                Roam();
                break;
            case State.Aggro:
                attackTimer += Time.deltaTime;
                if (attackTimer > attackInterval)
                {
                    attackTimer = 0;
                    StartCoroutine(Dash());
                }
                break;
        }
    }

    private IEnumerator Dash()
    {
        var playerPosition = Player.Instance.transform.position;
        Vector2 force = (playerPosition - transform.position).normalized * movementSpeed;
        body.AddForce(force);
        yield return new WaitForSeconds(dashDuration);
        while (dashTimer < 0.3f)
        {
            body.velocity = Vector2.Lerp(body.velocity, Vector2.zero, 1 - attackTimer / 0.3f);
            dashTimer += Time.deltaTime;
            yield return null;
        }
        dashTimer = 0;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        StopAllCoroutines();
        if (collision.gameObject.layer != 6)
        {
            body.AddForce((collision.relativeVelocity.normalized + 0.2f * Vector2.up) * 100, ForceMode2D.Impulse);
        }
    }

    public void Roam()
    {
        if (isWaiting) return;

        body.MovePosition(Vector2.MoveTowards(transform.position, roamDest, roamSpeed * Time.deltaTime));
        if (Vector2.Distance(transform.position, roamDest) < 1)
        {
            StartCoroutine(Wait());
            roamDest = GetRoamDestination();
        }
    }

    private Vector2 GetRoamDestination()
    {
        var randomXDir = Random.Range(-1, 1);
        var randomYDir = Random.Range(-1, 1);
        var randomDirection = new Vector2(randomXDir, randomYDir);

        var randomDistance = Random.Range(minRoamDistance, maxRoamDistance);
        return roamStart + randomDirection * randomDistance;
    }

    private IEnumerator Wait()
    {
        isWaiting = true;
        float waitTime = Random.Range(minWaitTime, maxWaitTime);
        yield return new WaitForSeconds(waitTime);
        isWaiting = false;
    }
}
