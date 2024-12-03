using UnityEngine;
using DG.Tweening;

public class UIManager : Singleton<UIManager> {
    public GameObject fade;

    public void FadeIn(){
        fade.GetComponent<CanvasGroup>().DOFade(0, 1f).SetEase(Ease.Linear).OnComplete(() => {fade.SetActive(false);});
    }

    public void FadeOut(){
        fade.SetActive(true);
        fade.GetComponent<CanvasGroup>().DOFade(1, 1f).SetEase(Ease.Linear).OnComplete(() => {SceneChanger.Instance.SceneChange(); FadeIn();});
    }
}
