// UIModel.cs
public class UIModel
{
    public float score { get; private set; }  // 当前分数

    public UIModel()
    {
        score = 0f;
    }

    // 更新分数
    public void UpdateScore(float amount)
    {
        score += amount;
    }

    // 设置分数
    public void SetScore(float newScore)
    {
        score = newScore;
    }
}
