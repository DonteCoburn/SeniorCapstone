using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPage : MonoBehaviour
{
    [Tooltip("The default UI to have selected when opening this page")]
    public GameObject defaultSelected;

    void Start()
    {
        // Initialization logic, if needed
    }

    public void SetSelectedUIToDefault()
    {
        if (GameManager.instance != null && GameManager.instance.uiManager != null && defaultSelected != null)
        {
            GameManager.instance.uiManager.eventSystem.SetSelectedGameObject(null);
            GameManager.instance.uiManager.eventSystem.SetSelectedGameObject(defaultSelected);
        }
    }
}

