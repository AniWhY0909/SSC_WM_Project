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

    public int HighestScore { get; private set; }

    private void Awake()
    {
        if(instance == this)
        {
            DontDestroyOnLoad(gameObject);
        }

        else
        {
            Destroy(gameObject);
        }
    }
}
