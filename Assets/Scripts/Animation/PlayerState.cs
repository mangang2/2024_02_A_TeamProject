using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerState
{
    protected PlayerStateMachine stateMachine;       //상태 머신에 대한 참조
    protected PlayerController playerController;     //플레이어 컨트롤러에 대한 참조
    protected PlayerAnimationManager animationManager;

    public PlayerState(PlayerStateMachine stateMachine)  //상태 머시과 플레이어 컨트롤러 참조 초기화
    {
        this.stateMachine = stateMachine;
        this.playerController = stateMachine.playerController;
        this.animationManager = stateMachine.GetComponent<PlayerAnimationManager>();
    }

    //가상 메서드 들 : 하위 클래스에서 필요에 따라 오버라이드
    public virtual void Enter() { } //상태 진입 시 호출
    public virtual void Exit() { } //상태 종료시 호출
    public virtual void Update() { } //매 프레임 호출
    public virtual void FixedUpdate() { } //고정 시간 간격으로 호출 (물리 연산용)

    //상태 전환 조건을 체크하는 메서드
    protected void CheckTransitions()
    {
        if (playerController.isGrounded()) //지상에 있을 때의 전환 로직
        {
            if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0) //이동 키가 눌렸을 때
            {
                stateMachine.TransitionToState(new MoveingState(stateMachine));
            }
            else
            {
                stateMachine.TransitionToState(new IdleState(stateMachine));    //아무 키도 누르지 않았을 때
            }
        }    
    }
}

//IdleState : 플레이어가 정지해 있는 상태
public class IdleState : PlayerState
{
    public IdleState(PlayerStateMachine stateMachine) : base(stateMachine) { }

    public override void Update()
    {
        CheckTransitions();       //매 프레임 마다 상태 전환 조건 체크 
    }
}

//MoveingState : 플레이어가 정지해 있는 상태
public class MoveingState : PlayerState
{
    private bool isRunning;
    public MoveingState(PlayerStateMachine stateMachine) : base(stateMachine) { }

    public override void Update()
    {
        //달리기 입력 확인
        isRunning = Input.GetKey(KeyCode.LeftShift);

        CheckTransitions();       //매 프레임 마다 상태 전환 조건 체크 
    }

    public override void FixedUpdate()
    {
        playerController.isGrounded();   //물리 기반 이동 처리
    }
}