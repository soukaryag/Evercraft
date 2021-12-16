using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random=UnityEngine.Random;

public class SlimeAIIdle : AI
{
    private float timer;
    private float actionTime = 4.0f;
    private int actionFrames = 270; //how many frames the action should run for
    // Start is called before the first frame update
    //private Random rng;
    private IEnumerator walk;
    public override void Start()
    {
        timer = 0f;
    }

    // Update is called once per frame
    void FixedUpdate() 
    {
        timer += Time.deltaTime;
        if (timer >= actionTime) {
            float directionX = (float)(Random.value - 0.5);
            float directionY = (float)(Random.value - 0.5);
            Vector2 randomDirection = (new Vector2(directionX, directionY)).normalized;
            getAnimator().SetFloat("SpeedX", randomDirection.x);
            getAnimator().SetFloat("SpeedY", randomDirection.y);
            getAnimator().SetBool("Moving", true);
            float distance = 2.0f;
            timer = 0f;
            walk = slimeRandomWalk(randomDirection, distance);
            StartCoroutine(walk);
        }
    }

    IEnumerator slimeRandomWalk(Vector2 direction, float distance) {
        Debug.Log("in the walk");
        int i = 0;
        while (i < actionFrames) {
            Vector3 increment = new Vector3((direction.x * distance / actionFrames), (direction.y * distance / actionFrames), 0);
            addVectorToPosition(increment);
            i++;
            yield return null;
        }
        getAnimator().SetBool("Moving", false);
    }

    public override void shutDown() {
        Debug.Log(walk);
        if (walk != null) {
            StopCoroutine(walk);
        }
        
        getAnimator().SetBool("Moving", false);
    }
}
