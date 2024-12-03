using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationManager : MonoBehaviour
{
    public Animator animator;
    public PlayerStateMachine stateMachine;

    //애니메이터 파라미터 이름들을 상수로 정의
    private const string PARAM_IS_MOVEING = "IsMoving";
    private const string PARAM_IS_RUNNING = "IsRunning";

    public void Update()
    {
        UpdateAnimationState();
    }

    private void UpdateAnimationState()
    {
        //현재 상태에 따라 애니메이셔 파라미터 설정
        if(stateMachine.currentState != null)
        {
            //모든 bool 파라미터를 초기화 
            ResetAllBoolParameters();

            //현재 상태에 따라 해당하는 애니메이션 파라미터 설정
            switch (stateMachine.currentState)
            {
                case IdleState:
                    //Idle 상태는 모든 파라미터가 false인 상태
                    break;

                case MoveingState:
                    animator.SetBool(PARAM_IS_MOVEING, true);
                    //달리기 입력 확인
                    if(Input.GetKey(KeyCode.LeftShift))
                    {
                        animator.SetBool(PARAM_IS_RUNNING, true);
                    }
                    break;
            }
        }
    }

    //모든 bool 파라미터 초기화
    private void ResetAllBoolParameters()
    {
        animator.SetBool(PARAM_IS_MOVEING, false);
        animator.SetBool(PARAM_IS_RUNNING, false);
    }
}
