using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [SerializeField]
    private int num;
    public void Game() {
        Debug.Log("siuuuu");
    }

    public void numUp() {
        num++;
    }
}
