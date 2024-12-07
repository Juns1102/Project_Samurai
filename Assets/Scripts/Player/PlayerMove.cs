using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem;
using DarkTonic.MasterAudio;

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
    bool isJump;
    [SerializeField]
    Vector2 targetPos;
    [SerializeField]
    bool userCtr;

    public float inputValue;

    private Rigidbody2D body;
    private Animator anim;
    private SpriteRenderer spriter;
    private PlayerParing pParing;
    private UIController uiController;


    private void Awake() {
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriter = GetComponent<SpriteRenderer>();
        pParing = GetComponent<PlayerParing>();
        uiController = GetComponent<UIController>();
    }

    private void Start() {
        SceneChangeMove();
    }

    private void FixedUpdate() {
        if(userCtr && !uiController.GetPause()){
            Move();
        }
    }

    private void LateUpdate() {
        if(userCtr && !uiController.GetPause()){
            MoveAnim();
        }
    }

    public bool GetUserCtr(){
        return userCtr;
    }

    private void SceneChangeMove(){
        userCtr = false;
        anim.SetFloat("Speed", 1);
        transform.DOMove((Vector2)transform.position + targetPos, 1f).SetEase(Ease.Linear).OnComplete(() => userCtr = true).SetLink(gameObject);
    }

    private void OnMove(InputValue value) {
        inputValue = value.Get<Vector2>().x;
    }

    private void Move(){
        if (inputValue == 0f) {
            speed = startSpeed;
        }
        if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Attack1") && 
            !anim.GetCurrentAnimatorStateInfo(0).IsName("Attack2") && 
            !anim.GetCurrentAnimatorStateInfo(0).IsName("Attack3") &&
            !anim.GetCurrentAnimatorStateInfo(0).IsName("Guard") &&
            !anim.GetCurrentAnimatorStateInfo(0).IsName("Paring_Fail") &&
            !anim.GetCurrentAnimatorStateInfo(0).IsName("Paring_Success")) {
            pParing.CancleParing();
            body.linearVelocityX = inputValue * speed;
            if (speed < maxSpeed) {
                speed += 0.5f;
            }
        }
    }

    private void Move_Sound1(){
        MasterAudio.PlaySound3DAtTransform("Walk_1", transform);
    }

    private void Move_Sound2(){
        MasterAudio.PlaySound3DAtTransform("Walk_2", transform);
    }

    private void MoveAnim(){
        anim.SetFloat("Speed", Mathf.Abs(inputValue));

        if (inputValue != 0f && (!anim.GetCurrentAnimatorStateInfo(0).IsName("Attack1") &&
            !anim.GetCurrentAnimatorStateInfo(0).IsName("Attack2") &&
            !anim.GetCurrentAnimatorStateInfo(0).IsName("Attack3")) && !pParing.GetParing()) {
            if (inputValue < 0f) {
                gameObject.transform.rotation = Quaternion.Euler(0, 180, 0);
            }
            else {
                gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
            }
        }
    }

    private void OnJump() {
        if (!isJump) {
            body.AddForceY(jumpPower, ForceMode2D.Impulse);
            isJump = true;
        }
    }

    public bool IsJumping() {
        return isJump;
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.contacts[0].normal.y > 0.7f) {
            isJump = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.layer == LayerMask.NameToLayer("SceneTrigger")){
            SceneChangeMove();
        }
    }
}
