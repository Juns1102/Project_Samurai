using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private Animator anim;
    private PlayerAnimation pAnim;
    private Rigidbody2D rb;
    private PlayerMove pm;

    private void Awake() {
        anim = GetComponent<Animator>();
        pAnim = GetComponent<PlayerAnimation>();
        rb = GetComponent<Rigidbody2D>();
        pm = GetComponent<PlayerMove>();
    }

    private void OnAttack() {
        if (!pm.IsJumping()) {
            if(pAnim.State1()) {
                anim.SetTrigger("Attack1");
            }
            else if (pAnim.State2()) {
                pAnim.Ready2();
            }
            else if(pAnim.State3()) {
                pAnim.Ready3();
            }
        }
    }
}
