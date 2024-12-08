using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class GameManager : Singleton<GameManager>
{
    [SerializeField]
    private float hearts;
    [SerializeField]
    private float maxHearts;
    private Slider slider;

    private void Start() {
        slider = UIManager.Instance.GethpBar().GetComponent<Slider>();
    }

    public void Damaged(float damage){
        hearts -= damage;
        slider.DOValue(hearts/maxHearts, 0.3f, false);
        if(hearts <= 0){
            GameObject.Find("Player").GetComponent<Animator>().SetTrigger("Dead");
            GameObject.Find("Player").GetComponent<PlayerMove>().UserCtr_F();
        }
    }
}
