using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEvents : MonoBehaviour
{
    Manager manager;
    // Start is called before the first frame update
    void Start()
    {
        manager = GetComponent<Manager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void doNothing() {

    }

    public void removeLock() {
        manager.removeLock();
    }
}
