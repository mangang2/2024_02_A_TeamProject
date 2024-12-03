using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationManager : MonoBehaviour
{
    public Animator animator;
    public PlayerStateMachine stateMachine;

    //�ִϸ����� �Ķ���� �̸����� ����� ����
    private const string PARAM_IS_MOVEING = "IsMoving";
    private const string PARAM_IS_RUNNING = "IsRunning";

    public void Update()
    {
        UpdateAnimationState();
    }

    private void UpdateAnimationState()
    {
        //���� ���¿� ���� �ִϸ��̼� �Ķ���� ����
        if(stateMachine.currentState != null)
        {
            //��� bool �Ķ���͸� �ʱ�ȭ 
            ResetAllBoolParameters();

            //���� ���¿� ���� �ش��ϴ� �ִϸ��̼� �Ķ���� ����
            switch (stateMachine.currentState)
            {
                case IdleState:
                    //Idle ���´� ��� �Ķ���Ͱ� false�� ����
                    break;

                case MoveingState:
                    animator.SetBool(PARAM_IS_MOVEING, true);
                    //�޸��� �Է� Ȯ��
                    if(Input.GetKey(KeyCode.LeftShift))
                    {
                        animator.SetBool(PARAM_IS_RUNNING, true);
                    }
                    break;
            }
        }
    }

    //��� bool �Ķ���� �ʱ�ȭ
    private void ResetAllBoolParameters()
    {
        animator.SetBool(PARAM_IS_MOVEING, false);
        animator.SetBool(PARAM_IS_RUNNING, false);
    }
}
