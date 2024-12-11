using System.Collections;
using System.Collections.Generic;
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
    Vector3 targetPos;

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

    IEnumerator Attack2(){
        int randomNumber = Random.Range(0, 1);
        targetPos = player.transform.position;
        for(int i=0; i < 5; i++){
            if(i == 2){
                if(transform.position.x > player.transform.position.x){
                    sword1.GetComponent<SpriteRenderer>().flipX = false;
                }
                else{
                    sword1.GetComponent<SpriteRenderer>().flipX = true;
                }
                sword1.GetComponent<Sword>().TargetPos(targetPos);
                sword1.SetActive(true);
            }
            else if(i == 1){
                if(randomNumber == 0){
                    if(transform.position.x > player.transform.position.x){
                        sword2.GetComponent<SpriteRenderer>().flipX = false;
                    }
                    else{
                        sword2.GetComponent<SpriteRenderer>().flipX = true;
                    }
                    sword2.GetComponent<Sword>().TargetPos(targetPos);
                    sword2.SetActive(true);
                }
                else{
                    if(transform.position.x > player.transform.position.x){
                        sword3.GetComponent<SpriteRenderer>().flipX = false;
                    }
                    else{
                        sword3.GetComponent<SpriteRenderer>().flipX = true;
                    }
                    sword3.GetComponent<Sword>().TargetPos(targetPos);
                    sword3.SetActive(true);
                }
            }
            else if(i == 3){
                if(randomNumber == 0){
                    if(transform.position.x > player.transform.position.x){
                        sword3.GetComponent<SpriteRenderer>().flipX = false;
                    }
                    else{
                        sword3.GetComponent<SpriteRenderer>().flipX = true;
                    }
                    sword3.GetComponent<Sword>().TargetPos(targetPos);
                    sword3.SetActive(true);
                }
                else{
                    if(transform.position.x > player.transform.position.x){
                        sword2.GetComponent<SpriteRenderer>().flipX = false;
                    }
                    else{
                        sword2.GetComponent<SpriteRenderer>().flipX = true;
                    }
                    sword2.GetComponent<Sword>().TargetPos(targetPos);
                    sword2.SetActive(true);
                }
            }
            else if(i == 0){
                if(randomNumber == 0){
                    if(transform.position.x > player.transform.position.x){
                        sword4.GetComponent<SpriteRenderer>().flipX = false;
                    }
                    else{
                        sword4.GetComponent<SpriteRenderer>().flipX = true;
                    }
                    sword4.GetComponent<Sword>().TargetPos(targetPos);
                    sword4.SetActive(true);
                }
                else{
                    if(transform.position.x > player.transform.position.x){
                        sword5.GetComponent<SpriteRenderer>().flipX = false;
                    }
                    else{
                        sword5.GetComponent<SpriteRenderer>().flipX = true;
                    }
                    sword5.GetComponent<Sword>().TargetPos(targetPos);
                    sword5.SetActive(true);
                }
            }
            else if(i == 4){
                if(randomNumber == 0){
                    if(transform.position.x > player.transform.position.x){
                        sword5.GetComponent<SpriteRenderer>().flipX = false;
                    }
                    else{
                        sword5.GetComponent<SpriteRenderer>().flipX = true;
                    }
                    sword5.GetComponent<Sword>().TargetPos(targetPos);
                    sword5.SetActive(true);
                }
                else{
                    if(transform.position.x > player.transform.position.x){
                        sword4.GetComponent<SpriteRenderer>().flipX = false;
                    }
                    else{
                        sword4.GetComponent<SpriteRenderer>().flipX = true;
                    }
                    sword4.GetComponent<Sword>().TargetPos(targetPos);
                    sword4.SetActive(true);
                }
            }
            yield return new WaitForSeconds(0.5f);
        }//-32
    }
    
    private void Update() {
        if(boss.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Die")){
            Destroy(gameObject);
        }
    }
}
