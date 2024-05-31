using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;



public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }
    // Start is called before the first frame update
    public float targetScore;
    public int score;
    public float sumScore;

    Player player;



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
    void Start()
    {
        score = 0;
       
        

    }

    // Update is called once per frame
    void Update()
    {
        //�����ܷ�
        //�㷨һ���п�ʼ�ռ�����
        if (score > 0)
        {
            //sumScore = (score / totalScore) * 90f + (GameManager.Instance.countdownTimer / GameManager.Instance.gameDuration) * 10f;
            sumScore = score + (GameManager.Instance.countdownTimer / GameManager.Instance.gameDuration) * 50f;
        }
        else
        {
            sumScore = 0;
        }
        
  
    }
    public void AddScore(int points)
    {
        score += points;
        Debug.Log("�÷����ӣ�" + points);

    }
    public void SubtractScore(int points)
    {
        score -= points;
        Debug.Log("�÷ּ��٣�" + points);     
    }   

}
