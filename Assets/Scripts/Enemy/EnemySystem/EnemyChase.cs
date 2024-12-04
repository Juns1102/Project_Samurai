using UnityEngine;

public class EnemyChase : MonoBehaviour
{
    [SerializeField]
    int dir;
    [SerializeField]
    int speed;

    Transform parent;
    Rigidbody2D body;
    Animator anim;
    GameObject wolf;
    WolfAttack wolfAttack;

    void Start()
    {
        dir = 0;
        parent = GetComponentInParent<Transform>();
        anim = GetComponentInParent<Animator>();
        body = GetComponentInParent<Rigidbody2D>();
        wolf = transform.parent.gameObject;
        wolfAttack = wolf.GetComponentInChildren<WolfAttack>();
    }

    private void FixedUpdate() {
        if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Attack") && !wolfAttack.GetStop()) {
            body.linearVelocityX = dir * speed;
        }
    }

    private void LateUpdate() {
        if(!wolfAttack.GetStop()){
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
            //float distance = Vector2.Distance(transform.position, player.transform.position);
            //if(distance < 10).....
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
