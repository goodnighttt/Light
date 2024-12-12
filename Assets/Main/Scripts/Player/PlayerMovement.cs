using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;  // 移动速度
    public float rotationSpeed = 700f; // 旋转速度，控制转向的平滑度
    private Vector3 movement;     // 存储玩家的输入方向
    private float verticalVelocity; // 垂直速度（用于处理重力）
    private float gravity = -9.8f; // 重力加速度
    private CharacterController controller;  // 引用CharacterController组件
    private Animator animator;    // 引用Animator组件

    void Start()
    {
        // 获取CharacterController和Animator组件
        controller = GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>();  // 获取子物体中的Animator
    }

    void Update()
    {
        // 获取玩家输入
        float horizontal = Input.GetAxisRaw("Horizontal");  // A/D 或 左/右箭头
        float vertical = Input.GetAxisRaw("Vertical");      // W/S 或 上/下箭头

        // 设置前后移动方向
        movement = transform.forward * vertical;

        // 控制动画
        if (animator != null)
        {
            // 设置Speed参数来控制动画过渡
            animator.SetFloat("Speed", Mathf.Abs(vertical));  // 使用前后输入的绝对值更新Speed
        }

        // 控制角色左右旋转
        if (horizontal != 0)
        {
            RotatePlayer(horizontal);
        }

        // 添加重力效果
        if (controller.isGrounded)
        {
            verticalVelocity = 0; // 如果角色在地面上，重置垂直速度
        }
        else
        {
            verticalVelocity += gravity * Time.deltaTime; // 应用重力
        }

        // 将重力添加到移动向量中
        movement.y = verticalVelocity;

        // 移动角色
        if (movement.magnitude > 0)
        {
            MovePlayer();
        }
    }

    void MovePlayer()
    {
        // 使用CharacterController进行移动
        controller.Move(movement * moveSpeed * Time.deltaTime);
    }

    void RotatePlayer(float horizontal)
    {
        // 根据水平输入计算旋转角度
        float rotation = horizontal * rotationSpeed * Time.deltaTime;
        transform.Rotate(0, rotation, 0); // 绕Y轴旋转
    }
}
