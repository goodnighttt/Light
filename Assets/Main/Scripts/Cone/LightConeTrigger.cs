// LightConeTrigger.cs
using UnityEngine;
using System;

public class LightConeTrigger : MonoBehaviour
{
    public static event Action<bool> OnPlayerInLightCone;  // 事件：玩家是否在光锥内

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))  // 检查tag
        {
            Debug.Log("Player entered light cone");
            OnPlayerInLightCone?.Invoke(true);  // 玩家进入光锥
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))  // 检查tag
        {
            Debug.Log("Player left light cone");
            OnPlayerInLightCone?.Invoke(false);  // 玩家离开光锥
        }
    }
}
