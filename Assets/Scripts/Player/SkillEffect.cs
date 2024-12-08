using UnityEngine;

public class SkillEffect : MonoBehaviour
{
    GameObject effect;
    GameObject player;
    Animator pAnim;

    private void Start() {
        effect = transform.Find("Skill_Effect_2").gameObject;
        player = GameObject.Find("Player");
        pAnim = player.GetComponent<Animator>();
    }

    private void StartEffect(){
        transform.parent.position = player.transform.position;
        transform.parent.rotation = player.transform.rotation;
        effect.SetActive(true);
    }   

    private void EndEffect(){
        effect.SetActive(false);
        pAnim.SetTrigger("Skill_A");
    } 

    private void EndSkill(){
        gameObject.SetActive(false);
    }
}
