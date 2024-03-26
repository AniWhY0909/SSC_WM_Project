using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayUI : MonoBehaviour
{
    public GameObject menuUI;
    public GameObject gameoverUI;


    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (EventSystem.current.IsPointerOverGameObject())
            {
                GameManager.Instance.touchUI = true;
            }

            else if(menuUI.activeSelf == true && !EventSystem.current.IsPointerOverGameObject())
            {
                ClickExit();
            }

            else
            {
                GameManager.Instance.touchUI = false;
            }
        }
    }

    public void ClickMenu()
    {
        menuUI.SetActive(true);
    }

    public void ClickExit()
    {
        menuUI.SetActive(false);
    }

    public void MusicOnOffBtn()
    {
        if (GameManager.Instance.audioSource.isPlaying)
        {
            GameManager.Instance.audioSource.Stop();
        }

        else if (!GameManager.Instance.audioSource.isPlaying)
        {
            GameManager.Instance.audioSource.Play();
        }
    }

    public void RestartBtn()
    {
        GameManager.Instance.RestartGame();;
    }

}
