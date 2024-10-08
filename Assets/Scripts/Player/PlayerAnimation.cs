using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator anim;

    private void Start() {
        anim = GetComponent<Animator>();
    }

    private void SetAttack() {
        gameObject.GetComponentInChildren<BoxCollider2D>().enabled = true;
    }

    private void EndAttack() {
        gameObject.GetComponentInChildren<BoxCollider2D>().enabled = false;
    }
}
