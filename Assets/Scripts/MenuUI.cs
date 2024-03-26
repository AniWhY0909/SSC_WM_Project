using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuUI : MonoBehaviour
{
    public GameObject menuUI;

    public void ClickMenu()
    {
        menuUI.SetActive(true);
    }

    public void ClickExit()
    {
        menuUI.SetActive(false);
    }
}
