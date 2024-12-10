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
    int percent;

    BoxCollider2D bc2d;
    GameObject boss;
    Animator anim;
    BossChase enemyChase;
    EnemyStat eStat;
    SwordsFunc sf;
    SwordsFunc sf2;

    void Start()
    {
        boss = transform.parent.gameObject;
        anim = GetComponentInParent<Animator>();
        enemyChase = boss.GetComponentInChildren<BossChase>();
        timeAfterCoolTime = coolTime;
        bc2d = GetComponent<BoxCollider2D>();
        eStat = GetComponentInParent<EnemyStat>();
        sf = GameObject.Find("Swords").GetComponent<SwordsFunc>();
        sf2 = GameObject.Find("Swords2").GetComponent<SwordsFunc>();
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D hit = Physics2D.CircleCast(transform.position, distance, Vector2.up, 0, 1 << LayerMask.NameToLayer("Player"));
        Debug.DrawRay(transform.position, transform.right * distance, Color.yellow);
        Debug.DrawRay(transform.position, -transform.right * distance, Color.yellow);
        if(!anim.GetCurrentAnimatorStateInfo(0).IsName("Attack_1") &&!anim.GetCurrentAnimatorStateInfo(0).IsName("Attack_3")){
            timeAfterCoolTime += Time.deltaTime;
        }

        if (hit.collider != null) {
            stop = true;
            boss.GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;
            if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Attack_1") &&
            !anim.GetCurrentAnimatorStateInfo(0).IsName("Attack_3") && 
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
        percent = Random.Range(0, 100);
        eStat.SetAttack();
        if(percent < 25){
            anim.SetTrigger("Attack1");
        }
        else if(percent >= 25 && percent < 50){
            anim.SetTrigger("Attack2");
        }
        else if(percent >= 50 && percent < 75){
            anim.SetTrigger("Attack3");
            sf.StartCoroutine("Attack");
        }
        else if(percent >= 75){
            anim.SetTrigger("Attack3");
            sf2.StartCoroutine("Attack2");
        }
    }//0.3 0.75 1

    public bool GetStop(){
        return stop;
    }
}
