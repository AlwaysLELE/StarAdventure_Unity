using System;
using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public GameObject startUI; // 开始游戏的UI面板
    public GameObject gameUI;//游戏进行中的UI面板
    public GameObject endUI; // 游戏结束的UI面板
    public GameObject configUI;//达到目标分数后的UI面板
    public TextMeshProUGUI scoreText_end;//endUI分数文本
    public TextMeshProUGUI scoreText_game;//gameUI分数文本
    public TextMeshProUGUI countdownText; // 倒计时文本
    public TextMeshProUGUI successedInfo;//通关显示提示文本
    public TextMeshProUGUI failedInfo;//失败显示提示文本

    public float gameDuration = 300f; // 游戏时长
    public float countdownTimer; // 倒计时计时器
    bool isActive = false;
    public AudioSource startBGM;
    public AudioSource timerBGM;
    public AudioSource failedBGM;
    public AudioSource successedBGM;
    public AudioSource gameBGM;
    public AudioSource waterBGM;
    
    //游戏暂停
    public Toggle pauseToggle;
    public bool isGamePaused;
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {

        // 只显示开始游戏的UI
        startUI.SetActive(true);
        gameUI.SetActive(false);
        endUI.SetActive(false);
        configUI.SetActive(false);

        pauseToggle.onValueChanged.AddListener(OnToggleValueChanged);


    }

   
    private void OnToggleValueChanged(bool isPaused)
    {
        if (isPaused)
        {
            PauseGame();
            
        }
        else
        {
            ResumeGame();
            
        }
    }

    private void PauseGame()
    {
        isGamePaused = true;
        Time.timeScale = 0f;
        pauseToggle.targetGraphic.enabled = false;
       
    }
    private void ResumeGame()
    {
        isGamePaused=false;
        Time.timeScale = 1f;
        pauseToggle.targetGraphic.enabled = true;
        
    }

    public void StartGame()
    {

        //清空所有结算时的历史信息
        ClearScore();
        HideInfo();
        StopAllCoroutines();
        
        startBGM.Stop();
        gameBGM.Play();
        waterBGM.Play();
        // 切换到游戏场景并开始计时
        SceneManager.LoadScene("GameScene");
        startUI.SetActive(false);
        endUI.SetActive(false);
        gameUI.SetActive(true);//显示游戏中UI
        countdownTimer = gameDuration;
        UpdateCountdownText();
        UpdateScoreText();
        isGamePaused = false;
        // 启动倒计时协程
        StartCoroutine(CountdownCoroutine());
        
    }

    public void EndGame()
    {
        //预处理
#if UNITY_EDITOR    //在编辑器模式下
        EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif

    }
    // --------------------------------------------------------------------------------
    /// <summary>
    /// 用来判断是否达到通关分数的方法
    /// </summary>
    // --------------------------------------------------------------------------------

    private void ReachTarget()
    {
        if (ScoreManager.Instance.score >= ScoreManager.Instance.targetScore && !isActive)
        {
            configUI.SetActive(true);
            isActive = true;
            isGamePaused=true;
           
            Debug.Log(countdownTimer);
        }
    }
   public IEnumerator CountdownCoroutine()
    {
           
       
        // 倒计时循环      
            while (countdownTimer > 0f)
            {

                yield return new WaitForSeconds(1f);
                if (!isGamePaused)
                {
                    countdownTimer--;
                }
                else
                {
                    countdownTimer -= 0; 
                    //break;
                }
                UpdateCountdownText();
                UpdateScoreText();
                ReachTarget();
                if (countdownTimer < 10f)
                {
                    countdownText.color = Color.red;
                    timerBGM.Play();
                }
                else
                {
                    countdownText.color = Color.white;
                }               
        }
        // 游戏倒计时结束，返回初始场景
        GoEnd();
    }

    private void UpdateCountdownText()
    {

        // 更新倒计时文本显示
        countdownText.text = countdownTimer.ToString();
    }
    private void UpdateScoreText()
    {
        //更新分数文本显示
        scoreText_end.text = Mathf.RoundToInt(ScoreManager.Instance.sumScore).ToString();
        scoreText_game.text = ScoreManager.Instance.score.ToString();
    }
    private void ShowInfo()
    {
        if (ScoreManager.Instance.sumScore >= ScoreManager.Instance.targetScore)
        {
            successedBGM.Play();
            successedInfo.gameObject.SetActive(true);

        }
        else
        {
            failedBGM.Play();
            failedInfo.gameObject.SetActive(true);
        }

    }
    // --------------------------------------------------------------------------------
    /// <summary>
    /// 用于清空状态提示信息
    /// </summary>
    // --------------------------------------------------------------------------------
    private void HideInfo()
    {
        successedInfo.gameObject.SetActive(false);
        failedInfo.gameObject.SetActive(false);
    }
    public void BackToOrigin()
    {
        //清空所有结算时的历史信息
        StopAllCoroutines();
        ClearScore();
        HideInfo();
        // 只显示开始游戏的UI
        startUI.SetActive(true);
        gameUI.SetActive(false);
        endUI.SetActive(false);
        configUI.SetActive(false);
        endUI.SetActive(false);
        gameBGM.Stop();
        waterBGM.Stop();
        startBGM.Play();
    }

    // --------------------------------------------------------------------------------
    /// <summary>
    /// 用于清空历史分数
    /// </summary>
    // --------------------------------------------------------------------------------

    private void ClearScore()
    {
        ScoreManager.Instance.score = 0;
        Debug.Log(ScoreManager.Instance.score);
    }
// --------------------------------------------------------------------------------
/// <summary>
/// 进入分数结算页面
/// </summary>
// --------------------------------------------------------------------------------

    public void GoEnd()
     {
        isGamePaused = true;
        StopAllCoroutines();
        SceneManager.LoadScene("StartScene");
        endUI.SetActive(true);
        ShowInfo();
        startUI.SetActive(false);
        gameUI.SetActive(false);
        configUI.SetActive(false);
        gameBGM.Stop();
        waterBGM.Stop();
    }
    public void ContinueGame()
    {
        isGamePaused = false;
       
        
    }
}

