using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovesUsingKeys : MonoBehaviour
{
    [SerializeField]
    public float moveSpeed = 0.075f;
    public Rigidbody2D rb;
    Vector2 movement;
    public Animator animator;

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);
    }

    void FixedUpdate() {
        rb.MovePosition(rb.position + movement * moveSpeed);
    }
}
