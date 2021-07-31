using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovesUsingKeys : MonoBehaviour
{
    //Collider col;
    // Start is called before the first frame update
    //void Start()
    //{
    //    col = gameObject.GetComponent(typeof(Collider)) as Collider;
    //    if (col == null) {
    //        Debug.Log("Idiot forgot collider on object");
    //    }
    //}

    public Vector2 moveSpeed = new Vector2(0.12f, 0.12f);
    public Rigidbody2D rb;
    Vector2 movement;
    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
    }

    void FixedUpdate() {
        rb.MovePosition(rb.position + movement * moveSpeed);
    }

    void OnCollisionEnter(Collision other) {
        Debug.Log("collided with" + other);

    }

    void OnCollision2D(Collision2D collision) {
        Debug.Log("Fuck you");
    }

    void OnTriggerEnter(Collider other) {
        Debug.Log("SMD");
    }
}
