using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour {
    #region Singleton
    private static SceneChanger instance;

    public static SceneChanger Instance{
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
    public void SceneChange() {
        if(SceneManager.GetActiveScene().name == "First Stage") {
            SceneManager.LoadScene("Second Stage");
        }
        else if(SceneManager.GetActiveScene().name == "Second Stage") {
            UIManager.Instance.OnBossHpBar();
            SceneManager.LoadScene("Final Stage");
        }
    }
}
