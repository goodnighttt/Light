// UIController.cs
using UnityEngine;

public class UIController : MonoBehaviour
{
    public UIView uiView;  // UI视图
    private bool isPlayerInLightCone = false;  // 玩家是否在光锥内
    public float scoreIncreaseRate = 5f;  // 每秒增加的分数

    void Start()
    {
        // 初始时显示分数
        uiView.UpdateScoreDisplay();
        
        // 订阅事件
        LightConeTrigger.OnPlayerInLightCone += HandlePlayerInLightCone;
    }

    void OnDestroy()
    {
        // 取消订阅事件，避免内存泄漏
        LightConeTrigger.OnPlayerInLightCone -= HandlePlayerInLightCone;
    }

    void Update()
    {
        // 如果玩家在光锥内，按时间增加分数
        if (isPlayerInLightCone)
        {
            uiView.GetModel().UpdateScore(scoreIncreaseRate * Time.deltaTime);  // 更新分数
            uiView.UpdateScoreDisplay();  // 更新UI显示
        }
    }

    // 事件响应：玩家进入或离开光锥
    void HandlePlayerInLightCone(bool isInLightCone)
    {
        isPlayerInLightCone = isInLightCone;
        Debug.Log("Player in light cone: " + isPlayerInLightCone);
    }
}
