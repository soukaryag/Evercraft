using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipWhenMoveLeft : MonoBehaviour
{
    private Animator anim;
    private Transform tfm;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(getAnimator().GetFloat("SpeedX") + " " + getAnimator().GetFloat("SpeedY"));
        if (getAnimator().GetFloat("SpeedX") < 0) {
            Debug.Log("SpeedX < 0, flip");
            tfm.localScale = new Vector2(-1f, 1f);
        }
        else {
            Debug.Log("SpeedX >= 0, regular");
            tfm.localScale = new Vector2(1f, 1f);
        }
    }

    public void setAnimator(Animator animator) {
        anim = animator;
    }

    public Animator getAnimator() {
        return anim;
    }

    public void setTransform(Transform transform) {
        tfm = transform;
    }

    public Transform getTransform() {
        return tfm;
    }
}
