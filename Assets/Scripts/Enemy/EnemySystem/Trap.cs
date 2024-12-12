using UnityEngine;
using UnityEngine.Rendering;

public class Trap : MonoBehaviour
{
    [SerializeField]
    private bool monster;
    [SerializeField]
    private int gap;
    public GameObject originEnemy1;
    public GameObject originEnemy2;
    private GameObject enemy1;
    private GameObject enemy2;

    public void OnTrap(){
        enemy1 = Instantiate(originEnemy1, transform.position + new Vector3(-gap, 0, 0), Quaternion.identity);
        enemy2 = Instantiate(originEnemy2, transform.position + new Vector3(gap, 0, 0), Quaternion.identity);
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player") && !monster){
            OnTrap();
        }
    }
}
