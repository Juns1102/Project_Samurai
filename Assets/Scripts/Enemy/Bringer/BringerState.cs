using UnityEngine;
using DG.Tweening;
using Unity.VisualScripting;
using DarkTonic.MasterAudio;

public class BringerState : MonoBehaviour
{
    [SerializeField]
    int dir;
    [SerializeField]
    Vector2 targetPos;
    [SerializeField]
    BringerChase enemyChase;
    BoxCollider2D bc2d;
    SpriteRenderer sr;
    EnemyStat eStat;

    void Start()
    {
        enemyChase = GetComponentInChildren<BringerChase>();
        bc2d = transform.GetChild(0).gameObject.GetComponent<BoxCollider2D>();
        sr = GetComponent<SpriteRenderer>();
        eStat = GetComponent<EnemyStat>();
    }

    private void SetAttackDir(){
        dir = enemyChase.GetDir();
    }

    private void SetAttack(){
        bc2d.enabled = true;
    }

    private void EndAttack(){
        bc2d.enabled = false;
    }

    private void Attack_Sound(){
        MasterAudio.PlaySound3DAtTransform("Bringer_Attack", transform);
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
