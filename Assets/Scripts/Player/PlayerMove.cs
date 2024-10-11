using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    [SerializeField]
    float startSpeed;
    [SerializeField]
    float maxSpeed;
    [SerializeField] 
    float speed;
    [SerializeField]
    float jumpPower;
    [SerializeField]
    bool change;

    public float inputValue;

    Rigidbody2D body;
    Animator anim;
    SpriteRenderer spriter;

    private void Awake() {
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriter = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate() {
        if (inputValue == 0f) {
            speed = startSpeed;
        }
        if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Attack1") && 
            !anim.GetCurrentAnimatorStateInfo(0).IsName("Attack2") && 
            !anim.GetCurrentAnimatorStateInfo(0).IsName("Attack3")) {
            body.linearVelocityX = inputValue * speed;
            if (speed < maxSpeed) {
                speed += 0.5f;
            }
        }
    }

    private void LateUpdate() {
        anim.SetFloat("Speed", Mathf.Abs(inputValue));

        if (inputValue != 0f && (!anim.GetCurrentAnimatorStateInfo(0).IsName("Attack1") &&
            !anim.GetCurrentAnimatorStateInfo(0).IsName("Attack2") &&
            !anim.GetCurrentAnimatorStateInfo(0).IsName("Attack3"))) {
            if (inputValue < 0f) {
                gameObject.transform.rotation = Quaternion.Euler(0, 180, 0);
            }
            else {
                gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
            }
        }
    }

    private void OnMove(InputValue value) {
        inputValue = value.Get<Vector2>().x;
        
    }

    private void OnJump() {
        body.AddForceY(jumpPower, ForceMode2D.Impulse);
    }
}
