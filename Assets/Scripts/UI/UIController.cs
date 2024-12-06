using UnityEngine;
using UnityEngine.InputSystem;

public class UIController : MonoBehaviour
{
    private bool isPause;

    void Start()
    {
        isPause = false;
    }

    private void OnEsc(){
        if(isPause){
            Time.timeScale = 1;
            isPause = false;
        }
        else{
            Time.timeScale = 0;
            isPause = true;
        }
    }

    public bool GetPause(){
        return isPause;
    }
}
