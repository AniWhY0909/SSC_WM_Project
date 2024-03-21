using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public BallData[] ballDatas;

    public GameObject ballSpawnPoint;
    public GameObject ballPrefab;
    public Ball lastBall;
    public TextMeshProUGUI tmp;


    public int currentScore;
    private static GameManager instance;

    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                return null;
            }
        
            return instance;
        }

    }
    
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(this.gameObject);

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void Start()
    {
        Nextball();    
    }

    private void FixedUpdate()
    {

        if (ballSpawnPoint != null && lastBall.isDrop)
        {
            Nextball();
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
    {
        if (SceneManager.GetActiveScene().name == "GameScene")
        {
            if (ballSpawnPoint == null)
            {
                ballSpawnPoint = GameObject.Find("BallSpawnPoint");

                if (lastBall == null)
                {
                    Nextball();
                }
            }

            if (tmp == null)
            {
                tmp = GameObject.Find("ScoreText").GetComponent<TextMeshProUGUI>();
                tmp.text = " score : 0";
            }
        }
    }

    public void UpdateScore(int score)
    {
        currentScore += (score + 1) * score;
        //Debug.Log($"{score}: {(score + 1) * score} : {currentScore}");
        tmp.text = "score : " + currentScore.ToString();
    }

    public void GameOver()
    {
        if(SceneManager.GetActiveScene().name == "GameScene")
        {
            SceneManager.LoadScene("EndScene");
        }
    }

    Ball GetBall()
    {
        if (ballSpawnPoint == null) return null;
        GameObject ball = Instantiate(ballPrefab, ballSpawnPoint.transform);
        Ball Ball = ball.GetComponent<Ball>();
        return Ball;
    }

    public void Nextball()
    {
        Ball newBall = GetBall();
        lastBall = newBall;
    }
}
