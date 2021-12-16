using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AI : MonoBehaviour
{
    [SerializeField]
    private Animator animator;
    [SerializeField]
    private Transform target;
    private AnimatorController ac;

    Transform tfm;
    // Start is called before the first frame update
    public virtual void Start()
    {
        
    }

    public void setTransform(Transform main) {
        tfm = main;
    }

    public Transform getTransform() {
        return tfm;
    }

    public void setAnimator(Animator anim) {
        animator = anim;
    }

    public Animator getAnimator() {
        return animator;
    }

    public void setTarget(Transform t) {
        target = t;
    }

    public Transform getTarget() {
        return target;
    }
    
    public void addVectorToPosition(Vector3 addend) {
        int direction = convertVectorToDirection(addend);
        tfm.position += addend;
        getAnimator().SetInteger("MoveDirection", direction);
    }

    public Vector3 getPosition() {
        return tfm.position;
    }

    public void modifyScale(Vector3 scale) {
        tfm.localScale = scale;
    }

    /*
    For switching out of this state */
    public abstract void shutDown();

    static int convertVectorToDirection(Vector3 vector) {
        float angle = Mathf.Atan2(vector.y, vector.x);
        int quadrant = (int)Mathf.Round( 4 * angle / (2*Mathf.PI) + 4 ) % 4;
        return quadrant;
    }

    public void setAnimatorController(AnimatorController ac) {
        this.ac = ac;
    }

    public AnimatorController GetAnimatorController() {
        return this.ac;
    }

    // Update is called once per frame
    public virtual void Update()
    {
        
    }
}
