using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeAnimations : AnimatorController
{


    public const string SLIME_IDLE = "Slime_idle";
    public const string SLIME_MOVE_UP = "Slime_move_up";
    public const string SLIME_MOVE_RIGHT = "Slime_move_right";
    public const string SLIME_MOVE_LEFT = "Slime_move_left";

    public const string SLIME_MOVE_DOWN = "Slime_move_down";

    

    public const string SLIME_ATTACK_UP = "Slime_attack_up";
    public const string SLIME_ATTACK_RIGHT = "Slime_attack_right";
    public const string SLIME_ATTACK_DOWN = "Slime_attack_down";
    public const string SLIME_ATTACK_LEFT = "Slime_attack_left";

    


    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
    }
}
