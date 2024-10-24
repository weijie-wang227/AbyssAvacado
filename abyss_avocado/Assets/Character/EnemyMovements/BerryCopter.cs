using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BerryCopter : MonoBehaviour
{
    // Start is called before the first frame update
    private float cooldown = 3.0f;
    private float dashDuration = 0.5f;
    private float dashTimer = 0f;
    private float timer = 0f;
    [SerializeField] private Transform player;
    private float movementSpeed = 5.0f;
    private Vector3 playerPosition;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > cooldown) {
            timer = 0;
            StartCoroutine(dash());
        }
    }

    private IEnumerator dash () {
        playerPosition = player.position;
        while (dashTimer < dashDuration) { 
            transform.position += (playerPosition - transform.position).normalized * movementSpeed * Time.deltaTime;
            dashTimer += Time.deltaTime;
            yield return null;
        }
        dashTimer = 0;
    }
}
