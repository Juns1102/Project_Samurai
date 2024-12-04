using UnityEngine;

public class EnemyStat : MonoBehaviour
{
    [SerializeField]
    float damage;
    [SerializeField]
    float hearts;
    public float GetDamage(){
        return damage;
    }
    public void Damaged(float damage){
        hearts -= damage;
    }
    public float GetHearts(){
        return hearts;
    }
}
