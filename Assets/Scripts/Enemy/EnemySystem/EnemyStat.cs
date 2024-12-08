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
    Slider slider;
    [SerializeField]
    GameObject hpCanvas;
    Animator anim;
    [SerializeField]
    bool sword;

    private void Start() {
        activeAttack = true;
        if(!sword){
            slider = transform.GetChild(2).GetChild(0).GetComponent<Slider>();
            hpCanvas = slider.transform.parent.gameObject;
            anim = GetComponent<Animator>();
        }
    }

    private void Update() {
        if(!sword){
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
        if(!sword){
            hearts -= damage;
            
            slider.DOValue(hearts/maxHearts, 0.3f, false);
            if(hearts <= 0){
                die = true;
                anim.SetTrigger("Die");
            }
        }
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
