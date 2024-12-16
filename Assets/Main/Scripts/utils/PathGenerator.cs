using UnityEngine;

public class PathGenerator : MonoBehaviour
{
    public LineRenderer lineRenderer;  // 用于绘制路径的LineRenderer

    // 这个列表用来在Inspector中编辑
    [Header("Path Points (编辑时手动输入)")]
    public Vector3[] inputPoints;

    // 生成路径并更新LineRenderer
    void Start()
    {
        // 生成路径
        GeneratePath();
    }

    void OnDrawGizmos()
    {
        // 仅在编辑器中可视化路径
        if (inputPoints.Length > 1)
        {
            Gizmos.color = Color.green;
            // 绘制路径点和路径
            for (int i = 0; i < inputPoints.Length; i++)
            {
                Gizmos.DrawSphere(inputPoints[i], 0.2f);  // 绘制路径点
                if (i < inputPoints.Length - 1)
                {
                    Gizmos.DrawLine(inputPoints[i], inputPoints[i + 1]);  // 绘制点与点之间的连接线
                }
                else
                {
                    // 确保最后一个点与第一个点相连
                    Gizmos.DrawLine(inputPoints[i], inputPoints[0]);
                }
            }
        }
    }

    // 生成路径点并更新LineRenderer
    void GeneratePath()
    {
        // 只有在输入的路径点数组不为空时
        if (inputPoints != null && inputPoints.Length > 1)
        {
            // 设置LineRenderer的点数和位置
            lineRenderer.positionCount = inputPoints.Length + 1;

            // 设置LineRenderer的线宽（可以根据需求调整）
            lineRenderer.startWidth = 0.5f;  // 起始宽度
            lineRenderer.endWidth = 0.5f;    // 结束宽度

            // 使用for循环逐一连接路径点
            for (int i = 0; i < inputPoints.Length; i++)
            {
                lineRenderer.SetPosition(i, inputPoints[i]);
            }

            // 确保最后一个点与第一个点连接
            lineRenderer.SetPosition(inputPoints.Length, inputPoints[0]);
        }
    }

    // 获取路径点（供其他脚本调用）
    public Vector3[] GetPathPoints()
    {
        return inputPoints;
    }

    // 获取随机路径点
    public Vector3 GetRandomPointOnPath()
    {
        // 获取一个随机的路径点索引
        int randomIndex = Random.Range(0, inputPoints.Length);

        // 返回随机选中的路径点，只取 x 和 z 坐标，y 保持为0
        return new Vector3(inputPoints[randomIndex].x, 0, inputPoints[randomIndex].z);
    }
}
