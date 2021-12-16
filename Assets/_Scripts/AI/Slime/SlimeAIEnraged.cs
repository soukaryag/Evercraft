using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class SlimeAIEnraged : AI
{
    private float actionTimer;
    private float actionTime = 0.7f;

    private ParticleSystem weapon1;
    private ParticleSystem weapon2;

    public float nextWaypointDistance = 3f;
    Path path;
    int currentWaypoint = 0;
    bool reachedEndOfPath = false;

    Seeker seeker;

    bool attacking = false;
    
    // Start is called before the first frame update
    public override void Start()
    {
        seeker = GetComponentInChildren<Seeker>();
        InvokeRepeating("UpdatePath", 0f, .5f);
    }

    public override void Update() 
    {
        Vector2 distanceToTarget = getTarget().position - transform.position;
        if (distanceToTarget.magnitude < 4.0f) {
            getAnimator().SetBool("Moving", false);
            getAnimator().SetBool("Attacking", true);
        }
        else { //distance is between 4 and 6 (or whatever upper bound set in AISM)
            //run towards player
            if (!attacking) { //If the slime isn't invested in an attack animation, allow it to move
                getAnimator().SetBool("Moving", true);
                getAnimator().SetBool("Attacking", false);
                
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
                //Debug.Log((Vector2)path.vectorPath[currentWaypoint]);

                Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - (Vector2)getTransform().position).normalized;
                if (direction.magnitude != 0) {
                    Debug.Log("Setting the SpeedX to " + direction.x + " and the speedy to " + direction.y);
                    Debug.Log("Because... the current waypoint is" + path.vectorPath[currentWaypoint]);
                    addVectorToPosition(new Vector3(direction.x * Time.deltaTime, direction.y * Time.deltaTime, 0));
                }
                
                float distance = Vector2.Distance(getTransform().position, path.vectorPath[currentWaypoint]);
                //Debug.Log("distance is " + distance);
                if (distance < nextWaypointDistance) {
                    
                    currentWaypoint++;
                }
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

    // private void setSeekerTarget(Transform target) {
    //     seeker.
    // }

    public void lockOnAndAttack() {
        lockOn();
        animateAttack();
        attacking = true;
    }

    private void lockOn() {
        /*Compute the direction of attack at this time by rotating the WEAPON to the target */
        RotateToTarget rotator = weapon1.GetComponent<RotateToTarget>();
        rotator.rotate(); 
    }

    private void animateAttack() {
        //Set up the animator for this attack with the intended attack direction
        /* Map directions to a number: N: 0 E: 1 S: 2 W: 3 */
        /* Also remember the X component is ACTUALLY weapon1.transform.up.y
            and the Y component is ACTUALLY -weapon1.transform.up.x 
            because the system is pre-rotated 90 degrees. Don't question! */
        if (-weapon1.transform.up.x > 0 //if NORTH
            && Mathf.Abs(-weapon1.transform.up.x) > Mathf.Abs(weapon1.transform.up.y)) {
                getAnimator().SetInteger("AttackDirection", 0);
        }
        else if (-weapon1.transform.up.x < 0 //if SOUTH
            && Mathf.Abs(-weapon1.transform.up.x) > Mathf.Abs(weapon1.transform.up.y)) {
                getAnimator().SetInteger("AttackDirection", 2);
        }
        else if (weapon1.transform.up.y > 0 //if EAST
            && Mathf.Abs(weapon1.transform.up.y) > -weapon1.transform.up.x) {
                getAnimator().SetInteger("AttackDirection", 1);
        }
        else if (weapon1.transform.up.y < 0 //if WEST
            && Mathf.Abs(weapon1.transform.up.y) > -weapon1.transform.up.x) {
                weapon1.transform.eulerAngles = new Vector3(0, 0, -weapon1.transform.eulerAngles.z);
                getAnimator().SetInteger("AttackDirection", 3);
        }
        else {
            getAnimator().SetInteger("AttackDirection", -1);
        }
        getAnimator().SetBool("Attacking", true);
    }

    public void attackEnded() {
        attacking = false;
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

    void OnEnable() {

    }

    void OnDisable() {
        
    }

    public override void shutDown() {
        getAnimator().SetBool("Attacking", false);
    }
}
