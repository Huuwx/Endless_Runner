using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance { get { return instance; } }
    
    [Header("-----SINGLETON-----")]
    [SerializeField] PlayerController player;
    public PlayerController Player { get { return player; } }
    
    [SerializeField] private ItemManager _itemManager;
    public ItemManager ItemManager { get { return _itemManager; } }
    
    [SerializeField] private ItemController _itemController;
    public ItemController ItemController { get { return _itemController; } }
    
    [SerializeField] ParticleSystemController _particleController;
    public ParticleSystemController ParticleController { get { return _particleController; } }
    
    [SerializeField] ObjectPooling _objectPooling;
    public ObjectPooling ObjectPooling { get { return _objectPooling; } }

    [Header("-----UI-----")]
    [SerializeField] TextMeshProUGUI StartingText;
    public bool isStarted;

    [SerializeField] TextMeshProUGUI coinNumberTxt;
    private int coinNumber;

    [SerializeField] GameObject gameOver;
    public bool isGameOver;


    void Init()
    {
        isGameOver = false;
        isStarted = false;
        coinNumber = 0;
        coinNumberTxt.text = coinNumber.ToString();
    }
    
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
        Init();
        Player.Init();
    }

    // Start is called before the first frame update
    // void Start()
    // {
    //     isGameOver = false;
    //     isStarted = false;
    //     coinNumber = 0;
    //     coinNumberTxt.text = coinNumber.ToString();
    // }

    // Update is called once per frame
    void Update()
    {
        if (SwipeManager.tap)
        {
            if(isGameOver)
            {
                GameController.Instance.SceneController.LoadSceneWithName("SampleScene");
                return;
            }
            isStarted = true;
            Player.animator.SetTrigger(CONSTANT.Start);
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
