using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float walkSpeed;

    [SerializeField]
    private float runSpeed;
    private float applySpeed;

    private bool isRun = false;

    [SerializeField]
    private float lookSensitivity;

    [SerializeField]
    private float cameraRotationLimit;
    private float currentCameraRotationX = 0;

    [SerializeField]
    private Camera theCamera;

    private Rigidbody myRigid;

    // 걷는 소리 추가
    [SerializeField]
    private AudioSource audioSource; // 발걸음 소리를 재생할 AudioSource

    [SerializeField]
    private AudioClip footstepClip;  // 발걸음 소리 오디오 클립

    // Start is called before the first frame update
    void Start()
    {

        myRigid = GetComponent<Rigidbody>();
        applySpeed = walkSpeed;

        // AudioSource 설정
        audioSource.clip = footstepClip;
        audioSource.loop = false; // 발걸음 소리는 짧게 반복되므로 false
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        ApplyGravity(); // 중력 적용
        TryRun();
        Move();
        CameraRotation();
        CharacterRotation();
    }

    private void ApplyGravity()
    {
        // 아래 방향으로 힘을 적용하여 플레이어를 지면에 붙이기
        Vector3 gravity = new Vector3(0, -19.62f, 0); // 중력 값
        myRigid.AddForce(gravity, ForceMode.Acceleration);
    }

    private void TryRun()
    {
        isRun = Input.GetKey(KeyCode.LeftShift);
        applySpeed = isRun ? runSpeed : walkSpeed;
    }

    private void Move()
    {
        float _moveDirX = Input.GetAxisRaw("Horizontal");
        float _moveDirZ = Input.GetAxisRaw("Vertical");

        Vector3 _moveHorizontal = transform.right * _moveDirX;
        Vector3 _moveVertical = transform.forward * _moveDirZ;

        Vector3 _velocity = (_moveHorizontal + _moveVertical).normalized * applySpeed;

        myRigid.MovePosition(transform.position + _velocity * Time.deltaTime);

        // 캐릭터가 움직일 때 발걸음 소리 재생
        if (_velocity.magnitude > 0.1f && !audioSource.isPlaying)
        {
            audioSource.Play();
        }
        else if (_velocity.magnitude <= 0.1f && audioSource.isPlaying)
        {
            audioSource.Stop(); // 캐릭터가 멈추면 발걸음 소리 정지
        }
    }

    private void CharacterRotation()
    {
        float _yRotation = Input.GetAxisRaw("Mouse X");
        Vector3 _chataterRotationY = new Vector3(0f, _yRotation, 0f) * lookSensitivity;
        myRigid.MoveRotation(myRigid.rotation * Quaternion.Euler(_chataterRotationY));
    }

    private void CameraRotation()
    {
        float _xRotation = Input.GetAxisRaw("Mouse Y");
        float _cameraRotationX = _xRotation * lookSensitivity;
        currentCameraRotationX -= _cameraRotationX;
        currentCameraRotationX = Mathf.Clamp(currentCameraRotationX, -cameraRotationLimit, cameraRotationLimit);

        theCamera.transform.localEulerAngles = new Vector3(currentCameraRotationX, 0f, 0f);
    }

    public bool isGrounded()    // 땅 체크 확인
    {
        return Physics.Raycast(transform.position, Vector3.down, 10.5f);
    }
    //private void Update()
    //{
        //if (transform.position.y < 0.1)
        //{
           //Vector3 temp = transform.position;
            //temp.y = 0.1f;
            //transform.position = temp;
        //}
    //}
}









