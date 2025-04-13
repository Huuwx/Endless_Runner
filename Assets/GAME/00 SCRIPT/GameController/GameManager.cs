using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance { get { return instance; } }
    
    [Header("-----SINGLETON-----")]
    [SerializeField] PlayerController player;
    public PlayerController Player { get { return player; } }
    
    [SerializeField] SoundController soundController;
    public SoundController SoundController { get { return soundController; } }
    
    [SerializeField] private ItemManager _itemManager;
    public ItemManager ItemManager { get { return _itemManager; } }
    
    [SerializeField] private ItemController _itemController;
    public ItemController ItemController { get { return _itemController; } }

    [Header("-----UI-----")]
    [SerializeField] TextMeshProUGUI StartingText;
    public bool isStarted;

    [SerializeField] TextMeshProUGUI coinNumberTxt;
    private int coinNumber;

    [SerializeField] GameObject gameOver;
    public bool isGameOver;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        isGameOver = false;
        isStarted = false;
        coinNumber = 0;
        coinNumberTxt.text = coinNumber.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (SwipeManager.tap)
        {
            if(isGameOver)
            {
                SceneManager.LoadScene("SampleScene");
                return;
            }
            isStarted = true;
            Player.animator.SetTrigger("Start");
            Destroy(StartingText);
        }
    }

    public void UpdateCoin(int point)
    {
        coinNumber += point;
        coinNumberTxt.text = coinNumber.ToString();
    }

    public void GameOver()
    {
        isGameOver = true;
        gameOver.SetActive(true);
    }
}
