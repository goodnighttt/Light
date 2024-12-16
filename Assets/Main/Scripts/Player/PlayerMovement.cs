using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;           // 移动速度
    public float rotationSpeed = 700f;     // 旋转速度，控制转向的平滑度
    private Vector3 movement;              // 存储玩家的输入方向
    private float verticalVelocity;        // 垂直速度（用于处理重力）
    private float gravity = -9.8f;         // 重力加速度
    private CharacterController controller; // 引用CharacterController组件
    private Animator animator;             // 引用Animator组件
    private Camera mainCamera;             // 主相机引用

    void Start()
    {
        // 获取CharacterController、Animator和Camera组件
        controller = GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>();  
        mainCamera = Camera.main; // 获取主相机
    }

    void Update()
    {
        // 获取玩家输入（WASD）
        float horizontal = Input.GetAxisRaw("Horizontal");  // A/D 或 左/右箭头
        float vertical = Input.GetAxisRaw("Vertical");      // W/S 或 上/下箭头

        // 获取相机的前后左右方向
        Vector3 cameraForward = mainCamera.transform.forward;
        Vector3 cameraRight = mainCamera.transform.right;

        // 忽略相机前后方向的Y分量
        cameraForward.y = 0;
        cameraRight.y = 0;

        // 设置角色的移动方向
        movement = cameraForward * vertical + cameraRight * horizontal;

        // 控制动画
        if (animator != null)
        {
            // 设置Speed参数来控制动画过渡
            animator.SetFloat("Speed", Mathf.Abs(vertical) + Mathf.Abs(horizontal));  // 使用前后输入的绝对值更新Speed
        }

        // 添加重力效果
        if (controller.isGrounded)
        {
            verticalVelocity = 0; // 如果角色在地面上，重置垂直速度
        }
        else
        {
            // 处理重力：应用重力加速度到角色的垂直方向（y）
            verticalVelocity += gravity * Time.deltaTime; // 应用重力
        }

        // 将重力添加到移动向量中
        // movement.y = verticalVelocity;

        // 移动角色
        if (movement.magnitude > 0)
        {
            MovePlayer();
            RotatePlayer();  // 角色始终朝向移动方向
        }
    }

    void MovePlayer()
    {
        // 使用CharacterController进行移动
        controller.Move(movement * moveSpeed * Time.deltaTime);
    }

    void RotatePlayer()
    {
        // 根据移动方向计算目标旋转角度
        if (movement.magnitude > 0)
        {
            Quaternion targetRotation = Quaternion.LookRotation(movement.normalized);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }
}
