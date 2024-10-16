using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkSound : MonoBehaviour
{
    
        [SerializeField] float speed = 5f;
        float moveX, moveZ;
        Rigidbody rb; // Rigidbody ��� (3D)
        AudioSource audioSrc;
        bool isMoving = false;

        // Start is called before the first frame update
        void Start()
        {
            rb = GetComponent<Rigidbody>(); // 3D Rigidbody ���
            audioSrc = GetComponent<AudioSource>();
        }

        // Update is called once per frame
        void Update()
        {
            MoveSfx();
        }

        void MoveSfx()
        {
            // �÷��̾��� �Է� ���� (3D������ Z�� �̵� �߰�)
            moveX = Input.GetAxis("Horizontal") * speed;
            moveZ = Input.GetAxis("Vertical") * speed;

            // Rigidbody�� ����� �̵� ó��
            rb.velocity = new Vector3(moveX, rb.velocity.y, moveZ);

            // �̵� ���� Ȯ�� (x �Ǵ� z ���� �ӵ�)
            if (Mathf.Abs(rb.velocity.x) > 0.1f || Mathf.Abs(rb.velocity.z) > 0.1f)
            {
                isMoving = true;
            }
            else
            {
                isMoving = false;
            }

            // �̵� ���� ���� �߼Ҹ� ���
            if (isMoving)
            {
                if (!audioSrc.isPlaying)
                {
                    audioSrc.Play();
                }
            }
            else
            {
                audioSrc.Stop();
            }
        }
    }




