using DG.Tweening;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class WolfAttack : EnemyAttack
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
    GameObject wolf;
    Animator anim;
    EnemyChase enemyChase;
    private void Awake()
    {
        wolf = transform.parent.gameObject;
        anim = GetComponentInParent<Animator>();
        enemyChase = wolf.GetComponentInChildren<EnemyChase>();
        timeAfterCoolTime = coolTime;
        bc2d = GetComponent<BoxCollider2D>();
    }

    private void Update(){
        RaycastHit2D hit = Physics2D.CircleCast(transform.position, distance, Vector2.up, 0, 1 << LayerMask.NameToLayer("Player"));
        Debug.DrawRay(transform.position, transform.right * distance, Color.yellow);
        Debug.DrawRay(transform.position, -transform.right * distance, Color.yellow);
        timeAfterCoolTime += Time.deltaTime;

        if (hit.collider != null) {
            stop = true;
            wolf.GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;
            if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Attack") && 
                !anim.GetCurrentAnimatorStateInfo(0).IsName("Dead") && 
                timeAfterCoolTime >= coolTime) {
                Attack();
                timeAfterCoolTime = 0f;
            }
        }
        else {
            stop = false;
        }
    }

    private void Attack(){
        anim.SetTrigger("Attack");
    }//0.3 0.75 1

    public bool GetStop(){
        return stop;
    }
}
