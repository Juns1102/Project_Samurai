using UnityEngine;

public class KaraCasaAttack : MonoBehaviour
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
    GameObject KaraCasa;
    Animator anim;
    EnemyChase enemyChase;
    EnemyStat eStat;
    KaraCasaState karaCasaState;

    void Awake()
    {
        KaraCasa = transform.parent.gameObject;
        anim = GetComponentInParent<Animator>();
        enemyChase = KaraCasa.GetComponentInChildren<EnemyChase>();
        timeAfterCoolTime = coolTime;
        bc2d = GetComponent<BoxCollider2D>();
        eStat = GetComponentInParent<EnemyStat>();
        karaCasaState = GetComponent<KaraCasaState>();
    }

    void Update()
    {
        RaycastHit2D hit = Physics2D.CircleCast(transform.position, distance, Vector2.up, 0, 1 << LayerMask.NameToLayer("Player"));
        Debug.DrawRay(transform.position, transform.right * distance, Color.yellow);
        Debug.DrawRay(transform.position, -transform.right * distance, Color.yellow);
        if(!anim.GetCurrentAnimatorStateInfo(0).IsName("Attack") && 
                !anim.GetCurrentAnimatorStateInfo(0).IsName("AttackMode")){
            timeAfterCoolTime += Time.deltaTime;
        }
        
        if (hit.collider != null) {
            stop = true;
            KaraCasa.GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;
            if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Attack") && 
                !anim.GetCurrentAnimatorStateInfo(0).IsName("AttackMode") &&
                timeAfterCoolTime >= coolTime) {
                    if(!eStat.GetDie()){
                        AttackMode();
                        timeAfterCoolTime = 0f;
                    }
            }
        }
        else {
            stop = false;
        }
    }
    
    private void AttackMode(){
        anim.SetTrigger("AttackMode");
    }

    

    public bool GetStop(){
        return stop;
    }
}
