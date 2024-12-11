using UnityEngine;
using DG.Tweening;
using Unity.VisualScripting;
using DarkTonic.MasterAudio;

public class BossState : MonoBehaviour
{
    [SerializeField]
    int dir;
    [SerializeField]
    Vector2 targetPos;
    [SerializeField]
    BossChase bossChase;
    BoxCollider2D bc2d;
    SpriteRenderer sr;
    EnemyStat eStat;
    GameObject swords;
    GameObject player;
    [SerializeField]
    Ease ease;
    SwordEffectFunc sef;


    void Start()
    {
        bossChase = GetComponentInChildren<BossChase>();
        bc2d = transform.GetChild(0).gameObject.GetComponent<BoxCollider2D>();
        sr = GetComponent<SpriteRenderer>();
        eStat = GetComponent<EnemyStat>();
        swords = GameObject.Find("Swords");
        player = GameObject.Find("Player");
        sef = GameObject.Find("SwordEffects").GetComponent<SwordEffectFunc>();
    }
    
    private void SetAttackDir(){
        dir = bossChase.GetDir();
        if (dir < 0f) {
                transform.rotation = Quaternion.Euler(0, 180, 0);
            }
        else {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        if(Mathf.Abs(player.transform.position.x - transform.position.x) >= 4){
            transform.DOMove((Vector2)transform.position + targetPos * new Vector2(dir, 1), 0.2f).SetEase(ease).SetLink(gameObject);
        }
    }

    private void SetAttack(){
        bc2d.enabled = true;
    }

    private void EndAttack(){
        bc2d.enabled = false;
    }

    private void Attack_Sound(){
        //MasterAudio.PlaySound3DAtTransform("Wolf_Attack", transform);
    }

    private void SetAttack4(){
        if(player.transform.position.x < transform.position.x){
            transform.position = new Vector3(14, transform.position.y, 0);
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else{
            transform.position = new Vector3(-14, transform.position.y, 0);
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }

    private void SetSwordE1(){
        sef.Effect1();
    }
    private void SetSwordE2(){
        sef.Effect2();
    }
    private void SetSwordE3(){
        sef.Effect3();
    }

    private void SetPos(){
        if(player.transform.position.x < transform.position.x){
            transform.position = player.transform.position + new Vector3(-2, 0, 0);
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else{
            transform.position = player.transform.position + new Vector3(2, 0, 0);
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
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
