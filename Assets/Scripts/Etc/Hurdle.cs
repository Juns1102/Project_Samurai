using UnityEngine;

public class Hurdle : MonoBehaviour
{
    [SerializeField]
    Vector3 targetPos;
    
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.layer == LayerMask.NameToLayer("Player") || other.gameObject.layer == LayerMask.NameToLayer("Enemy")){
            Debug.Log("asdf");
            other.transform.position = targetPos;
        }
    }
}
