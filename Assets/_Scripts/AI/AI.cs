using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class AI : MonoBehaviour
{
    [SerializeField]
    private Animator animator;
    [SerializeField]
    private Transform target;
    private AnimatorController ac;
    protected AISM manager;
    [SerializeField]
    private Image emoji;

    Transform tfm;

    protected Rigidbody2D rb;
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

    public void setRigidbody(Rigidbody2D rb) {
        this.rb = rb;
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
        //int direction = convertVectorToDirection(addend);
        tfm.position += addend;
        //getAnimator().SetInteger("MoveDirection", direction);
    }

    public Vector3 getPosition() {
        return tfm.position;
    }

    public void modifyScale(Vector3 scale) {
        tfm.localScale = scale;
    }

    /*
    For switching out of this state */
    public virtual void shutDown() {
        emoji.enabled = false;
    }

    public virtual void startUp() {
        emoji.enabled = true;
    }

    protected static int convertVectorToDirection(Vector3 vector) {
        float angle = Mathf.Atan2(vector.y, vector.x);
        int quadrant = (int)Mathf.Round( 4 * angle / (2*Mathf.PI) + 4 ) % 4;
        return quadrant;
    }

    public void setAnimatorController(AnimatorController ac) {
        this.ac = ac;
    }

    public AnimatorController getAnimatorController() {
        return this.ac;
    }

    public void setManager(AISM manager) {
        this.manager = manager;
    }

    public abstract void evaluate();

    public Image getEmoji() {
        return emoji;
    }

    public virtual void playDefaultAnimation() {

    }

    // Update is called once per frame
    public virtual void Update()
    {
        
    }
}
