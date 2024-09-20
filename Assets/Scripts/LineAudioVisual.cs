using UnityEngine;

public class CircleVisualizer : MonoBehaviour
{
    public AudioSource audioSource;
    public LineRenderer lineRenderer;
    public int numSamples = 256; // 頻譜數據的樣本數量
    public float radius = 5f; // 圓形的半徑
    public float amplitude = 5f; // 波形的幅度
    public float smoothFactor = 0.5f; // 用於平滑處理的係數
    private float[] spectrumData;
    private Vector3[] previousPositions; // 用來保存上一幀的點

    void Start()
    {
        spectrumData = new float[numSamples];
        lineRenderer.positionCount = numSamples;
        lineRenderer.useWorldSpace = false; // 使用局部空間
        previousPositions = new Vector3[numSamples];
    }

    void Update()
    {
        // 獲取音頻頻譜數據
        audioSource.GetSpectrumData(spectrumData, 0, FFTWindow.Rectangular);

        // 更新 LineRenderer 位置
        UpdateCircle();
    }

    void UpdateCircle()
    {
        for (int i = 0; i < numSamples; i++)
        {
            // 計算角度來生成圓形
            float angle = i * Mathf.PI * 2f / numSamples;
            float x = Mathf.Cos(angle) * radius;
            float y = Mathf.Sin(angle) * radius;

            // 根據頻譜數據調整半徑，使其產生波動效果，並進行平滑處理
            float scale = Mathf.Lerp(1, 1 + spectrumData[i] * amplitude, smoothFactor); // 緩和幅度

            // 設置當前點的位置 (忽略 Z 軸)
            Vector3 newPosition = new Vector3(x * scale, y * scale, 0f);

            // 使用插值來實現位置的平滑過渡，防止波動過於激進
            newPosition = Vector3.Lerp(previousPositions[i], newPosition, smoothFactor);

            // 更新 LineRenderer 的位置
            lineRenderer.SetPosition(i, newPosition);

            // 記錄當前位置，為下一幀平滑過渡做準備
            previousPositions[i] = newPosition;
        }
    }
}