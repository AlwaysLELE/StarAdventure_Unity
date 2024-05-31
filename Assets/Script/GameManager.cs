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
    public GameObject startUI; // ��ʼ��Ϸ��UI���
    public GameObject gameUI;//��Ϸ�����е�UI���
    public GameObject endUI; // ��Ϸ������UI���
    public GameObject configUI;//�ﵽĿ��������UI���
    public TextMeshProUGUI scoreText_end;//endUI�����ı�
    public TextMeshProUGUI scoreText_game;//gameUI�����ı�
    public TextMeshProUGUI countdownText; // ����ʱ�ı�
    public TextMeshProUGUI successedInfo;//ͨ����ʾ��ʾ�ı�
    public TextMeshProUGUI failedInfo;//ʧ����ʾ��ʾ�ı�

    public float gameDuration = 300f; // ��Ϸʱ��
    public float countdownTimer; // ����ʱ��ʱ��
    bool isActive = false;
    public AudioSource startBGM;
    public AudioSource timerBGM;
    public AudioSource failedBGM;
    public AudioSource successedBGM;
    public AudioSource gameBGM;
    public AudioSource waterBGM;
    
    //��Ϸ��ͣ
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

        // ֻ��ʾ��ʼ��Ϸ��UI
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

        //������н���ʱ����ʷ��Ϣ
        ClearScore();
        HideInfo();
        StopAllCoroutines();
        
        startBGM.Stop();
        gameBGM.Play();
        waterBGM.Play();
        // �л�����Ϸ��������ʼ��ʱ
        SceneManager.LoadScene("GameScene");
        startUI.SetActive(false);
        endUI.SetActive(false);
        gameUI.SetActive(true);//��ʾ��Ϸ��UI
        countdownTimer = gameDuration;
        UpdateCountdownText();
        UpdateScoreText();
        isGamePaused = false;
        // ��������ʱЭ��
        StartCoroutine(CountdownCoroutine());
        
    }

    public void EndGame()
    {
        //Ԥ����
#if UNITY_EDITOR    //�ڱ༭��ģʽ��
        EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif

    }
    // --------------------------------------------------------------------------------
    /// <summary>
    /// �����ж��Ƿ�ﵽͨ�ط����ķ���
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
           
       
        // ����ʱѭ��      
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
        // ��Ϸ����ʱ���������س�ʼ����
        GoEnd();
    }

    private void UpdateCountdownText()
    {

        // ���µ���ʱ�ı���ʾ
        countdownText.text = countdownTimer.ToString();
    }
    private void UpdateScoreText()
    {
        //���·����ı���ʾ
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
    /// �������״̬��ʾ��Ϣ
    /// </summary>
    // --------------------------------------------------------------------------------
    private void HideInfo()
    {
        successedInfo.gameObject.SetActive(false);
        failedInfo.gameObject.SetActive(false);
    }
    public void BackToOrigin()
    {
        //������н���ʱ����ʷ��Ϣ
        StopAllCoroutines();
        ClearScore();
        HideInfo();
        // ֻ��ʾ��ʼ��Ϸ��UI
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
    /// ���������ʷ����
    /// </summary>
    // --------------------------------------------------------------------------------

    private void ClearScore()
    {
        ScoreManager.Instance.score = 0;
        Debug.Log(ScoreManager.Instance.score);
    }
// --------------------------------------------------------------------------------
/// <summary>
/// �����������ҳ��
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

