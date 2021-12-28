using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExternalEventAnimations : MonoBehaviour
{
    // Start is called before the first frame update
    protected AnimatorController ac;
    protected void Start()
    {
        ac = GetComponent<AnimatorController>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void playHitAnimation(Vector2 impactDirection) {
        
    }

    protected int convertVectorToDirection(Vector3 vector) {
        float angle = Mathf.Atan2(vector.y, vector.x);
        int quadrant = (int)Mathf.Round( 4 * angle / (2*Mathf.PI) + 4 ) % 4;
        return quadrant;
    }
}
