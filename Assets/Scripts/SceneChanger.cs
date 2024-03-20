using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{

    public void SceneChange()
    {
        if(SceneManager.GetActiveScene().name == "TitleScene")
        {
            SceneManager.LoadScene("GameScene");
        }

        if(SceneManager.GetActiveScene().name == "EndScene")
        {
            SceneManager.LoadScene("TitleScene");
        }
    }

}
