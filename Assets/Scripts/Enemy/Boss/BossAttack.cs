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
    GameObject player;

    void Start()
    {
        boss = transform.parent.gameObject;
        anim = GetComponentInParent<Animator>();
        enemyChase = boss.GetComponentInChildren<BossChase>();
        bc2d = GetComponent<BoxCollider2D>();
        eStat = GetComponentInParent<EnemyStat>();
        sf = GameObject.Find("Swords").GetComponent<SwordsFunc>();
        sf2 = GameObject.Find("Swords2").GetComponent<SwordsFunc>();
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D hit = Physics2D.CircleCast(transform.position, distance, Vector2.up, 0, 1 << LayerMask.NameToLayer("Player"));
        Debug.DrawRay(transform.position, transform.right * distance, Color.yellow);
        Debug.DrawRay(transform.position, -transform.right * distance, Color.yellow);
        if(!anim.GetCurrentAnimatorStateInfo(0).IsName("Attack_1") &&
         !anim.GetCurrentAnimatorStateInfo(0).IsName("Attack_2") &&
         !anim.GetCurrentAnimatorStateInfo(0).IsName("Attack_3") &&
         !anim.GetCurrentAnimatorStateInfo(0).IsName("Attack_4") &&
         !anim.GetCurrentAnimatorStateInfo(0).IsName("Die")){
            timeAfterCoolTime += Time.deltaTime;
        }

        if (hit.collider != null) {
            if(Mathf.Abs(player.transform.position.x - transform.position.x) <= 6){
                stop = true;
            }
            else{
                stop = false;
                if(timeAfterCoolTime >= coolTime){
                    anim.SetTrigger("Attack2");
                    timeAfterCoolTime = 0f;
                }
            }
            boss.GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;
            if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Attack_1") &&
            !anim.GetCurrentAnimatorStateInfo(0).IsName("Attack_2") &&
            !anim.GetCurrentAnimatorStateInfo(0).IsName("Attack_3") && 
            !anim.GetCurrentAnimatorStateInfo(0).IsName("Attack_4") &&
            !anim.GetCurrentAnimatorStateInfo(0).IsName("Die") &&
            timeAfterCoolTime >= coolTime && stop) {
                if(!eStat.GetDie()){
                    Attack();
                    timeAfterCoolTime = 0f;
                }
            }
        }
        // else {
        //     stop = false;
        // }
    }

    

    private void Attack(){
        percent = Random.Range(0, 100);
        eStat.SetAttack();
        if(percent < 20){
            anim.SetTrigger("Attack1");
        }
        else if(percent >= 20 && percent < 40){
            anim.SetTrigger("Attack2");
        }
        else if(percent >= 40 && percent < 60){
            anim.SetTrigger("Attack3");
            sf.StartCoroutine("Attack");
        }
        else if(percent >= 60 && percent < 80){
            anim.SetTrigger("Attack3");
            sf2.StartCoroutine("Attack2");
        }
        else if(percent >= 80){
            anim.SetTrigger("Attack4");
        }
    }//0.3 0.75 1

    public bool GetStop(){
        return stop;
    }
}
