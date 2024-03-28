using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayUI : MonoBehaviour
{
    public GameObject menuUI;
    public GameObject gameoverUI;
    public GameObject soundoff;

    public void ClickMenu()
    {
        menuUI.SetActive(true);
        GameManager.Instance.touchUIIndex = 10;
    }

    public void ClickExit()
    {
        menuUI.SetActive(false);
        GameManager.Instance.touchUIIndex = 9;
    }

    public void MusicOnOffBtn()
    {
        if (GameManager.Instance.audioSource.isPlaying)
        {
            GameManager.Instance.audioSource.Stop();
            soundoff.SetActive(true);
        }

        else if (!GameManager.Instance.audioSource.isPlaying)
        {
            GameManager.Instance.audioSource.Play();
            soundoff.SetActive(false);
        }
    }

    public void RestartBtn()
    {
        GameManager.Instance.RestartGame();;
    }

}
