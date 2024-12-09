using System;
using UnityEngine;
using UnityEngine.InputSystem;
using DarkTonic.MasterAudio;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField]
    private int attackFunc;
    private Animator anim;
    private PlayerAnimation pAnim;
    private PlayerMove pm;
    private UIController uIController;

    private void Awake() {
        anim = GetComponent<Animator>();
        pAnim = GetComponent<PlayerAnimation>();
        pm = GetComponent<PlayerMove>();
        uIController = GetComponent<UIController>();
    }

    private void OnAttack() {
        if(!uIController.GetPause() && pm.GetUserCtr()){
            Attack();
        }
    }

    private void Attack(){
        if (!pm.IsJumping()) {
            if(pAnim.State1()) {
                anim.SetTrigger("Attack1");
            }
            else if (pAnim.State2()) {
                pAnim.Ready2();
            }
            else if(pAnim.State3()) {
                pAnim.Ready3();
            }
        }
    }

    private void AttackFunc(){
        attackFunc++;
    }

    public int GetAF(){
        return attackFunc;
    }

    private void Attack_1_Sound(){
        MasterAudio.PlaySound3DAtTransform("Attack_1", transform);
    }

    private void Attack_2_Sound(){
        MasterAudio.PlaySound3DAtTransform("Attack_2", transform);
    }

    private void Attack_3_Sound(){
        MasterAudio.PlaySound3DAtTransform("Attack_3", transform);
    }
    
}
