using UnityEngine;
using DG.Tweening;
using System.Collections;

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
    float dir;

    void Start()
    {
        player = GameObject.Find("Player");
        boss = GameObject.Find("Boss");
        body = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        bc2d = GetComponent<BoxCollider2D>();
        resetPos = transform.position;
    }

    private void FixedUpdate() {
        if(player.transform.position.x < boss.transform.position.x){
            dir = -1f;
        }
        else{
            dir = 1f;
        }
        if(step1){
            if(set1 < 0.5f){
                set1 += Time.deltaTime;
                Vector3 rotation = transform.position - player.transform.position + new Vector3(0, 0.75f, 0);
                float rotationZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
                direction = player.transform.position + new Vector3(0, 0.75f, 0) - transform.position;
                transform.DORotate(new Vector3(0, 0, rotationZ + 90), 0.4f);
            }
            else{
                step1 = false;
                set1 = 0;
                Attack();
            }
        }
    }

    void SetAttack(){
        sr.DOFade(1, 0.5f).OnComplete(()=> step1 = true);
        transform.position = boss.transform.position + new Vector3(resetPos.x * dir, resetPos.y, 0);
        bc2d.enabled = true;
    }

    void SetAttack2(){
        Vector3 rotation = transform.position - player.transform.position + new Vector3(0, 0.75f, 0);
        float rotationZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        direction = player.transform.position + new Vector3(0, 0.75f, 0) - transform.position;
        transform.DORotate(new Vector3(0, 0, rotationZ + 90), 1f).OnComplete(()=> Attack()).SetLink(gameObject);
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
            body.linearVelocity = Vector2.zero;
            bc2d.enabled = false;
            StartCoroutine(EndAttack());
        }
    }

    IEnumerator EndAttack(){
        yield return new WaitForSeconds(3f);
        sr.DOFade(0, 0);
        gameObject.SetActive(false);
        transform.rotation = Quaternion.Euler(0, 0, 0);
    }
}
