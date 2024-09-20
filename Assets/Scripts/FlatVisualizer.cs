using UnityEngine;

public class AudioVisualizer : MonoBehaviour
{
    public AudioSource audioSource;
    public LineRenderer lineRenderer;
    public int numSamples = 256;
    private float[] spectrumData;
    public float amplitude = 5f; // 波形的幅度
    public float smoothFactor = 0.5f; // 用於平滑處理的係數
    private Vector3[] previousPositions; // 用來保存上一幀的點

    void Start()
    {
        spectrumData = new float[numSamples];
        lineRenderer.positionCount = numSamples;
        previousPositions = new Vector3[numSamples];
    }

    void Update()
    {
        // 獲取音頻頻譜數據
        audioSource.GetSpectrumData(spectrumData, 0, FFTWindow.Rectangular);

        // 更新 LineRenderer 位置
        for (int i = 0; i < numSamples; i++)
        {
            float y = spectrumData[i] * 10; // 調整強度
            
            // 根據頻譜數據調整半徑，使其產生波動效果，並進行平滑處理
            float scale = Mathf.Lerp(1, 1 + spectrumData[i] * amplitude, smoothFactor); // 緩和幅度

            // 設置當前點的位置 (忽略 Z 軸)
            Vector3 newPosition = new Vector3(0f, y * scale, 0f);

            // 使用插值來實現位置的平滑過渡，防止波動過於激進
            newPosition = Vector3.Lerp(previousPositions[i], newPosition, smoothFactor);

            // 更新 LineRenderer 的位置
            lineRenderer.SetPosition(i, new Vector3(i * 0.1f, y, 0));

            // 記錄當前位置，為下一幀平滑過渡做準備
            previousPositions[i] = newPosition;
        }
    }
}