using UnityEngine;
using DG.Tweening;

public class WolfState : MonoBehaviour
{
    [SerializeField]
    int dir;
    [SerializeField]
    int hearts;
    [SerializeField]
    Vector2 targetPos;
    EnemyChase enemyChase;

    private void Start(){
        enemyChase = GetComponentInChildren<EnemyChase>();
    }

    private void SetAttackDir(){
        dir = enemyChase.GetDir();
    }
    private void SetAttack(){
        transform.DOMove((Vector2)transform.position + targetPos * new Vector2(dir, 1), 0.35f).SetEase(Ease.Linear).OnComplete(() => EndAttack());
    }

    private void EndAttack(){
        transform.DOMove((Vector2)transform.position + targetPos * new Vector2(dir * 0.6f, -1), 0.3f).SetEase(Ease.Linear);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("PlayerAttack")){
            hearts--;
            Debug.Log(hearts);
            if(hearts <= 0){
                Destroy(gameObject);
            }
        }
    }
}
