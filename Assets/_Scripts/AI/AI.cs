using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AI : MonoBehaviour
{
    [SerializeField]
    private Animator animator;
    [SerializeField]
    private Transform target;
    [SerializeField]
    private float speed = 0.06f;

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
    
    public void modifyPosition(Vector3 addend) {
        tfm.position += addend;
    }

    public Vector3 getPosition() {
        return tfm.position;
    }

    public void modifyScale(Vector3 scale) {
        tfm.localScale = scale;
    }

    // Update is called once per frame
    public virtual void Update()
    {
        
    }
}
