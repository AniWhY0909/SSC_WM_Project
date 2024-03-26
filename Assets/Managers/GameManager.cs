using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public BallData[] ballDatas;
    public BallData[] nextBall;

    public GameObject ballSpawnPoint;
    public GameObject ballPrefab;
    public Ball lastBall;
    public TextMeshProUGUI scoreTMP;
    public TextMeshProUGUI highestscoreTMP;
    public Image nextImage;


    public int currentScore;
    public int nextBallLevel;
    public int currentBallLevel;

    private static GameManager instance;

    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<GameManager>();
                if (instance == null)
                {
                    GameObject managerObject = new GameObject("Game Manager");
                    instance = managerObject.AddComponent<GameManager>();
                }
            }

            return instance;
        }

    }

    private void Awake()
    {
        ballSpawnPoint = GameObject.Find("BallSpawnPoint");
    }

    private void Start()
    {
        currentBallLevel = Random.Range(0, 5);
        nextBallLevel = Random.Range(0, 5);

        UpdateNextBall(nextBallLevel);

        scoreTMP.text = "" +
            "0";
        highestscoreTMP.text = ProjectManager.Instance.HighestScore.ToString();
        Nextball();
    }

    private void FixedUpdate()
    {
        if (lastBall == null || lastBall.isDrop)
        {
            Nextball();
        }
    }

    public void UpdateScore(int value)
    {
        currentScore += (value + 1) * value;
        //Debug.Log($"{score}: {(score + 1) * score} : {currentScore}");
        scoreTMP.text = "score : " + currentScore.ToString();
    }

    public void UpdateNextBall(int level)
    {
        currentBallLevel = nextBallLevel;
        nextImage.sprite = nextBall[nextBallLevel].ballImage;
    }

    public void GameOver()
    {
        ProjectManager.Instance.Score = currentScore;

        SceneManager.LoadScene("EndScene");
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
