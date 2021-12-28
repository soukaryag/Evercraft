using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExternalEventAnimationsPlayer : ExternalEventAnimations
{
    Manager manager;
    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        manager = GetComponent<Manager>();
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
            case 0: ac.changeAnimation("Player_hit_from_left"); break;
            case 1: ac.changeAnimation("Player_hit_from_down"); break;
            case 2: ac.changeAnimation("Player_hit_from_right"); break;
            case 3: ac.changeAnimation("Player_hit_from_up"); break;
            default: ac.changeAnimation("Player_idle"); break;
        }
    }
}
