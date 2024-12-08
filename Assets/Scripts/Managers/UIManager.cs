using UnityEngine;
using DG.Tweening;

public class UIManager : Singleton<UIManager> {
    private GameObject fade;
    private GameObject hpBar;
    private GameObject skillBar;

    private void Start() {
        fade = transform.Find("FadeImg").gameObject;
        hpBar = transform.Find("Hp_Bar").gameObject;
        skillBar = transform.Find("Skill_Bar").gameObject;
    }

    public GameObject GethpBar(){
        return hpBar;
    }

    public GameObject GetSkillBar(){
        return skillBar;
    }

    public void FadeIn(){
        fade.GetComponent<CanvasGroup>().DOFade(0, 1f).SetEase(Ease.Linear).OnComplete(() => {fade.SetActive(false);});
    }

    public void FadeOut(){
        fade.SetActive(true);
        fade.GetComponent<CanvasGroup>().DOFade(1, 1f).SetEase(Ease.Linear).OnComplete(() => {SceneChanger.Instance.SceneChange(); FadeIn();});
    }
}
