using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slidingdoor : MonoBehaviour
{
    // ��� �����ϱ� ����
    bool flag;

    public GameObject door;

    // Start is called before the first frame update
    void Start()
    {
        //�����ִ� ����
        flag = flag;
    }

    // Update is called once per frame
    void Update()
    {
        //���� ���� ó��
        if (flag == true)
        {
            if(door.transform.position.x >= 1.326f)
            {
                door.transform.Translate(-0.05f, 0, 0);
            }
        }

        if (flag == false)
        {
            if (door.transform.position.x < 2.122f)
            {
                door.transform.Translate(0.05f, 0, 0);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        flag = true;
    }
    private void OnTriggerExit(Collider other)
    {
        flag = false;
    }
}
