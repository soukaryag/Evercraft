using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShrubBehavior : MonoBehaviour
{
    public GameObject shrubDestroyed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDestroy() {
        Instantiate(shrubDestroyed, transform.position, Quaternion.identity);
    }


}
