using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject mTutorialPanel = null;
    [SerializeField] private GameObject mEndCardPanel = null;
    [SerializeField] private GameObject mSwipeHandlerPanel = null;
    private static UIManager _instance;
    public static UIManager Instance => _instance;

    public GameObject MTutorialPanel => mTutorialPanel;
    public GameObject MEndCardPanel => mEndCardPanel;
    public GameObject MSwipeHandlerPanel => mSwipeHandlerPanel;

    private void Awake()
    {
        if (_instance != null && _instance != this)
            Destroy(this.gameObject);
        else
            _instance = this;
    }
}
