using UnityEngine;

public class SceneTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Player")) {
            UIManager.Instance.FadeOut();
        }
    }
}
