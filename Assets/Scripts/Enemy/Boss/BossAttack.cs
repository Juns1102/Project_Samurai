using DG.Tweening;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class BossAttack : MonoBehaviour
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
    GameObject boss;
    Animator anim;
    BossChase enemyChase;
    EnemyStat eStat;
    SwordsFunc sf;

    void Start()
    {
        boss = transform.parent.gameObject;
        anim = GetComponentInParent<Animator>();
        enemyChase = boss.GetComponentInChildren<BossChase>();
        timeAfterCoolTime = coolTime;
        bc2d = GetComponent<BoxCollider2D>();
        eStat = GetComponentInParent<EnemyStat>();
        sf = GameObject.Find("Swords").GetComponent<SwordsFunc>();
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D hit = Physics2D.CircleCast(transform.position, distance, Vector2.up, 0, 1 << LayerMask.NameToLayer("Player"));
        Debug.DrawRay(transform.position, transform.right * distance, Color.yellow);
        Debug.DrawRay(transform.position, -transform.right * distance, Color.yellow);
        if(!anim.GetCurrentAnimatorStateInfo(0).IsName("Attack_3")){
            timeAfterCoolTime += Time.deltaTime;
        }

        if (hit.collider != null) {
            stop = true;
            boss.GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;
            if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Attack_3") && 
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
        anim.SetTrigger("Attack1");
        //sf.StartCoroutine("Attack");
    }//0.3 0.75 1

    public bool GetStop(){
        return stop;
    }
}
