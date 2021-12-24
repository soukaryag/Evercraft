using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random=UnityEngine.Random;

public class SlimeAIIdle : AI
{
    private float timer;
    private float actionTime = 2.0f;
    private int actionFrames = 270; //how many frames the action should run for
    private IEnumerator walk;
    public override void Start()
    {
        timer = 0f;
    }

    // Update is called once per frame
    public override void Update() 
    {
        evaluate();
    }

    public override void evaluate() {
        if (manager.isLocked()) {
            return;
        }
        timer += Time.deltaTime;
        if (timer >= actionTime) {
            float directionX = (float)(Random.value - 0.5);
            float directionY = (float)(Random.value - 0.5);
            Vector2 randomDirection = (new Vector2(directionX, directionY)).normalized;
            int quadrant = convertVectorToDirection(randomDirection);
            switch(quadrant) {
                case 0: getAnimatorController().changeAnimation("Slime_move_right"); break;
                case 1: getAnimatorController().changeAnimation("Slime_move_up"); break;
                case 2: getAnimatorController().changeAnimation("Slime_move_left"); break;
                case 3: getAnimatorController().changeAnimation("Slime_move_down"); break;
                default: getAnimatorController().changeAnimation("Slime_idle"); break;
            }
            float distance = 2.0f;
            timer = 0f;
            walk = slimeRandomWalk(randomDirection, distance);
            StartCoroutine(walk);
        }
    }

    IEnumerator slimeRandomWalk(Vector2 direction, float distance) {
        int i = 0;
        while (i < actionFrames) {
            Vector3 increment = new Vector3((direction.x * distance / actionFrames), (direction.y * distance / actionFrames), 0);
            addVectorToPosition(increment);
            i++;
            yield return null;
        }
        getAnimatorController().changeAnimation("Slime_idle");
    }

    public override void shutDown() {
        base.shutDown();
        if (walk != null) {
            StopCoroutine(walk);
        }
    }

    public override void startUp()
    {
        base.startUp();
        getAnimatorController().changeAnimation("Slime_idle");
        timer = 0f;
    }
}
