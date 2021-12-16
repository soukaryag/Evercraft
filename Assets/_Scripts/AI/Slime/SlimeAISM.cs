using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SlimeAISM : AISM
{


    [SerializeField]
    private ParticleSystem weapon1;
    [SerializeField]
    private ParticleSystem weapon2;
    SlimeAIIdle idleAI;
    SlimeAIEnraged enragedAI;

    /* Acquire handles to our AI scripts. Set them up with object instances accordingly */
    public override void Start()
    {
        base.Start();
        //Slime specific AI setup goes here
        idleAI = GetComponentInChildren<SlimeAIIdle>();
        enragedAI = GetComponentInChildren<SlimeAIEnraged>();
        enragedAI.setWeapon1(weapon1);
        enragedAI.setWeapon2(weapon2);
        
    }

    /* Idle - > Enraged if collision with player detector */
    /* Enraged -> Idle if distance > a bigger number */
    public override void Update()
    {
        if (enragedAI == currentAI) {
            Vector2 distanceToTarget = enragedAI.getTarget().position - transform.position;
            if (distanceToTarget.magnitude > 6.0f) {
                enragedAI.getAnimator().SetBool("Attacking", false);
                transitionTo(idleAI);
            }
        }
    }

    /* Transition to a new AI, disabling the current one */
    void transitionTo(AI newAI) {
        currentAI.enabled = false;
        newAI.enabled = true;
        currentAI = newAI;
        transform.parent.localScale = new Vector2(1f, 1f);
    }

    /* Transition to enraged upon spotting a player. Set that player as the AI's target */
    void OnTriggerEnter2D(Collider2D other) {
        if (enragedAI != currentAI) {
            if (other.tag == "Player") {
                if (currentAI == idleAI) {
                    currentAI.shutDown();
                }
                
                //Debug.Log(other.tag + " is Player");
                enragedAI.setTarget(other.transform);
                weapon1.GetComponent<RotateToTarget>().setTarget(other.transform);
                transitionTo(enragedAI);
            }
        }
        
    }

    public ParticleSystem getWeapon1() {
        return weapon1;
    }


}
