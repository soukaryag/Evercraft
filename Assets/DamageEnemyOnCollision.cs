using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageEnemyOnCollision : MonoBehaviour
{
    public ParticleSystem part;
    public List<ParticleCollisionEvent> collisionEvents;
    public int damage;

    // Start is called before the first frame update
    void Start()
    {
        part = GetComponent<ParticleSystem>();
        collisionEvents = new List<ParticleCollisionEvent>();
        //Physics2D.IgnoreLayerCollision(3, 6, true);
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("?");
    }

    void OnParticleCollision(GameObject other) {
        //Physics.IgnoreCollision(part, other);
        //Debug.Log("collision");
        //Debug.Log(other);
        int numCollisionEvents = part.GetCollisionEvents(other, collisionEvents);
        int i = 0;
        if (other.tag == "Enemy") {
            while (i < numCollisionEvents) {
                //Activate the enemy's get hit animation
                ExternalEventAnimations eea = other.GetComponentInChildren<ExternalEventAnimations>();
                eea.playHitAnimation(collisionEvents[i].velocity);

                HealthBar health = other.GetComponentInChildren<HealthBar>();
                if (health != null) {
                    //Debug.Log("health.null");
                    health.damage(this.damage);
                }
                i++;
            }
        }
        
    }

    // void OnParticleTrigger() {
    //     int i = 0;
    //     while (i < part.trigger.colliderCount) {
    //         Component comp = part.trigger.GetCollider(i);
    //         Debug.Log(comp + " " + i);
    //         HealthBar health = comp.GetComponentInChildren<HealthBar>();
    //         if (health != null) {
    //             //Debug.Log("health.null");
    //             health.damage(this.damage);
    //         }
    //         i++;
    //     }
        
    // }
}
