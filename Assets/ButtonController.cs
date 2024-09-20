using UnityEngine;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour
{
    public Text scoreText;
    public Button increaseButton;
    public Button decreaseButton;
    public Button switchAudioButton;
    public Button playPauseButton;
    public AudioSource audioSource;
    
    private int score = 0;
    private bool isPlaying = true;
    
    private AudioClip[] audioClips;
    private int currentClipIndex = 0;

    void Start()
    {
        // 初始化 AudioSource
        audioSource = GetComponent<AudioSource>();

        // 從 Resources/Audio 文件夾加載所有音頻文件
        audioClips = Resources.LoadAll<AudioClip>("Audio");

        // 初始化分數顯示
        UpdateScoreText();

        // 設置按鈕事件
        increaseButton.onClick.AddListener(IncreaseScore);
        decreaseButton.onClick.AddListener(DecreaseScore);
        switchAudioButton.onClick.AddListener(SwitchAudioSource);
        playPauseButton.onClick.AddListener(TogglePlayPause);

        // 播放第一個音頻
        if (audioClips.Length > 0)
        {
            audioSource.clip = audioClips[currentClipIndex];
            audioSource.Play();
        }
    }

    // 增加分數
    void IncreaseScore()
    {
        if (score < 9)
        {
            score++;
            UpdateScoreText();
        }
    }

    // 減少分數
    void DecreaseScore()
    {
        if (score > 0)
        {
            score--;
            UpdateScoreText();
        }
    }

    // 更新分數文本
    void UpdateScoreText()
    {
        scoreText.text = score.ToString();
    }

    // 切換音源
    void SwitchAudioSource()
    {
        if (audioClips.Length > 0)
        {
            currentClipIndex = (currentClipIndex + 1) % audioClips.Length; // 循環播放
            audioSource.clip = audioClips[currentClipIndex];
            audioSource.Play();
        }
    }

    // 切換音樂的播放與暫停
    void TogglePlayPause()
    {
        if (isPlaying)
        {
            audioSource.Pause();
        }
        else
        {
            audioSource.Play();
        }
        isPlaying = !isPlaying;
    }
}
