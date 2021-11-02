using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AISM : MonoBehaviour
{
    private AI defaultAI;
    private List<AI> AIs;
    private Animator animator;
    private Transform tfm;
    // Start is called before the first frame update
    public virtual void Start()
    {
        AIs = new List<AI>();
        tfm = transform.parent;
        animator = transform.parent.gameObject.GetComponentInChildren<Animator>();
        AI[] ais = GetComponentsInChildren<AI>();
        FlipWhenMoveLeft flipper = GetComponent<FlipWhenMoveLeft>();
        flipper.setAnimator(animator);
        flipper.setTransform(tfm);
        foreach(AI el in GetComponentsInChildren<AI>()) {
            //Add it to our list, and give it access to the animater and transform
            el.setAnimator(animator);
            el.setTransform(tfm);
            AIs.Add(el);
        }
        defaultAI = AIs[0];
        defaultAI.enabled = true;
    }

    // Update is called once per frame
    public virtual void Update()
    {
        
    }
}
