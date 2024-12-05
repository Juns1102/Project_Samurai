using UnityEngine;
using DG.Tweening;

public class PlayerParing : MonoBehaviour
{
    [SerializeField]
    private bool paring;
    [SerializeField]
    private float paring_BackWards;
    [SerializeField]
    private bool onParing;
    private bool timing;
    private bool guard;
    private bool damaged;
    private Animator anim;
    private PlayerAnimation pAnim;
    private BoxCollider2D bc2d1;
    private BoxCollider2D bc2d2;
    private Rigidbody2D body;
    private SpriteRenderer sr;

    void Start()
    {
        anim = GetComponent<Animator>();
        bc2d1 = transform.GetChild(0).GetComponent<BoxCollider2D>();
        bc2d2 = transform.GetChild(1).GetComponent<BoxCollider2D>();
        body = GetComponent<Rigidbody2D>();
        pAnim = GetComponent<PlayerAnimation>();
        sr = GetComponent<SpriteRenderer>();
    }

    private void OnParing(){
        if(!damaged){
            body.linearVelocity = Vector2.zero;
            anim.SetTrigger("Guard");
        }
    }

    private void SetParing(){
        timing = true;
        bc2d1.enabled = false;
        bc2d2.enabled = true;
        pAnim.ResetAttack();
        onParing = true;
    }

    private void ParingFail(){
        timing = false;
    }

    private void EndParing(){
        bc2d2.enabled = false;
        onParing = false;
    }

    private void BackWards_S(float dir){
        body.AddForceX(dir * paring_BackWards * 0.4f, ForceMode2D.Impulse);
    }

    private void BackWards_F(float dir){
        body.AddForceX(dir * paring_BackWards, ForceMode2D.Impulse);
    }

    private void Damaged(float damage){
        GameManager.Instance.Damaged(damage);
        if(damage != 0){
            sr.color = new Color(255f/255f, 130f/255f, 130f/255f);
            transform.DOShakePosition(0.1f, new Vector2(0.3f, 0), 10, 90, false, true, ShakeRandomnessMode.Full).OnComplete(() => 
            {sr.color = new Color(1, 1, 1); guard = false; damaged = false;}).SetLink(gameObject);
        }
    }

    public void CancleParing(){
        onParing = false;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.CompareTag("EnemyAttack")){
            if(anim.GetCurrentAnimatorStateInfo(0).IsName("Guard")){
                if(timing){
                    anim.SetTrigger("Paring_S");
                    if(transform.position.x < other.transform.position.x){
                        BackWards_S(-1f);
                    }
                    else{
                        BackWards_S(1f);
                    }
                    other.GetComponentInParent<EnemyStat>().AfterAttack();
                }
                else{
                    anim.SetTrigger("Paring_F");
                    if(transform.position.x < other.transform.position.x){
                        BackWards_F(-1f);
                    }
                    else{
                        BackWards_F(1f);
                    }
                    guard = true;
                    if(!damaged){
                        GameManager.Instance.Damaged(other.GetComponentInParent<EnemyStat>().GetDamage()*0.5f);
                        guard = false; 
                        damaged = false;
                    }
                    other.GetComponentInParent<EnemyStat>().AfterAttack();
                }
            }
            else{
                if(!onParing){
                    damaged = true;
                    if(!guard){
                        Damaged(other.GetComponentInParent<EnemyStat>().GetDamage());
                        other.GetComponentInParent<EnemyStat>().AfterAttack();
                    }
                }
            }
        }
    }
}
