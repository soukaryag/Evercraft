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
            Debug.Log("Initiating the coroutine (speed != 0)");
            getAnimator().SetFloat("SpeedX", randomDirection.x);
            getAnimator().SetFloat("SpeedY", randomDirection.y);
            float distance = 2.0f;
            timer = 0f;
            StartCoroutine(randomWalk(randomDirection, distance));
        }
    }

    IEnumerator randomWalk(Vector2 direction, float distance) {
        int i = 0;
        while (i < actionFrames) {
            Vector3 position = getTransform().position;
            //Debug.Log(position);
            position = new Vector3((direction.x * distance / actionFrames), (direction.y * distance / actionFrames), 0);
            modifyPosition(position);
            //position.y = position.y + (direction.normalized.y * distance / actionFrames);
            i++;
            yield return null;
        }
        Debug.Log("Setting the speed to 0 now");
        getAnimator().SetFloat("SpeedX", 0);
        getAnimator().SetFloat("SpeedY", 0);
        
        
    }
}
