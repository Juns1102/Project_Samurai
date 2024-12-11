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
        boss = GameObject.Find("Boss").gameObject;
        player = GameObject.Find("Player").gameObject;
    }

    public void Effect1(){
        if(player.transform.position.x < boss.transform.position.x){
            swordE1.transform.position = new Vector3(14, boss.transform.position.y, 0);
            swordE1.transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else{
            swordE1.transform.position = new Vector3(-14, boss.transform.position.y, 0);
            swordE1.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        swordE1.SetActive(true);
        swordE1.GetComponent<SwordEffect>().SetAttack();
    }

    public void Effect2(){
        if(player.transform.position.x < boss.transform.position.x){
            swordE2.transform.position = new Vector3(14, boss.transform.position.y, 0);
            swordE2.transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else{
            swordE2.transform.position = new Vector3(-14, boss.transform.position.y, 0);
            swordE2.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        swordE2.SetActive(true);
        swordE2.GetComponent<SwordEffect>().SetAttack();
    }

    public void Effect3(){
        if(player.transform.position.x < boss.transform.position.x){
            swordE3.transform.position = new Vector3(14, boss.transform.position.y, 0);
            swordE3.transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else{
            swordE3.transform.position = new Vector3(-14, boss.transform.position.y, 0);
            swordE3.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        swordE3.SetActive(true);
        swordE3.GetComponent<SwordEffect>().SetAttack();
    }

}
