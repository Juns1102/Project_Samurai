using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField]
    private int force;

    private bool attack1;
    private bool attack2;
    private bool attack3;

    private bool ready2;
    private bool ready3;

    private Animator anim;
    private Rigidbody2D rb;

    private void Awake() {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        attack1 = false;
        attack2 = false;
        attack3 = false;
        ready2 = false;
        ready3 = false;
    }

    public bool State1() {
        if(!attack1 && !attack2 && !attack3) {
            return true;
        }
        else {
            return false;
        }
    }

    public bool State2() {
        if (attack1 && !attack2 && !attack3) {
            return true;
        }
        else {
            return false;
        }
    }

    public bool State3() {
        if (!attack1 && attack2 && !attack3) {
            return true;
        }
        else {
            return false;
        }
    }

    private void Start1() {
        attack1 = true;
        attack2 = false;
        attack3 = false;
    }

    private void Start2() {
        attack1 = false;
        attack2 = true;
        attack3 = false;
    }

    private void Start3() {
        attack1 = false;
        attack2 = false;
        attack3 = true;
    }

    private void End1() {
        attack1 = false;
        attack2 = false;
        attack3 = false;
        if (ready2) {
            anim.SetTrigger("Attack2");
            ready2 = false;
        }
    }

    private void End2() {
        attack1 = false;
        attack2 = false;
        attack3 = false;
        if (ready3) {
            anim.SetTrigger("Attack3");
            ready3 = false;
        }
    }

    private void End3() {
        attack1 = false;
        attack2 = false;
        attack3 = false;
    }

    public void Ready2() {
        ready2 = true;
    }

    public void Ready3() {
        ready3 = true;
    }

    private void AttackForce() {
        if (transform.rotation.y == 0) {
            rb.linearVelocity = Vector2.zero;
            rb.AddForceX(force, ForceMode2D.Force);
        }
        else {
            rb.linearVelocity = Vector2.zero;
            rb.AddForceX(-force, ForceMode2D.Force);
        }
    }

    private void SetAttack() {
        gameObject.GetComponentInChildren<BoxCollider2D>().enabled = true;
    }

    private void EndAttack() {
        gameObject.GetComponentInChildren<BoxCollider2D>().enabled = false;
    }

    public void ResetAttack(){
        attack1 = false;
        attack2 = false;
        attack3 = false;
        ready2 = false;
        ready3 = false;
        gameObject.GetComponentInChildren<BoxCollider2D>().enabled = false;
    }
}
