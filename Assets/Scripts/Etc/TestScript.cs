using UnityEngine;
using DG.Tweening;

public class TestScript : MonoBehaviour
{
    public GameObject player;

    void FixedUpdate()
    {
        transform.DOMove(player.transform.position + new Vector3(-1, +1, 0), 0.3f);
    }
}
