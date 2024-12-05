using UnityEngine;

public class EnemyStat : MonoBehaviour
{
    [SerializeField]
    float damage;
    [SerializeField]
    bool activeAttack;
    [SerializeField]
    float hearts;

    private void Start() {
        activeAttack = true;
    }

    public float GetDamage(){
        if(activeAttack){
            return damage;
        }
        else{
            return 0;
        }
    }
    public void Damaged(float damage){
        hearts -= damage;
    }

    public float GetHearts(){
        return hearts;
    }

    public void AfterAttack(){
        activeAttack = false;
    }

    public void SetAttack(){
        activeAttack = true;
    }
}
