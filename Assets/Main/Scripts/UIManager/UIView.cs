// UIView.cs
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIView: MonoBehaviour
{
    public TextMeshProUGUI scoreText;  // 用于显示分数的 UI Text

    private UIModel uiModel;  // 数据模型

    // 初始化
    void Start()
    {
        uiModel = new UIModel();  // 初始化数据模型
        UpdateScoreDisplay();  // 初始化时更新UI
    }

    // 更新分数显示
    public void UpdateScoreDisplay()
    {
        scoreText.text = "Score: " + Mathf.FloorToInt(uiModel.score);  // 显示整数分数
    }

    // 获取数据模型（供控制器使用）
    public UIModel GetModel()
    {
        return uiModel;
    }
}
