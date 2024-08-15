using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 绑定在canvas身上便于自适应canvas大小
/// </summary>
public class CanvasMatch : MonoBehaviour
{
    /// <summary>
    /// 需要调整大小的CanvasScaler
    /// </summary>
    private CanvasScaler canvasScaler;
    /// <summary>
    /// Canvas参考大小刚开始的时候计算
    /// </summary>
    private float referenceAspect;

    /// <summary>
    /// 在第一帧调用
    /// </summary>
    void Start()
    {
        canvasScaler = transform.GetComponent<CanvasScaler>();
        referenceAspect = canvasScaler.referenceResolution.x / canvasScaler.referenceResolution.y;
        // 获取屏幕的宽度和高度
        float screenWidth = Screen.width;
        float screenHeight = Screen.height;
        // 计算屏幕的宽高比
        float screenAspect = screenWidth / screenHeight;
        // 计算Match值
        float match = referenceAspect / screenAspect;
        // 设置Canvas Scaler的Match值
        canvasScaler.matchWidthOrHeight = match;
    }
}
