using UnityEngine;
using UnityEngine.UI;

public class AudioVisualShader2 : MonoBehaviour
{
    public AudioSource audioSource;
    public Image circleImage; // 用於顯示圓形 UI
    public int numSamples = 64; // 頻譜數據的樣本數量
    public float amplitude = 5f; // 波形的強度
    public float smoothingFactor = 0.1f; // 平滑係數
    private float[] spectrumData;
    private Material circleMaterial; // 圓形 UI 的材質

    void Start()
    {
        spectrumData = new float[numSamples];
        if (circleImage != null)
        {
            circleMaterial = circleImage.material; // 獲取 UI 圓形的材質
        }
    }

    void Update()
    {
        // 獲取音頻頻譜數據
        audioSource.GetSpectrumData(spectrumData, 0, FFTWindow.BlackmanHarris);

        // 設置波形數據到 Shader
        UpdateShaderWaveform();
    }

    void UpdateShaderWaveform()
    {
        if (circleMaterial != null)
        {
            // 使用樣本中的某個數據來作為波動值
            float waveAmount = Mathf.Clamp01(spectrumData[0] * amplitude);
            circleMaterial.SetFloat("_WaveAmount", waveAmount);
        }
    }
}