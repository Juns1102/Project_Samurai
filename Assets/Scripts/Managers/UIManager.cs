using UnityEngine;
using DG.Tweening;
using TMPro;

public class UIManager : MonoBehaviour {
    #region Singleton
    private static UIManager instance;

    public static UIManager Instance{
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

    private GameObject fade;
    private GameObject hpBar;
    private GameObject boss_HpBar;
    private GameObject skillBar;
    private TextMeshProUGUI timer;
    private TextMeshProUGUI time;

    private void Start() {
        fade = transform.Find("FadeImg").gameObject;
        hpBar = transform.Find("Hp_Bar").gameObject;
        skillBar = transform.Find("Skill_Bar").gameObject;
        boss_HpBar = transform.Find("Boss_Hp_Bar").gameObject;
        timer = transform.Find("Timer").GetComponent<TextMeshProUGUI>();
        time = transform.Find("Esc").Find("Time").GetComponent<TextMeshProUGUI>();
    }

    public GameObject GethpBar(){
        return hpBar;
    }

    public GameObject GetSkillBar(){
        return skillBar;
    }

    public void OnBossHpBar(){
        boss_HpBar.SetActive(true);
    }

    public void SetTime(int min, int sec){
        timer.text = ((int)min).ToString().PadLeft(2, '0') + " : " + ((int)sec).ToString().PadLeft(2, '0');
        time.text = ((int)min).ToString().PadLeft(2, '0') + " : " + ((int)sec).ToString().PadLeft(2, '0');
    }

    public void FadeIn(){
        fade.GetComponent<CanvasGroup>().DOFade(0, 1f).SetEase(Ease.Linear).OnComplete(() => {fade.SetActive(false);});
    }

    public void FadeOut(){
        fade.SetActive(true);
        fade.GetComponent<CanvasGroup>().DOFade(1, 1f).SetEase(Ease.Linear).OnComplete(() => {SceneChanger.Instance.SceneChange(); FadeIn();});
    }
}
