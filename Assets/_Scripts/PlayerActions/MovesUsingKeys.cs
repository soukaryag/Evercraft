using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class MovesUsingKeys : MonoBehaviour
{
    public float moveSpeed = 0.075f;
    public float dashSpeed = 5f;
    public bool dash = true;
    public int dashCooldown = 0;
    public bool loadingScreen = false;
    public Rigidbody2D rb;
    Vector2 movement;
    private Animator animator;
    public Transform pfDashEffect;
    public GameObject PlayerCamera;
    public GameObject ParticleSystemObject;
    protected ParticleSystem ps;
    public Text PlayerNameText;
    PhotonView view;

    private int yRoomIndex;
    private int xRoomIndex;

    private int roomSize = 30;
    private int roomMiddle = 20;

    private void Awake() {
        Physics2D.queriesHitTriggers = false;
        view = GetComponent<PhotonView>();
        animator = GetComponent<Animator>();

        if (view.IsMine) {
            PlayerCamera.SetActive(true);
            if (PlayerPrefs.GetString("playerName") != null && PlayerPrefs.GetString("playerName") != "u") {
                PlayerNameText.text = PlayerPrefs.GetString("playerName");
            } else {
                PlayerNameText.text = "";
            }
            
        }
    }

    private void Start() {
        ps = ParticleSystemObject.GetComponent<ParticleSystem>();
        ps.Stop();

        yRoomIndex = Mathf.RoundToInt(transform.position.y / roomSize);
        xRoomIndex = Mathf.RoundToInt(transform.position.x / roomSize);

        if (!view.IsMine) {
            PlayerNameText.text = view.Owner.NickName;
            PlayerNameText.color = Color.red;
        }
    }

    void Update()
    {
        if (loadingScreen) {
            animator.SetFloat("Horizontal", 1.0f);
            animator.SetFloat("Vertical", 0.0f);
            animator.SetFloat("Speed", moveSpeed);
            ps.Play();
        }

        if (view.IsMine) {
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");

            if (movement.x < -0.01f) {
                animator.SetBool("Is_Moving_Side", true);
                Flip(-1);
            } else {
                Flip(1);
                animator.SetBool("Is_Moving_Side", true);
            }

            if (movement.x == 0) {
                animator.SetBool("Is_Moving_Side", false);
            }

            if (movement.sqrMagnitude == 0) {
                if (ps != null) { //From Austin: this kept throwing errors
                    ps.Stop();
                }
                
            } else {
                if (ps != null) { //Also
                    ps.Play();
                }
                
            }

            animator.SetFloat("Horizontal", movement.x);
            animator.SetFloat("Vertical", movement.y);
            animator.SetFloat("Speed", movement.sqrMagnitude);
        }
    }

    void FixedUpdate() {
        if (loadingScreen) {
            transform.position = transform.position + (Vector3)(new Vector2(1, 0) * 0.06f);
            return;
        }

        if (view.IsMine) {
            if (dashCooldown == 0) {
                dash = true;
            } else {
                dashCooldown--;
            }

            if (Input.GetKey(KeyCode.Space) && dash && movement.sqrMagnitude != 0) {
                // Transform dashEffectTransform = Instantiate(pfDashEffect, transform.position, Quaternion.identity);

                float dashDistance = MaxDashDistance(movement, dashSpeed);
                // dashEffectTransform.transform.position = new Vector3(
                //     dashEffectTransform.transform.position.x + movement.x/2, 
                //     dashEffectTransform.transform.position.y + movement.y/2,
                //     dashEffectTransform.transform.position.z
                // );

                // dashEffectTransform.eulerAngles = new Vector3(0, 0, GetAngleFromVectorFloat(movement));
                // dashEffectTransform.localScale = new Vector3(dashDistance, 1f, 1f);

                transform.position = transform.position + (Vector3)(movement.normalized * dashDistance);
                dashCooldown = 30;
                dash = false;
            } else {
                transform.position = transform.position + (Vector3)(movement.normalized * moveSpeed);
            }
        }
    }

    private float MaxDashDistance(Vector2 dir, float distance) {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, dir, distance);
        if (hit.distance < 0.1f) {
            hit = Physics2D.Raycast(transform.position + new Vector3(0, 0.1f, 0), dir, distance);
        }

        if (hit.collider != null) {
            return Mathf.Max(0.0f, Mathf.Min(dashSpeed, hit.distance - 1.0f));
        } else {
            return dashSpeed;
        }
    }

    public static float GetAngleFromVectorFloat(Vector2 dir) {
        dir = dir.normalized;
        float n = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        if (n < 0) n += 360;

        return n;
    }

    private void Flip (int coeff) {
        Vector3 theScale = transform.localScale;
        theScale.x = coeff * Mathf.Abs(theScale.x);
        transform.localScale = theScale;
    }
}
