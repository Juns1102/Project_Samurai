using UnityEngine;
using DG.Tweening;
using Unity.VisualScripting;
using DarkTonic.MasterAudio;

public class WolfState : MonoBehaviour
{
    [SerializeField]
    int dir;
    [SerializeField]
    Vector2 targetPos;
    [SerializeField]
    EnemyChase enemyChase;
    BoxCollider2D bc2d;
    SpriteRenderer sr;
    EnemyStat eStat;

    private void Start(){
        enemyChase = GetComponentInChildren<EnemyChase>();
        bc2d = transform.GetChild(0).gameObject.GetComponent<BoxCollider2D>();
        sr = GetComponent<SpriteRenderer>();
        eStat = GetComponent<EnemyStat>();
    }

    private void SetAttackDir(){
        dir = enemyChase.GetDir();
    }
    
    private void SetAttack(){
        bc2d.enabled = true;
        transform.DOMove((Vector2)transform.position + targetPos * new Vector2(dir, 1), 0.35f).SetEase(Ease.Linear).OnComplete(() => EndAttack()).SetLink(gameObject);
    }

    private void EndAttack(){
        transform.DOMove((Vector2)transform.position + targetPos * new Vector2(dir * 0.6f, -1), 0.3f).SetEase(Ease.Linear).OnComplete(() => bc2d.enabled = false).SetLink(gameObject);
    }

    private void Attack_Sound(){
        MasterAudio.PlaySound3DAtTransform("Wolf_Attack", transform);
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
