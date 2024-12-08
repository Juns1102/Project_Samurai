using UnityEngine;

public class BringerAttack : MonoBehaviour
{
    [SerializeField]
    float coolTime;
    [SerializeField]
    float timeAfterCoolTime;
    [SerializeField]
    bool stop;
    [SerializeField]
    float distance;

    BoxCollider2D bc2d;
    GameObject bringer;
    Animator anim;
    EnemyChase enemyChase;
    EnemyStat eStat;

    void Start()
    {
        bringer = transform.parent.gameObject;
        anim = GetComponentInParent<Animator>();
        enemyChase = bringer.GetComponentInChildren<EnemyChase>();
        timeAfterCoolTime = coolTime;
        bc2d = GetComponent<BoxCollider2D>();
        eStat = GetComponentInParent<EnemyStat>();
    }

    void Update()
    {
        RaycastHit2D hit = Physics2D.CircleCast(transform.position, distance, Vector2.up, 0, 1 << LayerMask.NameToLayer("Player"));
        Debug.DrawRay(transform.position, transform.right * distance, Color.yellow);
        Debug.DrawRay(transform.position, -transform.right * distance, Color.yellow);
        timeAfterCoolTime += Time.deltaTime;

        if (hit.collider != null) {
            stop = true;
            bringer.GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;
            if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Attack") && 
                timeAfterCoolTime >= coolTime) {
                    if(!eStat.GetDie()){
                        Attack();
                        timeAfterCoolTime = 0f;
                    }
            }
        }
        else {
            stop = false;
        }
    }

    private void Attack(){
        eStat.SetAttack();
        anim.SetTrigger("Attack");
    }

    public bool GetStop(){
        return stop;
    }
}
