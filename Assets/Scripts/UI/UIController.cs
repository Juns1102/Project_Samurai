using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using DG.Tweening;
using TMPro;

public class UIController : MonoBehaviour
{
    private bool isPause;
    [SerializeField]
    private GameObject fade;
    private GameObject esc;
    [SerializeField]
    private GameObject managers;
    private GameObject hp_Bar;
    private GameObject boss_Hp_Bar;
    private GameObject skill_Bar;
    private GameObject timer;
    

    void Start()
    {
        if(SceneManager.GetActiveScene().name == "Title"){
            fade = transform.Find("FadeImg").gameObject;
            fade.GetComponent<CanvasGroup>().alpha = 1;
            FadeIn();
        }
        else if(SceneManager.GetActiveScene().name != "Title"){
            managers = GameObject.Find("Managers");
            hp_Bar = GameObject.Find("Hp_Bar");
            skill_Bar = GameObject.Find("Skill_Bar");
            timer = GameObject.Find("Timer");
            fade = GameObject.Find("FadeImg");
            fade.GetComponent<CanvasGroup>().alpha = 1;
            fade.SetActive(true);
            esc = GameObject.Find("UIManager").transform.Find("Esc").gameObject;
            esc.GetComponent<CanvasGroup>().alpha = 0;
            esc.SetActive(false);
            FadeIn();
        }
        isPause = false;
    }

    public void FadeIn(){
        fade.GetComponent<CanvasGroup>().DOFade(0, 1f).SetEase(Ease.Linear).OnComplete(() => {fade.SetActive(false);});
    }

    public void FadeOut(){
        fade.SetActive(true);
        fade.GetComponent<CanvasGroup>().DOFade(1, 1f).SetEase(Ease.Linear).OnComplete(() => {SceneManager.LoadScene("First Stage");});
    }

    public void GameStart(){
        FadeOut();
    }

    public void Quit(){
        Application.Quit();
    }

    public void Retry(){
        if(SceneManager.GetActiveScene().name == "Final Stage"){
            boss_Hp_Bar = GameObject.Find("Boss_Hp_Bar");
        }
        Time.timeScale = 1;
        esc.GetComponent<CanvasGroup>().DOFade(0f, 0.2f).SetEase(Ease.Linear);
        hp_Bar.GetComponent<CanvasGroup>().DOFade(0f, 0.2f).SetEase(Ease.Linear);
        skill_Bar.GetComponent<CanvasGroup>().DOFade(0f, 0.2f).SetEase(Ease.Linear);
        timer.GetComponent<CanvasGroup>().DOFade(0f, 0.2f).SetEase(Ease.Linear);
        if(SceneManager.GetActiveScene().name == "Final Stage"){
            boss_Hp_Bar.GetComponent<CanvasGroup>().DOFade(0f, 0.2f).SetEase(Ease.Linear);
        }
        fade.GetComponent<CanvasGroup>().DOFade(1, 1f).SetEase(Ease.Linear).OnComplete(() => {FadeInReset(); SceneManager.LoadScene("First Stage");});
    }

    private void FadeInReset(){
        hp_Bar.GetComponent<CanvasGroup>().DOFade(1f, 0.2f).SetEase(Ease.Linear);
        skill_Bar.GetComponent<CanvasGroup>().DOFade(1f, 0.2f).SetEase(Ease.Linear);
        if(SceneManager.GetActiveScene().name == "Final Stage"){
            boss_Hp_Bar.SetActive(false);
            boss_Hp_Bar.GetComponent<CanvasGroup>().DOFade(1f, 0.2f).SetEase(Ease.Linear);
        }
        timer.GetComponent<CanvasGroup>().DOFade(1f, 0.2f).SetEase(Ease.Linear);
        esc.transform.Find("Time").gameObject.SetActive(false);
        GameManager.Instance.Reset();
        fade.GetComponent<CanvasGroup>().DOFade(0, 1f).SetEase(Ease.Linear).OnComplete(() => {fade.SetActive(false);});
    }

    private void OnEsc(){
        if(SceneManager.GetActiveScene().name != "Title"){
            if(isPause){
                Time.timeScale = 1;
                isPause = false;
                timer.GetComponent<CanvasGroup>().DOFade(1f, 0.2f).SetEase(Ease.Linear);
                fade.GetComponent<CanvasGroup>().DOFade(0f, 0.2f).SetEase(Ease.Linear).OnComplete(() => fade.SetActive(false));
                esc.GetComponent<CanvasGroup>().DOFade(0f, 0.2f).SetEase(Ease.Linear).OnComplete(() => {esc.SetActive(false);});
            }
            else{
                isPause = true;
                fade.SetActive(true);
                esc.SetActive(true);
                timer.GetComponent<CanvasGroup>().DOFade(0f, 0.2f).SetEase(Ease.Linear);
                fade.GetComponent<CanvasGroup>().DOFade(0.85f, 0.2f).SetEase(Ease.Linear);
                esc.GetComponent<CanvasGroup>().DOFade(1f, 0.2f).SetEase(Ease.Linear).OnComplete(() => Time.timeScale = 0);
            }
        }
    }

    public bool GetPause(){
        return isPause;
    }
}
