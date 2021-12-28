using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    private bool locked;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void engageLock() {
        locked = true;
    }

    public void removeLock() {
        locked = false;
    }

    public bool isLocked() {
        return locked;
    }
}
