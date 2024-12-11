using UnityEngine;

public class SwordEffectFunc : MonoBehaviour
{
    GameObject swordE1;
    GameObject swordE2;
    GameObject swordE3;
    GameObject boss;
    GameObject player;

    void Start()
    {
        swordE1 = transform.Find("SE1").gameObject;
        swordE2 = transform.Find("SE2").gameObject;
        swordE3 = transform.Find("SE3").gameObject;
        boss = GameObject.Find("Boss");
        player = GameObject.Find("Player");
    }

    public void Effect1(){
        swordE1.SetActive(true);
        swordE1.GetComponent<SwordEffect>().SetAttack();
    }

    public void Effect2(){
        swordE2.SetActive(true);
        swordE2.GetComponent<SwordEffect>().SetAttack();
    }

    public void Effect3(){
        swordE3.SetActive(true);
        swordE3.GetComponent<SwordEffect>().SetAttack();
    }

}
