using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BadApple : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Transform player;
    private char direction;
    private float timer;
    private float countDown;
    private bool isGrounded = false;

    void Start()
    {
        direction = 'r';
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        isGrounded = true;
    }
}
