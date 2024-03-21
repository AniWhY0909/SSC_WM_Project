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
    public TextMeshProUGUI tmp;
    public Image image;


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
        tmp = GameObject.Find("ScoreText").GetComponent<TextMeshProUGUI>();
        image = GameObject.Find("NextBallImage").GetComponent<Image>();
    }

    private void Start()
    {
        currentBallLevel = Random.Range(0, 5);
        nextBallLevel = Random.Range(0, 5);

        UpdateNextBall(nextBallLevel);

        tmp.text = " score : 0";
        Nextball();
    }

    private void FixedUpdate()
    {
        if (lastBall == null || lastBall.isDrop)
        {
            Nextball();
        }
    }

    public void UpdateScore(int score)
    {
        currentScore += (score + 1) * score;
        //Debug.Log($"{score}: {(score + 1) * score} : {currentScore}");
        tmp.text = "score : " + currentScore.ToString();
    }

    public void UpdateNextBall(int level)
    {
        image.sprite = nextBall[nextBallLevel].ballImage;
        currentBallLevel = nextBallLevel;
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
