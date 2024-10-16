using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkSound : MonoBehaviour
{
    
        [SerializeField] float speed = 5f;
        float moveX, moveZ;
        Rigidbody rb; // Rigidbody 사용 (3D)
        AudioSource audioSrc;
        bool isMoving = false;

        // Start is called before the first frame update
        void Start()
        {
            rb = GetComponent<Rigidbody>(); // 3D Rigidbody 사용
            audioSrc = GetComponent<AudioSource>();
        }

        // Update is called once per frame
        void Update()
        {
            MoveSfx();
        }

        void MoveSfx()
        {
            // 플레이어의 입력 감지 (3D에서는 Z축 이동 추가)
            moveX = Input.GetAxis("Horizontal") * speed;
            moveZ = Input.GetAxis("Vertical") * speed;

            // Rigidbody를 사용한 이동 처리
            rb.velocity = new Vector3(moveX, rb.velocity.y, moveZ);

            // 이동 여부 확인 (x 또는 z 방향 속도)
            if (Mathf.Abs(rb.velocity.x) > 0.1f || Mathf.Abs(rb.velocity.z) > 0.1f)
            {
                isMoving = true;
            }
            else
            {
                isMoving = false;
            }

            // 이동 중일 때만 발소리 재생
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




