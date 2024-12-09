using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using DarkTonic.MasterAudio;

public class GameManager : Singleton<GameManager>
{
    [SerializeField]
    private float hearts;
    [SerializeField]
    private float maxHearts;
    [SerializeField]
    private float skillGuage;
    [SerializeField]
    private float maxSkillGuage;
    private Slider slider;
    private Slider skillSlider;

    private void Start() {
        slider = UIManager.Instance.GethpBar().GetComponent<Slider>();
        skillSlider = UIManager.Instance.GetSkillBar().GetComponent<Slider>();
    }

    public void Damaged(float damage){
        hearts -= damage;
        MasterAudio.PlaySound3DAtTransform("Damaged", GameObject.Find("Player").transform);
        slider.DOValue(hearts/maxHearts, 0.3f, false);
        if(hearts <= 0){
            MasterAudio.StopAllOfSound("KaraCasa");
            GameObject.Find("Player").GetComponent<Animator>().SetTrigger("Dead");
            GameObject.Find("Player").GetComponent<PlayerMove>().UserCtr_F();
        }
    }

    public void Parried(float guage){
        skillGuage += guage;
        skillSlider.DOValue(skillGuage/maxSkillGuage, 0.3f, false);
        if(skillGuage >= maxSkillGuage){
            GameObject.Find("Player").GetComponent<PlayerMove>().ActiveSkill();
        }
    }

    public void EDamaged(){
        skillGuage++;
        skillSlider.DOValue(skillGuage/maxSkillGuage, 0.3f, false);
        if(skillGuage >= maxSkillGuage){
            GameObject.Find("Player").GetComponent<PlayerMove>().ActiveSkill();
        }
    }

    public void ResetSkill(){
        skillGuage = 0;
        skillSlider.DOValue(skillGuage/maxSkillGuage, 0.3f, false);
    }
}
