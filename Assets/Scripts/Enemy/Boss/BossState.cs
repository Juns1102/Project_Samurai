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

    void Start()
    {
        bossChase = GetComponentInChildren<BossChase>();
        bc2d = transform.GetChild(0).gameObject.GetComponent<BoxCollider2D>();
        sr = GetComponent<SpriteRenderer>();
        eStat = GetComponent<EnemyStat>();
        swords = GameObject.Find("Swords");
    }
    
    private void SetAttackDir(){
        //dir = bossChase.GetDir();
    }

    private void SetAttack(){
        //bc2d.enabled = true;
        //transform.DOMove((Vector2)transform.position + targetPos * new Vector2(dir, 1), 0.35f).SetEase(Ease.Linear).OnComplete(() => EndAttack()).SetLink(gameObject);
    }

    private void EndAttack(){
       //transform.DOMove((Vector2)transform.position + targetPos * new Vector2(dir * 0.6f, -1), 0.3f).SetEase(Ease.Linear).OnComplete(() => bc2d.enabled = false).SetLink(gameObject);
    }

    private void Attack_Sound(){
        //MasterAudio.PlaySound3DAtTransform("Wolf_Attack", transform);
    }

    private void SetSwoard(){
        
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("PlayerAttack")){
            if(!eStat.GetDie()){
                eStat.Damaged(1);
                Damaged();
            }
        }
        if(other.CompareTag("Skill_Effect")){
            if(!eStat.GetDie()){
                eStat.Damaged(5);
                Damaged();
            }
        }
    }

    private void Damaged(){
        sr.color = new Color(255f/255f, 130f/255f, 130f/255f);
        transform.DOShakePosition(0.1f, new Vector2(0.3f, 0), 10, 90, false, true, ShakeRandomnessMode.Full).OnComplete(() => sr.color = new Color(1, 1, 1)).SetLink(gameObject);
    }
}
