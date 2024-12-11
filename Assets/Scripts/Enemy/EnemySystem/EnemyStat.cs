using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class EnemyStat : MonoBehaviour
{
    [SerializeField]
    float damage;
    [SerializeField]
    bool activeAttack;
    [SerializeField]
    float hearts;
    [SerializeField]
    float maxHearts;
    bool die;
    bool inv;
    [SerializeField]
    bool trap;
    Slider slider;
    [SerializeField]
    GameObject hpCanvas;
    Animator anim;
    [SerializeField]
    bool sword;
    [SerializeField]
    bool boss;
    [SerializeField]
    int attackFunc;
    [SerializeField]
    PlayerAttack playerAttack;
    SpriteRenderer sr;
    Trap monsterTrap;

    private void Start() {
        activeAttack = true;
        playerAttack = GameObject.Find("Player").GetComponent<PlayerAttack>();
        if(!sword){
            if(boss){
                slider = GameObject.Find("Boss_Hp_Bar").GetComponent<Slider>();
                slider.DOValue(hearts/maxHearts, 0f, false);
            }
            else{
                slider = transform.GetChild(2).GetChild(0).GetComponent<Slider>();
                monsterTrap = GetComponent<Trap>();
            }
            hpCanvas = slider.transform.parent.gameObject;
            anim = GetComponent<Animator>();
            sr = GetComponent<SpriteRenderer>();
        }
    }

    private void Update() {
        if(!sword && !boss){
            hpCanvas.GetComponent<RectTransform>().rotation = Quaternion.Euler(0, 0, 0);
        }
    }

    public float GetDamage(){
        if(activeAttack){
            return damage;
        }
        else{
            return 0;
        }
    }
    public void Damaged(float damage){
        if(!sword && !inv){
            if(attackFunc != playerAttack.GetAF()){
                attackFunc = playerAttack.GetAF();
                hearts -= damage;
                GameManager.Instance.EDamaged();
                slider.DOValue(hearts/maxHearts, 0.3f, false);
                DamagedEffect();
                if(hearts <= 0){
                    die = true;
                    if(trap){
                        monsterTrap.OnTrap();
                    }
                    anim.SetTrigger("Die");
                }
            }
        }
    }

    private void SetInv(){
        inv = true;
    }

    private void EndInv(){
        inv = false;
    }

    private void DamagedEffect(){
    sr.color = new Color(255f/255f, 130f/255f, 130f/255f);
    transform.DOShakePosition(0.1f, new Vector2(0.3f, 0), 10, 90, false, true, ShakeRandomnessMode.Full).OnComplete(() => sr.color = new Color(1, 1, 1)).SetLink(gameObject);
    }
    
    public bool GetDie(){
        return die;
    }

    public void Die(){
        Destroy(gameObject);
    }

    public float GetHearts(){
        return hearts;
    }

    public void AfterAttack(){
        activeAttack = false;
    }

    public void SetAttack(){
        activeAttack = true;
    }
}
