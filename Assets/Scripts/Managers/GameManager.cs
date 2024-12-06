using UnityEngine;
using UnityEngine.UI;

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
        slider.value = hearts/maxHearts;
    }
}
