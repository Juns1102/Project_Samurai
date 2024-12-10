using UnityEngine;
using DG.Tweening;
using System.Collections;
using Unity.Cinemachine;

public class Sword : MonoBehaviour
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
    Camera cam;
    float dir;
    [SerializeField]
    int mode;
    Vector3 targetPos;

    void Start()
    {
        player = GameObject.Find("Player");
        boss = GameObject.Find("Boss");
        body = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        bc2d = GetComponent<BoxCollider2D>();
        sr.DOFade(0, 0);
        es = GetComponent<EnemyStat>();
        cam = Camera.main;
    }

    private void FixedUpdate() {
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
        if(step1){
            if(mode == 0){
                if(set1 < 0.5f){
                    set1 += Time.deltaTime;
                    Vector3 rotation = transform.position - player.transform.position + new Vector3(0, 0.75f, 0);
                    float rotationZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
                    direction = player.transform.position + new Vector3(0, 0.75f, 0) - transform.position;
                    transform.DORotate(new Vector3(0, 0, rotationZ + 90), 0.4f).SetLink(gameObject);
                }
                else{
                    step1 = false;
                    set1 = 0;
                    Attack();
                }
            }
            else{
                if(set1 < 2.1f){
                    set1 += Time.deltaTime;
                    Vector3 rotation = transform.position - player.transform.position + new Vector3(0, 0.75f, 0);
                    float rotationZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
                    direction = player.transform.position + new Vector3(0, 0.75f, 0) - transform.position;
                    transform.DORotate(new Vector3(0, 0, rotationZ + 90), 0.4f).SetLink(gameObject);
                }
                else{
                    step1 = false;
                    set1 = 0;
                    Attack();
                }
            }
        }
    }

    public void TargetPos(Vector3 targetPos){
        this.targetPos = targetPos;
    }

    void SetAttack(){
        es.SetAttack();
        sr.DOFade(1, 1f).OnComplete(()=> step1 = true).SetLink(gameObject);
        if(boss != null){
            if(mode == 0){
                transform.position = boss.transform.position + new Vector3(resetPos.x * dir, resetPos.y, 0);
            }
            else{
                transform.position = targetPos + new Vector3(resetPos.x * dir, resetPos.y, 0);
            }
        }
        bc2d.enabled = true;
    }

    void Attack(){
        body.AddForce(new Vector2(direction.x, direction.y).normalized * force, ForceMode2D.Impulse);
    }

    // private void OnCollisionEnter2D(Collision2D other) {
    //     if(other.gameObject.layer == LayerMask.NameToLayer("Platform")){
    //         body.linearVelocity = Vector2.zero;
    //         StartCoroutine("EndAttack");
    //     }
    // }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.layer == LayerMask.NameToLayer("Platform")){
            es.AfterAttack();
            body.linearVelocity = Vector2.zero;
            bc2d.enabled = false;
            StartCoroutine(EndAttack());
        }
    }

    IEnumerator EndAttack(){
        yield return new WaitForSeconds(3f);
        sr.DOFade(0, 0.1f).OnComplete(()=> {gameObject.SetActive(false); transform.rotation = Quaternion.Euler(0, 0, 0);}).SetLink(gameObject);
    }
}
