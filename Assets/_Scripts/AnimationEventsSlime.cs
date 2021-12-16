using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEventsSlime : MonoBehaviour
{
    private ParticleSystem weapon1;
    private Transform tfm;
    private SlimeAIEnraged enragedAI;
    // Start is called before the first frame update
    void Start()
    {
        tfm = transform.parent;
        weapon1 = transform.parent.gameObject.GetComponentInChildren<SlimeAISM>().getWeapon1();
        enragedAI = transform.parent.gameObject.GetComponentInChildren<SlimeAIEnraged>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void fireWeapon1() {
        Debug.Log("FIRING shot");
        weapon1.Emit(1);
    }

    public void mirror() {
        tfm.localScale = new Vector2(-1f, 1f);
    }

    public void resetMirror() {
        tfm.localScale = new Vector2(1f, 1f);
    }

    public void mirrorWeapon() {
        weapon1.transform.localScale = new Vector2(-1f, 1f);
    }

    public void resetWeaponMirror() {
        weapon1.transform.localScale = new Vector2(1f, 1f);
    }

    public void animateAttack() {
        enragedAI.lockOnAndAttack();
    }

    public void attackEnded() {
        enragedAI.attackEnded();
    }

    public void doNothing() {
        //Do nothing. Filler for adding animation events to the end of animations
        //and squelching the error message
    }
}
