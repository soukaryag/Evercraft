using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExternalEventAnimationsSlime : ExternalEventAnimations
{
    // Start is called before the first frame update
    AISM manager;
    void Start()
    {
        base.Start();
        manager = transform.parent.gameObject.GetComponentInChildren<AISM>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void playHitAnimation(Vector2 impactDirection)
    {
        base.playHitAnimation(impactDirection);
        manager.engageLock();
        int quadrant = convertVectorToDirection(impactDirection);
        switch(quadrant) {
            case 0: ac.changeAnimation("Slime_hit_from_left"); break;
            case 1: ac.changeAnimation("Slime_hit_from_down"); break;
            case 2: ac.changeAnimation("Slime_hit_from_right"); break;
            case 3: ac.changeAnimation("Slime_hit_from_up"); break;
            default: ac.changeAnimation("Slime_idle"); break;
        }
    }
}
