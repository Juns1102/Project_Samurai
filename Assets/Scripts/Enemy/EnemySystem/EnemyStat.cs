using UnityEngine;
using UnityEngine.UI;

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
    Slider slider;
    [SerializeField]
    GameObject hpCanvas;

    private void Start() {
        activeAttack = true;
        slider = transform.GetChild(2).GetChild(0).GetComponent<Slider>();
        hpCanvas = slider.transform.parent.gameObject;
    }

    private void Update() {
        if(transform.rotation.y != 0){
            hpCanvas.GetComponent<RectTransform>().rotation = Quaternion.Euler(0, 0, 0);
        }
        else{
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
        hearts -= damage;
        
        slider.value = hearts/maxHearts;
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
