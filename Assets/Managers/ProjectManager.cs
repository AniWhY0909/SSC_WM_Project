using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectManager : MonoBehaviour
{
    private static ProjectManager instance;

    public static ProjectManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<ProjectManager>();
                if (instance == null)
                {
                    GameObject gameObject = new GameObject("Project Manager");
                    instance = gameObject.AddComponent<ProjectManager>();
                }

            }
            return instance;
        }
    }

    private int score;

    public int Score
    {
        get => score;

        set
        {
            HighestScore = Mathf.Max(score, value);
            score = value;
        }
    }

    private int highestScore;
    public int HighestScore
    {
        get => highestScore; 
        
        private set
        {
            highestScore = value;
            PlayerPrefs.SetInt("Highest Score", value);
        }
    }

    private void Awake()
    {
        if (instance == this)
        {
            DontDestroyOnLoad(gameObject);
        }

        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        highestScore = PlayerPrefs.GetInt("Highest Score");
    }
}
