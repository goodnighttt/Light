using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement_3D : MonoBehaviour
{
    public float moveSpeed = 5f;          // 移动速度
    public float rotationSpeed = 10f;    // 人物旋转速度
    public float cameraSensitivity = 2f; // 相机灵敏度
    public float cameraDistance = 5f;    // 相机与角色的距离
    public float cameraHeightOffset = 2f; // 相机相对于角色的高度偏移
    public float minYAngle = 10f;        // 最小俯仰角
    public float maxYAngle = 80f;        // 最大俯仰角

    private CharacterController controller;  // 引用CharacterController组件
    private Animator animator;           // 引用Animator组件
    private Transform cameraTransform;   // 相机的Transform
    private Vector3 movementDirection;   // 当前移动方向
    private float currentYaw;            // 当前水平角度
    private float currentPitch;          // 当前俯仰角度

    void Start()
    {
        // 获取CharacterController和Animator组件
        controller = GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>();

        // 初始化相机
        cameraTransform = Camera.main.transform;
        currentYaw = cameraTransform.eulerAngles.y;
        currentPitch = 20f; // 初始俯仰角
    }

    void Update()
    {
        HandleCameraRotation();
        HandleMovement();
        UpdateCameraPosition();
    }

    void HandleMovement()
    {
        // 获取WASD输入
        float horizontal = Input.GetAxis("Horizontal");  // A/D 或 左/右箭头
        float vertical = Input.GetAxis("Vertical");      // W/S 或 上/下箭头

        // 根据相机方向计算角色移动方向
        Vector3 forward = cameraTransform.forward;
        Vector3 right = cameraTransform.right;

        forward.y = 0f; // 忽略相机的垂直分量
        right.y = 0f;

        forward.Normalize();
        right.Normalize();

        movementDirection = forward * vertical + right * horizontal;

        // 控制动画
        if (animator != null)
        {
            animator.SetFloat("Speed", movementDirection.magnitude);
        }

        // 控制角色朝向
        if (movementDirection.magnitude > 0.1f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(movementDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }

        // 使用CharacterController移动
        Vector3 moveVector = movementDirection * moveSpeed;
        moveVector.y = Physics.gravity.y; // 简单处理重力
        controller.Move(moveVector * Time.deltaTime);
    }

    void HandleCameraRotation()
    {
        // 获取鼠标输入
        float mouseX = Input.GetAxis("Mouse X") * cameraSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * cameraSensitivity;

        // 更新相机水平和俯仰角度
        currentYaw += mouseX;
        currentPitch -= mouseY;
        currentPitch = Mathf.Clamp(currentPitch, minYAngle, maxYAngle); // 限制俯仰角度范围
    }

    void UpdateCameraPosition()
    {
        // 计算基于俯仰角的方向
        Quaternion rotation = Quaternion.Euler(currentPitch, currentYaw, 0);
        Vector3 targetPosition = transform.position - rotation * Vector3.forward * cameraDistance;

        // 调整相机的高度偏移
        targetPosition.y += cameraHeightOffset;

        // 设置相机位置和旋转
        cameraTransform.position = targetPosition;
        cameraTransform.LookAt(transform.position + Vector3.up * cameraHeightOffset);
    }
}
