using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Coin : MonoBehaviour
{
    
    [Header("Coin Random Splash")]
    public Transform objTrans;
    private float delay = 0;
    private float pasttime = 0;
    private float when = 1.5f;
    private float bounceCoeff, bounceRange;
    private Vector3 off;
    

    [Header("Coin Player Follow")]
    public Rigidbody2D rig;
    public GameObject player;
    private bool magentize = false;

    private void Awake() 
    {
        off = new Vector3(Random.Range(-0.5f, 0.5f), Random.Range(-1.0f, 0f), off.z);
    }

    // Start is called before the first frame update
    private void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        bounceCoeff = Random.Range(-0.05f, -0.01f);
        bounceRange = Random.Range(0.8f, 1.2f);
        
        if (player == null) {
            player = GameObject.FindWithTag("Player");
        }
        StartCoroutine(Magnet());
    }

    // Update is called once per frame
    void Update()
    {
        if (when >= delay) {
            pasttime = Time.deltaTime;
            Vector3 newPos = off * Time.deltaTime;

            objTrans.position += new Vector3(newPos.x, newPos.y + GetWobbleY(delay), newPos.z);
            delay += pasttime;
        }

        if (magentize) {
            Vector3 playerPoint = Vector3.MoveTowards(transform.position, player.transform.position + new Vector3(0, -0.3f, 0), 20 * Time.deltaTime);
            rig.MovePosition(playerPoint);
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (!magentize && other.transform.name == "Walls") {
            off = new Vector3(0f, 0f, off.z);
        }

        if (other.CompareTag("Player") && !other.isTrigger) {
            StartCoroutine(DestroyObject());
        }
    }

    private IEnumerator DestroyObject() 
    {
        yield return new WaitForSeconds(1.1f);
        Destroy(this.gameObject);
    }

    private IEnumerator Magnet() 
    {
        yield return new WaitForSeconds(2.5f);
        magentize = true;
    }

    float GetWobbleY(float t)
    {
        return bounceCoeff * (Mathf.PingPong(3.0f * t, bounceRange) - (bounceRange/2)) * Mathf.Exp(-t);
    }

}
