using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class SwordsFunc : MonoBehaviour
{
    GameObject sword1;
    GameObject sword2;
    GameObject sword3;
    GameObject sword4;
    GameObject sword5;
    GameObject boss;
    GameObject player;

    void Start()
    {
        sword1 = transform.Find("Sword1").gameObject;
        sword2 = transform.Find("Sword2").gameObject;
        sword3 = transform.Find("Sword3").gameObject;
        sword4 = transform.Find("Sword4").gameObject;
        sword5 = transform.Find("Sword5").gameObject;
        boss = GameObject.Find("Boss");
        player = GameObject.Find("Player");
    }

    IEnumerator Attack(){
        transform.position = boss.transform.position;
        if(transform.position.x > player.transform.position.x){
            sword1.GetComponent<SpriteRenderer>().flipX = false;
        }
        else{
            sword1.GetComponent<SpriteRenderer>().flipX = true;
        }
        sword1.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        if(transform.position.x > player.transform.position.x){
            sword2.GetComponent<SpriteRenderer>().flipX = false;
        }
        else{
            sword2.GetComponent<SpriteRenderer>().flipX = true;
        }
        sword2.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        if(transform.position.x > player.transform.position.x){
            sword3.GetComponent<SpriteRenderer>().flipX = false;
        }
        else{
            sword3.GetComponent<SpriteRenderer>().flipX = true;
        }
        sword3.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        if(transform.position.x > player.transform.position.x){
            sword4.GetComponent<SpriteRenderer>().flipX = false;
        }
        else{
            sword4.GetComponent<SpriteRenderer>().flipX = true;
        }
        sword4.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        if(transform.position.x > player.transform.position.x){
            sword5.GetComponent<SpriteRenderer>().flipX = false;
        }
        else{
            sword5.GetComponent<SpriteRenderer>().flipX = true;
        }
        sword5.SetActive(true);
    }
}
