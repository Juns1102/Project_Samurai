using UnityEngine;

public class BringerChase : MonoBehaviour
{
    [SerializeField]
    int dir;
    [SerializeField]
    int speed;

    Transform parent;
    Rigidbody2D body;
    Animator anim;
    GameObject bringer;
    BringerAttack bringerAttack;
    EnemyStat eStat;

    void Start()
    {
        dir = 0;
        parent = GetComponentInParent<Transform>();
        anim = GetComponentInParent<Animator>();
        body = GetComponentInParent<Rigidbody2D>();
        bringer = transform.parent.gameObject;
        bringerAttack = bringer.GetComponentInChildren<BringerAttack>();
        eStat = GetComponentInParent<EnemyStat>();
    }

    private void FixedUpdate() {
        if(!eStat.GetDie()){
            Move();
        }
    }

    private void LateUpdate() {
        if(!eStat.GetDie()){
            MoveAnim();
        }
    }

    private void Move(){
        if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Attack") && !bringerAttack.GetStop()) {
            body.linearVelocityX = dir * speed;
        }
    }

    private void MoveAnim(){
        if(!bringerAttack.GetStop()){
            anim.SetFloat("Speed", Mathf.Abs(dir));
        }
        else{
            anim.SetFloat("Speed", Mathf.Abs(0));
        }

        if (dir != 0f && !anim.GetCurrentAnimatorStateInfo(0).IsName("Attack")) {
            if (dir < 0f) {
                body.transform.rotation = Quaternion.Euler(0, 180, 0);
            }
            else {
                body.transform.rotation = Quaternion.Euler(0, 0, 0);
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision) {
        if (collision.CompareTag("Player")) {
            dir = collision.transform.position.x < parent.position.x ? -1 : 1;
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.CompareTag("Player")) {
            dir = 0;
        }
    }

    public void SetDir(int dir){
        this.dir = dir;
    }

    public int GetDir(){
        return dir;
    }
}
