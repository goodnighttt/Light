using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightConeController : MonoBehaviour
{
    public GameObject lightConePrefab;  // 引用光锥的预制体
    public float moveSpeed = 5f;  // 光锥的移动速度
    private GameObject lightConeInstance;  // 光锥实例
    private Vector3 targetPosition;  // 光锥的目标位置
    public PathGenerator pathGenerator;  // 场景范围生成工具

    private Vector3 minBounds;  // 生成范围的最小边界
    private Vector3 maxBounds;  // 生成范围的最大边界

    void Start()
    {
        lightConeInstance = Instantiate(lightConePrefab);  // 实例化光锥预制体
        SetBounds();  // 计算并设置光锥的移动范围
        SetRandomTargetPosition();  // 设置初始目标位置
    }

    void Update()
    {
        // 光锥以指定速度朝目标位置移动
        lightConeInstance.transform.position = Vector3.MoveTowards(lightConeInstance.transform.position, targetPosition, moveSpeed * Time.deltaTime);

        // 如果光锥到达目标位置，重新设置一个随机目标
        if (Vector3.Distance(lightConeInstance.transform.position, targetPosition) < 0.1f)
        {
            SetRandomTargetPosition();
        }
    }

    // 随机选择一个目标位置，确保它在通过PathGenerator工具生成的范围内
    void SetRandomTargetPosition()
    {
        // 获取PathGenerator生成的范围
        float randomX = Random.Range(minBounds.x, maxBounds.x);
        float randomZ = Random.Range(minBounds.z, maxBounds.z);

        // 将光锥的目标位置设置为随机位置
        targetPosition = new Vector3(randomX, 3.5f, randomZ);  // 假设光锥高度为 0.5
    }

    // 计算PathGenerator生成的路径点的边界
    void SetBounds()
    {
        Vector3[] pathPoints = pathGenerator.GetPathPoints();

        // 初始化最小值和最大值
        float minX = Mathf.Infinity, maxX = -Mathf.Infinity;
        float minZ = Mathf.Infinity, maxZ = -Mathf.Infinity;

        // 遍历路径点，计算最小和最大边界
        foreach (Vector3 point in pathPoints)
        {
            minX = Mathf.Min(minX, point.x);
            maxX = Mathf.Max(maxX, point.x);
            minZ = Mathf.Min(minZ, point.z);
            maxZ = Mathf.Max(maxZ, point.z);
        }

        // 设置边界
        minBounds = new Vector3(minX, 0, minZ);
        maxBounds = new Vector3(maxX, 0, maxZ);
    }
}
