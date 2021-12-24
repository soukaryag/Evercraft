using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class SlimeAIEnraged : AI
{
    private ParticleSystem weapon1;
    private ParticleSystem weapon2;

    public float nextWaypointDistance = 3f;
    Path path;
    int currentWaypoint = 0;
    bool reachedEndOfPath = false;

    Seeker seeker;
    
    // Start is called before the first frame update
    public override void Start()
    {
        seeker = GetComponentInChildren<Seeker>();
        InvokeRepeating("UpdatePath", 0f, .5f); 
        /*In the future, change this to update after a walk animation (?) */

    }

    public override void Update() 
    {
        if (manager.isLocked()) {
            return;
        }
        evaluate();
    }

    public override void evaluate() {
        Vector2 distanceToTarget = getTarget().position - transform.position;
        if (distanceToTarget.magnitude < 4.0f) {
            lockOnAndAttack();
        }
        else { //distance is between 4 and 6 (or whatever upper bound set in AISM)
            //run towards player
            if (path == null) {
                return;
            }
            if (currentWaypoint >= path.vectorPath.Count) {
                reachedEndOfPath = true;
                return;
            }
            else {
                reachedEndOfPath = false;
            }
            Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - (Vector2)getTransform().position).normalized;
            if (direction.magnitude != 0) {
                int quadrant = convertVectorToDirection((Vector3)direction);
                switch(quadrant) {
                    case 0: getAnimatorController().changeAnimation("Slime_move_right"); break;
                    case 1: getAnimatorController().changeAnimation("Slime_move_up"); break;
                    case 2: getAnimatorController().changeAnimation("Slime_move_left"); break;
                    case 3: getAnimatorController().changeAnimation("Slime_move_down"); break;
                    default: getAnimatorController().changeAnimation("Slime_idle"); break;
                }
                addVectorToPosition(new Vector3(direction.x * Time.deltaTime, direction.y * Time.deltaTime, 0));
            }
            float distance = Vector2.Distance(getTransform().position, path.vectorPath[currentWaypoint]);
            if (distance < nextWaypointDistance) {
                currentWaypoint++;
            }
        }
    }

    void UpdatePath() {
        if (seeker.IsDone()) {
            seeker.StartPath(getTransform().position, getTarget().position, OnPathComplete);
        }
    }

    void OnPathComplete(Path p) {
        if(!p.error) {
            path = p;
            currentWaypoint = 0;
        }
    }

    public void lockOnAndAttack() {
        lockOn();
        animateAttack();
    }

    private void lockOn() {
        /*Compute the direction of attack at this time by rotating the WEAPON to the target */
        RotateToTarget rotator = weapon1.GetComponent<RotateToTarget>();
        rotator.rotate(); 
    }

    private void animateAttack() {
        //Debug.Log("in animate attack");
        //Set up the animator for this attack with the intended attack direction
        /* Map directions to a number: N: 0 E: 1 S: 2 W: 3 */
        /* Also remember the X component is ACTUALLY weapon1.transform.up.y
            and the Y component is ACTUALLY -weapon1.transform.up.x 
            because the system is pre-rotated 90 degrees. Don't question! */
        if (-weapon1.transform.up.x > 0 //if NORTH
            && Mathf.Abs(-weapon1.transform.up.x) > Mathf.Abs(weapon1.transform.up.y)) {
                getAnimatorController().changeAnimation("Slime_attack_up");
        }
        else if (-weapon1.transform.up.x < 0 //if SOUTH
            && Mathf.Abs(-weapon1.transform.up.x) > Mathf.Abs(weapon1.transform.up.y)) {
                getAnimatorController().changeAnimation("Slime_attack_down");
        }
        else if (weapon1.transform.up.y > 0 //if EAST
            && Mathf.Abs(weapon1.transform.up.y) > -weapon1.transform.up.x) {
                getAnimatorController().changeAnimation("Slime_attack_right");
        }
        else if (weapon1.transform.up.y < 0 //if WEST
            && Mathf.Abs(weapon1.transform.up.y) > -weapon1.transform.up.x) {
                //weapon1.transform.eulerAngles = new Vector3(0, 0, -weapon1.transform.eulerAngles.z);
                getAnimatorController().changeAnimation("Slime_attack_left");
        }
        else {
            getAnimatorController().changeAnimation("Slime_idle");
        }
        manager.toggleLock();
    }

    public void setWeapon1(ParticleSystem w1) {
        weapon1 = w1;
    }

    public void setWeapon2(ParticleSystem w2) {
        weapon2 = w2;
    }

    public void fire() {
        weapon1.Emit(1);
    }

    public override void startUp()
    {
        base.startUp();
        manager.getEmojiBackground().color = new Color32(255, 0, 0, 255);
    }

    public override void shutDown() {
        base.shutDown();
        manager.getEmojiBackground().color = new Color32(200, 200, 200, 255);
    }
}
