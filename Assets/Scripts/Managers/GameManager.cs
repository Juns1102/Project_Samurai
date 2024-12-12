using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using DarkTonic.MasterAudio;

public class GameManager : MonoBehaviour
{
    #region Singleton
    private static GameManager instance;

    public static GameManager Instance{
        get{
            if(null == instance){
                return null;
            }
            return instance;
        }
    }

    private void Awake() {
        if(instance == null){
            instance = this;
            if(transform.parent != null && transform.root != null){
                DontDestroyOnLoad(this.transform.root.gameObject);
            }
            else{
                DontDestroyOnLoad(this.gameObject);
            }
        }
        else{
            Destroy(this.gameObject);
        }
    }
    #endregion

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
    [SerializeField]
    int healCount;
    [SerializeField]
    int maxHealCount;
    [SerializeField]
    int healSize;
    private bool ending;

    
    float time;
    int min;
    int sec;
    int temp;

    private void Start() {
        slider = UIManager.Instance.GethpBar().GetComponent<Slider>();
        skillSlider = UIManager.Instance.GetSkillBar().GetComponent<Slider>();
    }

    private void FixedUpdate(){
        time += Time.deltaTime;
        min = (int)time / 60;
        sec = (int)time % 60;
        if(sec != temp){
            UIManager.Instance.SetTime(min, sec);
        }
        temp = sec;
    }

    public void Reset(){
        hearts = maxHearts;
        slider.DOValue(hearts/maxHearts, 0f, false);
        skillGuage = maxSkillGuage;
        skillSlider.DOValue(skillGuage/maxSkillGuage, 0.3f, false);
        healCount = maxHealCount;
        UIManager.Instance.GetHeal(healCount);
        time = 0;
        min = 0;
        sec = 0;
        UIManager.Instance.SetTime(min, sec);
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

    public void Heal(){
        if(healCount > 0 && hearts < maxHearts){
            MasterAudio.PlaySound3DAtTransform("Heal", transform);
            healCount--;
            UIManager.Instance.GetHeal(healCount);
            hearts += healSize;
            if(hearts >= maxHearts){
                hearts = maxHearts;
            }
            slider.DOValue(hearts/maxHearts, 0.3f, false);
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
