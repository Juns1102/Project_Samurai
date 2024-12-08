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
    [SerializeField]
    float DodgeSpeed;
    [SerializeField]
    float MaxDodgeSpeed;
    [SerializeField]
    float dashMaxCoolTime;
    [SerializeField]
    float dashCoolTime;

    public float inputValue;
    bool sprint;
    bool skill;
    [SerializeField]
    float skill_Dir;
    bool activeSkill;

    private Rigidbody2D body;
    private Animator anim;
    private SpriteRenderer spriter;
    private PlayerParing pParing;
    private UIController uiController;
    private CapsuleCollider2D cp2d;
    private PlayerAnimation pAnim;
    [SerializeField]
    private GameObject skill_Effect;


    private void Awake() {
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriter = GetComponent<SpriteRenderer>();
        pParing = GetComponent<PlayerParing>();
        uiController = GetComponent<UIController>();
        cp2d = GetComponent<CapsuleCollider2D>();
        pAnim = GetComponent<PlayerAnimation>();
        skill_Effect = GameObject.Find("Skill_Effect");
        activeSkill = true;
    }

    private void Start() {
        SceneChangeMove();
    }

    private void FixedUpdate() {
        if(userCtr && !uiController.GetPause()){
            Move();
        }
        if(isJump && body.linearVelocityY < 0){
            anim.SetTrigger("Jump_Down");
        }
        dashCoolTime += Time.fixedDeltaTime;
    }

    private void LateUpdate() {
        if(userCtr && !uiController.GetPause()){
            MoveAnim();
        }
    }

    public bool GetUserCtr(){
        return userCtr;
    }

    public void UserCtr_F(){
        userCtr = false;
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
                skill_Dir = inputValue;
                gameObject.transform.rotation = Quaternion.Euler(0, 180, 0);
            }
            else {
                skill_Dir = inputValue;
                gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
            }
        }
    }

    private void SetEffect(){
        skill_Effect.transform.Find("Skill_Effect").gameObject.SetActive(true);
    }

    private void OnSkill(){
        if(userCtr && !isJump && activeSkill){
            activeSkill = false;
            GameManager.Instance.ResetSkill();
            anim.SetTrigger("Skill");
        }
    }

    public void ActiveSkill(){
        activeSkill = true;
    }

    private void Skill(){
        body.linearVelocity = Vector2.zero;
        userCtr = false;
        skill = true;
        pAnim.ResetAttack();
        pParing.EndParing();
    }

    private void Skill_Step(){
        transform.Translate(new Vector2(9, 0));
    }

    private void EndSkill(){
        userCtr = true;
        skill = false;
    }

    public bool GetSkill(){
        return skill;
    }

    private void OnSprint(){
        if(userCtr && dashCoolTime >= dashMaxCoolTime){
            if(inputValue != 0){
                dashCoolTime = 0;
                anim.SetTrigger("Dodge");
            }
        }
    }

    private void Sprint(){
        userCtr = false;
        sprint = true;
        body.gravityScale = 0;
        pAnim.ResetAttack();
        pParing.EndParing();
        body.linearVelocity = Vector3.zero;
        body.AddForceX(DodgeSpeed * inputValue, ForceMode2D.Impulse);
    }

    private void EndSprint(){
        userCtr = true;
        sprint = false;
        body.gravityScale = 2;
    }

    public bool GetSprint(){
        return sprint;
    }

    private void OnJump() {
        if (!isJump && userCtr && anim.GetCurrentAnimatorStateInfo(0).IsName("LocoMotion")) {
            anim.ResetTrigger("Jump_Down");
            anim.SetTrigger("Jump_Up");
            body.AddForceY(jumpPower, ForceMode2D.Impulse);
            isJump = true;
            anim.SetBool("Jump", true);
        }
    }

    public bool IsJumping() {
        return isJump;
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.contacts[0].normal.y > 0.7f) {
            isJump = false;
            anim.SetBool("Jump", false);
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.layer == LayerMask.NameToLayer("SceneTrigger")){
            SceneChangeMove();
        }
    }
}
