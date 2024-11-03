using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class SpawnPoint {
    [SerializeField]
    private float x;
    [SerializeField]
    private float y;
}

public class SceneChanger : Singleton<SceneChanger> {
    [SerializeField]
    private SpawnPoint[] spawnPoints;

    public void SceneChange() {
        if(SceneManager.GetActiveScene().name == "First Stage") {
            SceneManager.LoadScene("Second Stage");
        }
    }
}
