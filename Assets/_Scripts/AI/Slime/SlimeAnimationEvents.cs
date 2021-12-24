using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeAnimationEvents : MonoBehaviour
{
    private ParticleSystem weapon1;
    private Transform tfm;
    private SlimeAIEnraged enragedAI;
    private SlimeAISM manager;
    // Start is called before the first frame update
    void Start()
    {
        tfm = transform.parent;
        weapon1 = transform.parent.gameObject.GetComponentInChildren<SlimeAISM>().getWeapon1();
        enragedAI = transform.parent.gameObject.GetComponentInChildren<SlimeAIEnraged>();
        manager = transform.parent.gameObject.GetComponentInChildren<SlimeAISM>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void fireWeapon1() {
        weapon1.Emit(1);
    }

    public void mirror() {
        Debug.Log("mirror called");
        tfm.localScale = new Vector2(-1f, 1f);
    }

    public void resetMirror() {
        Debug.Log("reset mirror called");
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

    public void toggleLock() {
        manager.toggleLock();
    }

    public void doNothing() {
        //Do nothing. Filler for adding animation events to the end of animations
        //and squelching the error message
    }
}
