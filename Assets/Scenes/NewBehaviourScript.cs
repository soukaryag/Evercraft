using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    private string moveInputAxis = "Vertical";
    
    public float moveSpeed = 0.1f;
    public Rigidbody rb;
    public bool cubeIsOnTheGround = true;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    
    // Update is called once per frame
    void Update()
    {
       float moveAxis = Input.GetAxis(moveInputAxis); 

       ApplyInput(moveAxis);

       if(Input.GetButtonDown("Jump") && cubeIsOnTheGround == true)
       {
           rb.AddForce(new Vector3(0, 7, 0), ForceMode.Impulse);
           cubeIsOnTheGround = false;
       } 
    }

    private void ApplyInput(float moveInput)
    {
        Move(moveInput);
    }

    private void Move(float input)
    {
        transform.Translate(Vector3.forward * input * moveSpeed);
    }    

    private void OnCollisionEnter(Collision collision) {
        if(collision.gameObject.tag == "Ground") {
            cubeIsOnTheGround = true;
        }
    }
}
