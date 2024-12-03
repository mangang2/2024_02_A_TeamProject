using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : MonoBehaviour
{
    //���� �÷��̾��� ���¸� ��Ÿ���� ����

    public PlayerState currentState;             //���� �÷��̾��� ���¸� ��Ÿ���� ����
    public PlayerController playerController;    //�÷��̾� ������

    private void Awake()
    {
        playerController = GetComponent<PlayerController>();   //�÷��̾� ������Ʈ ����
    }

    // Start is called before the first frame update
    void Start()
    {
        TransitionToState(new IdleState(this));     //�ʱ� ���¸� IdleState�� ����
    }

    // Update is called once per frame
    void Update()
    {
        if (currentState != null)    //���� ���°� ���� �Ѵٸ�
        {
            currentState.Update();
        }
    }

    private void FixedUpdate()
    {
        if (currentState != null)    //���� ���°� ���� �Ѵٸ�
        {
            currentState.FixedUpdate();
        }
    }

    //TransitionToState ���ο� ���·� ��ȭ�ϴ� �޼���
    public void TransitionToState(PlayerState newState) 
    {

        //���� ���¿� ���ο� ���°� ���� Ÿ���� ��� ���� ��ȭ�� ���� �ʰ� �Ѵ�.
        if (currentState?.GetType() == newState.GetType())
        {
            return;         //���� Ÿ���̸� ������ȯ�� ���� �ʰ� ����
        }

        currentState?.Exit();                                           //���� ���°� �����Ѵٸ� [?] If �� ó�� ���� (���� ����)
        currentState = newState;                                        //���ο� ���·� ��ȯ
        currentState.Enter();                                           //���� ����
        Debug.Log($"Transitioned to State {newState.GetType().Name}");  //���� ���� �α׷� ���
    }
}
