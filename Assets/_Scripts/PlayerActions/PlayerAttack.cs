using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerAttack : MonoBehaviour
{
    private Animator anim;
    public float attackTime;
    private float startTimeAttack = 0.2f;
    PhotonView view;

    private void Start()
    {
        anim = GetComponent<Animator>();
        view = GetComponent<PhotonView>();
    }

    void Update()
    {
        if (view.IsMine) {
            if (attackTime <= 0 && !anim.GetBool("Is_Attacking")) {
                if(Input.GetMouseButtonDown(0))
                {
                    //anim.SetBool("Is_Attacking", true);
                    // Collider2D[] damage = Physics2D.OverlapCircleAll( attackLocation.position, attackRange, enemies );

                    // for (int i = 0; i < damage.Length; i++)
                    // {
                    //     Destroy( damage[i].gameObject );
                    // }
                    attackTime = startTimeAttack;
                }
            } else if (attackTime <= 0 && anim.GetBool("Is_Attacking")) {
                anim.SetBool("Is_Attacking", false);
            } else if (anim.GetBool("Is_Attacking")) {
                attackTime -= Time.deltaTime;
            }
        }
    }
}