using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public BallData[] ballDatas;

    public GameObject ballSpawnPoint;
    public GameObject ballPrefab;
    public Ball lastBall;
    
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
    }

    private void Start()
    {
        Nextball();    
    }

    private void FixedUpdate()
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
            if (lastBall.isDrop  || lastBall == null)
            {
                Nextball();
            }
        }
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
