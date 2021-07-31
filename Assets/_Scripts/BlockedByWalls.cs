using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockedByWalls : MonoBehaviour
{

    //Collider col;
    // Start is called before the first frame update
    void Start()
    {
        //col = gameObject.GetComponent(typeof(Collider)) as Collider;
        //if (col == null) {
        //    Debug.Log("Idiot forgot collider on object");
        //}
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void onCollisionEnter(Collider other) {
        Debug.Log("collided with" + other);

    }

    
}
