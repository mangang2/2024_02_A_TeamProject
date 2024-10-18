using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slidingdoor : MonoBehaviour
{
    // 도어를 제어하기 위해
    bool flag;

    public GameObject door;

    // Start is called before the first frame update
    void Start()
    {
        //닫혀있는 상태
        flag = flag;
    }

    // Update is called once per frame
    void Update()
    {
        //문을 여는 처리
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
