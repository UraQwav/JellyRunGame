using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance => _instance;
    private void Awake()
    {
        if (_instance != null && _instance != this)
            Destroy(this.gameObject);
        else
            _instance = this;
    }
    
    public void StartGame()
    {
        UIManager.Instance.MTutorialPanel.SetActive(false);
    }
    public void ShowEndCard()
    {
        UIManager.Instance.MSwipeHandlerPanel.SetActive(false);
        UIManager.Instance.MEndCardPanel.SetActive(true);
    }
    public void GoToStore()
    {
        print("Go To Store");
    }
}
