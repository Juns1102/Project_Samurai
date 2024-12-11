using UnityEngine;
using DG.Tweening;

public class SwordEffect : MonoBehaviour
{
    [SerializeField]
    float force;
    float set1;
    bool step1;
    [SerializeField]
    GameObject player;
    [SerializeField]
    GameObject boss;
    [SerializeField]
    Vector2 resetPos;
    Rigidbody2D body;
    Vector3 direction;
    SpriteRenderer sr;
    BoxCollider2D bc2d;
    EnemyStat es;
    float dir;
    Vector3 targetPos;
    [SerializeField]
    Ease ease;

    private void Start() {
        player = GameObject.Find("Player");
        boss = GameObject.Find("Boss");
        body = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        bc2d = GetComponent<BoxCollider2D>();
        sr.DOFade(0, 0);
        es = GetComponent<EnemyStat>();
    }

    void FixedUpdate()
    {
        if(boss != null){
            if(player.transform.position.x < boss.transform.position.x){
                dir = -1f;
            }
            else{
                dir = 1f;
            }
        }
        else{
            Destroy(transform.parent.gameObject);
        }
    }

    public void SetAttack(){
        sr.DOFade(0, 0).SetLink(gameObject);
        es.SetAttack();
        if(boss != null){
            if (dir < 0f) {
                gameObject.transform.rotation = Quaternion.Euler(0, 180, 0);
            }
            else {
                gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
            }
            transform.position = boss.transform.position;
        }
        sr.DOFade(1, 0.2f).SetLink(gameObject);
        bc2d.enabled = true;
        if(dir < 0f){
            body.DOMoveX(-14f, 1f).SetEase(ease).OnComplete(() => gameObject.SetActive(false)).SetLink(gameObject);
        }
        else{
            body.DOMoveX(14f, 1f).SetEase(ease).OnComplete(() => gameObject.SetActive(false)).SetLink(gameObject);
        }
    }
}
