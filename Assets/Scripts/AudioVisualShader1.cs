using UnityEngine;

public class AudioVisualShader1 : MonoBehaviour
{
    public Material waveMaterial; // 指向你的 WaveCircleShader 的材質
    public float sensitivity = 2.0f; // 控制波形反應靈敏度
    public int spectrumIndex = 1; // 用來選擇哪個頻率段來影響效果
    public AudioSource audioSource; // 音樂來源

    private float[] spectrum = new float[64];

    void Start()
    {
        if (audioSource == null)
            audioSource = GetComponent<AudioSource>(); // 確保有 AudioSource
    }

    void Update()
    {
        // 提取音頻的頻譜數據
        audioSource.GetSpectrumData(spectrum, 0, FFTWindow.Blackman);

        // 使用指定頻率段的值來控制波動
        float amplitude = spectrum[spectrumIndex] * sensitivity;

        // 將音頻數據傳遞到 Shader
        waveMaterial.SetFloat("_Amplitude", amplitude);
    }
}