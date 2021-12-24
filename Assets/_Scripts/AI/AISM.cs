using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class AISM : MonoBehaviour
{
    [SerializeField]
    private Image emojiBackground;
    private AI defaultAI;
    protected AI currentAI;
    protected List<AI> AIs;
    private Animator animator;

    private AnimatorController animatorController;
    private Transform tfm;
    private bool locked = false;
    // Start is called before the first frame update
    public virtual void Start()
    {
        AIs = new List<AI>();
        tfm = transform.parent;
        animator = transform.parent.gameObject.GetComponentInChildren<Animator>();
        animatorController = transform.parent.gameObject.GetComponentInChildren<AnimatorController>();
        AI[] ais = GetComponentsInChildren<AI>();
        FlipWhenMoveLeft flipper = GetComponent<FlipWhenMoveLeft>();
        flipper.setAnimator(animator);
        flipper.setTransform(tfm);
        foreach(AI el in GetComponentsInChildren<AI>()) {
            //Add it to our list, and give it access to the animater and transform
            el.setAnimator(animator);
            el.setTransform(tfm);
            el.setAnimatorController(animatorController);
            el.setManager(this);
            AIs.Add(el);
        }
        defaultAI = AIs[0];
        currentAI = AIs[0];
        currentAI.enabled = true;
    }

    // Update is called once per frame
    public virtual void Update()
    {
        
    }

    /* Transition to a new AI, disabling the current one */
    protected void transitionTo(AI newAI) {
        currentAI.enabled = false;
        currentAI.shutDown();
        newAI.enabled = true;
        newAI.startUp();
        currentAI = newAI;
        transform.parent.localScale = new Vector2(1f, 1f);
    }

    public bool isLocked() { //is this slime locked in an animation and cannot take any new actions?
        return locked;
    }

    public void toggleLock() {
        //Debug.Log("setting the lock to " + !locked);
        locked = !locked;
        if (!locked) {
            currentAI.evaluate();
        }
    }

    public Image getEmojiBackground() {
        return emojiBackground;
    }
}
