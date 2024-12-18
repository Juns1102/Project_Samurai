using UnityEngine;
using DG.Tweening;
using DarkTonic.MasterAudio;

public class KaraCasaState : MonoBehaviour
{
    [SerializeField]
    int dir;
    bool attackMode;
    [SerializeField]
    Transform targetPos;
    [SerializeField]
    KaraCasaChase karaCasaChase;
    CircleCollider2D bc2d;
    SpriteRenderer sr;
    EnemyStat eStat;
    [SerializeField]
    float destination;
    [SerializeField]
    float gap;
    [SerializeField]
    Ease ease;
    Animator anim;
    [SerializeField]
    int shield;
    [SerializeField]
    int maxShield;
    bool shieldFunc;


    void Start()
    {
        karaCasaChase = GetComponentInChildren<KaraCasaChase>();
        bc2d = transform.GetChild(0).gameObject.GetComponent<CircleCollider2D>();
        sr = GetComponent<SpriteRenderer>();
        eStat = GetComponent<EnemyStat>();
        targetPos = GameObject.Find("Player").transform;
        anim = GetComponent<Animator>();
        shield = maxShield;
        shieldFunc = true;
    }

    private void SetAttackDir(){
        dir = karaCasaChase.GetDir();
    }

    private void GetDestPos(){
        destination = targetPos.position.x + gap * dir;
        if(dir == 1){
            gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else{
            gameObject.transform.rotation = Quaternion.Euler(0, 180, 0);
        }
    }

    public void SetAttackDest(){
        if(attackMode){
            shieldFunc = true;
            bc2d.enabled = true;
            eStat.SetAttack();
            SetAttackDir();
            GetDestPos();
            transform.DOMove(new Vector2(destination, transform.position.y), 1.2f).SetEase(ease).SetLink(gameObject);
        }
    }

    public void SetAttackMode(){
        attackMode = true;
    }

    private void Attack(){
        eStat.SetAttack();
        anim.SetTrigger("Attack");
        SetAttackMode();
        SetAttackDest();
        MasterAudio.PlaySound3DAtTransform("KaraCasa", transform);
    }//0.3 0.75 1

    private void AttackCancle(){
        transform.DOMove(new Vector2(transform.position.x, transform.position.y), 1.2f).SetLink(gameObject);
        bc2d.enabled = false;
        attackMode = false;
        //MasterAudio.PauseAllSoundsOfTransform(transform);
        MasterAudio.StopAllSoundsOfTransform(transform);
        anim.SetTrigger("AttackCancle");
    }

    // private void Attack_Sound(){
    //     MasterAudio.PlaySound3DAtTransform("Wolf_Attack", transform);
    // }

    public void Parried(){
        if(shieldFunc){
            shieldFunc = false;
            shield--;
            if(shield <= 0){
                AttackCancle();
                shield = maxShield;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.layer == LayerMask.NameToLayer("PlayerAttack")){
            if(!anim.GetCurrentAnimatorStateInfo(0).IsName("Attack")){
                if(other.CompareTag("PlayerAttack")){
                    if(!eStat.GetDie()){
                        eStat.Damaged(1);
                    }
                }
                if(other.CompareTag("Skill_Effect")){
                    if(!eStat.GetDie()){
                        eStat.Damaged(5);
                    }
                }
            }
        }
    }
}
