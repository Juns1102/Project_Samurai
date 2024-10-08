using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    Animator anim;

    private void Awake() {
        anim = GetComponentInParent<Animator>();
    }

    private void OnAttack() {
        if(!anim.GetCurrentAnimatorStateInfo(0).IsName("Attack1") && 
            !anim.GetCurrentAnimatorStateInfo(0).IsName("Attack2") && 
            !anim.GetCurrentAnimatorStateInfo(0).IsName("Attack3")) {
            anim.SetTrigger("Attack1");
        }
        else if (anim.GetCurrentAnimatorStateInfo(0).IsName("Attack1") &&
            !anim.GetCurrentAnimatorStateInfo(0).IsName("Attack2") &&
            !anim.GetCurrentAnimatorStateInfo(0).IsName("Attack3")) {
            anim.SetTrigger("Attack2");
        }
        else if(!anim.GetCurrentAnimatorStateInfo(0).IsName("Attack1") &&
            anim.GetCurrentAnimatorStateInfo(0).IsName("Attack2") &&
            !anim.GetCurrentAnimatorStateInfo(0).IsName("Attack3")) {
            anim.SetTrigger("Attack3");
        }
    }
}
