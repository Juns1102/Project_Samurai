using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerParing : MonoBehaviour
{
    private bool paring;
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnParing(InputValue value){
        paring = !paring;
    }
}
