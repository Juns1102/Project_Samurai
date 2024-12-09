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

    void Start()
    {
        bossChase = GetComponentInChildren<BossChase>();
        bc2d = transform.GetChild(0).gameObject.GetComponent<BoxCollider2D>();
        sr = GetComponent<SpriteRenderer>();
        eStat = GetComponent<EnemyStat>();
        swords = GameObject.Find("Swords");
        player = GameObject.Find("Player");
    }
    
    private void SetAttackDir(){
        dir = bossChase.GetDir();
        if (dir < 0f) {
                transform.rotation = Quaternion.Euler(0, 180, 0);
            }
        else {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        if(Mathf.Abs(player.transform.position.x - transform.position.x) >= 4 && Mathf.Abs(player.transform.position.x - transform.position.x) <= 7){
            transform.DOMove((Vector2)transform.position + targetPos * new Vector2(dir, 1), 0.2f).SetEase(ease);
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
