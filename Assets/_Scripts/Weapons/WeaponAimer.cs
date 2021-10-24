using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAimer : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem weapon1;
    [SerializeField]
    private ParticleSystem weapon2;

    private Vector2 mousePos;

    [SerializeField]
    private Camera playerCamera;

    //private bool weapon2Playing = false;
    private float weaponCooldown = 1f;
    private float weaponTimer = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        weapon2.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Update..." + weaponTimer);
        if (Input.GetMouseButton(0)) {
            mousePos = playerCamera.ScreenToWorldPoint(Input.mousePosition);
            //Debug.Log("raw mouse pos is " + mousePos);
            lookAt(mousePos);
            if (weaponTimer >= weaponCooldown) {
                weaponTimer = 0;
                weapon2.Emit(1);
            }
            
        }
        weaponTimer += Time.deltaTime;

        // if (Input.GetMouseButtonUp(0)) {
        //     Debug.Log("disabled weapon");
        //     weapon2.Stop();
        //     weapon2Playing = false;
        // }



        // if (weapon1 != null) {
        //     //weapon1.transform.localRotation = Quaternion.Euler(0, 0, (Input.mousePosition - transform.position).z);
        //     weapon1.transform.LookAt(new Vector2(
        //         Input.GetAxis("Mouse X"),
        //         Input.GetAxis("Mouse Y")
        //     ));
        // }

        // if (weapon2 != null) {
        //     //Debug.Log(Input.mousePosition);
        //     Debug.Log(weapon2.transform.localRotation);
        //     weapon2.transform.LookAt(new Vector2(
        //         Input.GetAxis("Mouse X"),
        //         Input.GetAxis("Mouse Y")
        //     ));
        //     Debug.Log(weapon2.transform.localRotation);
        // }s
    }

    void lookAt(Vector2 p2)
    {
        float forwardAngle = 0;
        Vector2 p1 = weapon2.transform.position; //0,0
        //Debug.Log("weapon at: " + p1 + " and mouse at " + p2);
        float opp = p2.y - p1.y;
        float adj = p2.x - p1.x;
        //Debug.Log("opp: " + opp + " adj: " + adj);
        float zRadians = Mathf.Atan((p2.y - p1.y) / (p2.x - p1.x));
        //Debug.Log("zRadians: " + zRadians);
        if (adj < 0) {
            //Debug.Log("adding Math.pi");
            zRadians += Mathf.PI;
        }
            
        Quaternion q = Quaternion.Euler(0, 0, Mathf.Rad2Deg * zRadians);
       // Debug.Log("final quaternion: " + q);
        weapon2.transform.rotation = q;
        weapon2.transform.Rotate(0, 0, -forwardAngle);
    }
}
