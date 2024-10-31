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

    // �ȴ� �Ҹ� �߰�
    [SerializeField]
    private AudioSource audioSource; // �߰��� �Ҹ��� ����� AudioSource

    [SerializeField]
    private AudioClip footstepClip;  // �߰��� �Ҹ� ����� Ŭ��

    // Start is called before the first frame update
    void Start()
    {

        myRigid = GetComponent<Rigidbody>();
        applySpeed = walkSpeed;

        // AudioSource ����
        audioSource.clip = footstepClip;
        audioSource.loop = false; // �߰��� �Ҹ��� ª�� �ݺ��ǹǷ� false
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        ApplyGravity(); // �߷� ����
        TryRun();
        Move();
        CameraRotation();
        CharacterRotation();
    }

    private void ApplyGravity()
    {
        // �Ʒ� �������� ���� �����Ͽ� �÷��̾ ���鿡 ���̱�
        Vector3 gravity = new Vector3(0, -19.62f, 0); // �߷� ��
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

        // ĳ���Ͱ� ������ �� �߰��� �Ҹ� ���
        if (_velocity.magnitude > 0.1f && !audioSource.isPlaying)
        {
            audioSource.Play();
        }
        else if (_velocity.magnitude <= 0.1f && audioSource.isPlaying)
        {
            audioSource.Stop(); // ĳ���Ͱ� ���߸� �߰��� �Ҹ� ����
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
}








