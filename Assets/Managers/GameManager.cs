using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public BallData[] ballDatas;

    public Transform ballSpawnPoint;
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
        if (lastBall.isDrop)
        {
            Nextball();
        }
    }

    Ball GetBall()
    {
        GameObject ball = Instantiate(ballPrefab, ballSpawnPoint);
        Ball Ball = ball.GetComponent<Ball>();
        return Ball;
    }

    public void Nextball()
    {
        Ball newBall = GetBall();
        lastBall = newBall;
    }
}
