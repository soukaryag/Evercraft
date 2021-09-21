using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShrubDestroyedBehavior : MonoBehaviour
{
    private int lifetime;
    
    void Start()
    {
        lifetime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        lifetime++;

        if (lifetime >= 200) {
            Destroy(this);
        }
    }
}
