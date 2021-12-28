using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowKeysToMove : MonoBehaviour
{
    Vector2 movement;
    Rigidbody2D rb;
    [SerializeField]
    private float speed;
    AnimatorController ac;

    Manager manager;
    // Start is called before the first frame update
    void Start()
    {
        movement = new Vector2(0, 0);
        rb = GetComponent<Rigidbody2D>();
        ac = GetComponent<AnimatorController>();
        manager = GetComponent<Manager>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (manager.isLocked()) {
            return;
        }
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        //transform.position += (Vector3)movement.normalized * 0.01f;
        rb.MovePosition(rb.position + (movement.normalized * Time.deltaTime * speed));
        if (movement.magnitude == 0) {
            ac.changeAnimation("Player_idle");
        }
        else {
            int quadrant = convertVectorToDirection((Vector3)movement);
            switch(quadrant) {
                case 0: ac.changeAnimation("Player_move_right"); break;
                case 1: ac.changeAnimation("Player_move_up"); break;
                case 2: ac.changeAnimation("Player_move_left"); break;
                case 3: ac.changeAnimation("Player_move_down"); break;
                default: ac.changeAnimation("Player_idle"); break;
            }
        }
    }

    protected int convertVectorToDirection(Vector3 vector) {
        float angle = Mathf.Atan2(vector.y, vector.x);
        int quadrant = (int)Mathf.Round( 4 * angle / (2*Mathf.PI) + 4 ) % 4;
        return quadrant;
    }
}
