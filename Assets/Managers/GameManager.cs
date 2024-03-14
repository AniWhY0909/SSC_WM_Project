using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public BallData[] ballDatas;

    public GameObject ballSpawnPoint;
    public GameObject ballPrefab;
    
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

    public void SpawnBall()
    {
        Instantiate(ballPrefab, ballSpawnPoint.transform.position, Quaternion.identity);
    }
}
