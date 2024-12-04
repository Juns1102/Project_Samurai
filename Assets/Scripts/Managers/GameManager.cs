using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [SerializeField]
    private float hearts;

    public void Damaged(float damage){
        hearts -= damage;
        Debug.Log(hearts);
    }
}
