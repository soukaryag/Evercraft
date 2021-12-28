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

    private Manager manager;

    private float weaponCooldown = 0.25f;
    private float weaponTimer = 10f;

    // Start is called before the first frame update
    void Start()
    {
        var main = weapon1.main;
        main.startLifetime = float.PositiveInfinity;
        weapon2.Stop();
        manager = transform.parent.GetComponent<Manager>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (!manager.isLocked()) {
            if (Input.GetMouseButton(0)) {
                mousePos = playerCamera.ScreenToWorldPoint(Input.mousePosition);
                lookAt(mousePos);
                if (weaponTimer >= weaponCooldown) {
                    weaponTimer = 0;
                    weapon2.Emit(1);
                }
            }
        }
        
        weaponTimer += Time.deltaTime;
    }

    void lookAt(Vector2 p2)
    {
        float forwardAngle = 0;
        Vector2 p1 = weapon2.transform.position;
        float opp = p2.y - p1.y;
        float adj = p2.x - p1.x;
        float zRadians = Mathf.Atan((p2.y - p1.y) / (p2.x - p1.x));

        if (adj < 0) {
            zRadians += Mathf.PI;
        }
        
        Quaternion q = Quaternion.Euler(0, 0, Mathf.Rad2Deg * zRadians);
        weapon2.transform.rotation = q;
        weapon2.transform.Rotate(0, 0, -forwardAngle);
    }
}
