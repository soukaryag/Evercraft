using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateToTarget : MonoBehaviour
{
    private Transform target;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("update: " + transform.rotation.z);
    }

    void lookAt(Vector2 p2)
    {
        float forwardAngle = 0;
        Vector2 p1 = transform.position;
        float opp = p2.y - p1.y;
        float adj = p2.x - p1.x;
        float zRadians = Mathf.Atan((p2.y - p1.y) / (p2.x - p1.x));

        if (adj < 0) {
            zRadians += Mathf.PI;
        }
        
        Quaternion q = Quaternion.Euler(0, 0, Mathf.Rad2Deg * zRadians);
        transform.rotation = q;
        transform.Rotate(0, 0, -forwardAngle);
    }

    public void setTarget(Transform t) {
        target = t;
    }

    public void rotate() {
        if (target != null) { //maybe unnecessary
            lookAt(new Vector2(target.position.x, target.position.y));
        }
        
    }
}
