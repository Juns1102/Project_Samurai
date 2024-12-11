using UnityEngine;
using DG.Tweening;

public class SwordEffect : MonoBehaviour
{
    [SerializeField]
    float force;
    [SerializeField]
    GameObject player;
    [SerializeField]
    GameObject boss;
    [SerializeField]
    Rigidbody2D body;
    SpriteRenderer sr;
    BoxCollider2D bc2d;
    EnemyStat es;
    [SerializeField]
    float dir;
    [SerializeField]
    Ease ease;
    bool lastSword;

    private void Awake() {
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
        if(boss == null){
            Destroy(transform.parent.gameObject);
        }
    }

    public void SetAttack(){
        es.SetAttack();
        sr.DOFade(1, 0.2f).SetLink(gameObject);
        bc2d.enabled = true;
        if(player.transform.position.x < boss.transform.position.x){
            body.DOMoveX(-14f, force).SetEase(ease).OnComplete(() => {sr.DOFade(0, 0).SetLink(gameObject); gameObject.SetActive(false);}).SetLink(gameObject);
        }
        else{
            body.DOMoveX(14f, force).SetEase(ease).OnComplete(() => {sr.DOFade(0, 0).SetLink(gameObject); gameObject.SetActive(false);}).SetLink(gameObject);
        }
    }
}
