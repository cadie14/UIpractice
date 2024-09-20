using UnityEngine;

public class AudioController : MonoBehaviour
{
    public AudioSource audioSource; // 音源
    public Animator animator; // 控制的动画
    public float sensitivity = 1.0f; // 鼓点检测灵敏度
    public float minSpeed = 0.5f; // 最小动画速度
    public float maxSpeed = 2.0f; // 最大动画速度

    private float[] audioSamples = new float[1024]; // 存储音频数据

    void Update()
    {
        // 获取音频输出数据
        audioSource.GetOutputData(audioSamples, 0);

        // 计算音频的平均能量（用于检测鼓点）
        float averageAmplitude = 0f;
        for (int i = 0; i < audioSamples.Length; i++)
        {
            averageAmplitude += Mathf.Abs(audioSamples[i]);
        }
        averageAmplitude /= audioSamples.Length;

        // 根据鼓点强度调整动画速度
        float speedMultiplier = Mathf.Clamp(averageAmplitude * sensitivity, minSpeed, maxSpeed);
        animator.speed = speedMultiplier;
    }
}