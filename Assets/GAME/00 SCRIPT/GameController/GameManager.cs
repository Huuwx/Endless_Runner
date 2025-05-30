using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Serialization;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance { get { return instance; } }
    
    [Header("-----SINGLETON-----")]
    [SerializeField] PlayerController player;
    public PlayerController Player { get { return player; } }
    
    [SerializeField] private ItemManager _itemManager;
    public ItemManager ItemManager { get { return _itemManager; } }
    
    [FormerlySerializedAs("_itemController")] [SerializeField] private ItemAdapter itemAdapter;
    public ItemAdapter ItemAdapter { get { return itemAdapter; } }
    
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
    [SerializeField] TextMeshProUGUI gameOverTxt;
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
                //GameController.Instance.SceneController.LoadSceneWithName("SampleScene");
                if (Player.playerParameters.Lives <= 0)
                {
                    StartCoroutine(GameController.Instance.SceneController.LoadLevel("SampleScene"));
                }
                else
                {
                    StartCoroutine(GameController.Instance.SceneController.WaitForRevivePlayer());
                }

                gameOver.SetActive(false);
                isGameOver = false;
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
        if (Player.playerParameters.Lives <= 0)
        {
            gameOverTxt.text = "Tap to Restart";
        }
        else
        {
            gameOverTxt.text = "Tap to Save Player";
        }

        gameOver.SetActive(true);
    }
}
