using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance { get { return instance; } }

    [SerializeField] TextMeshProUGUI StartingText;
    public bool isStarted;

    [SerializeField] TextMeshProUGUI coinNumberTxt;
    private int coinNumber;

    [SerializeField] GameObject gameOver;
    public bool isGameOver;

    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

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
            PlayerController.Instance.animator.SetTrigger("Start");
            Destroy(StartingText);
        }
    }

    public void UpdateCoin()
    {
        coinNumber++;
        coinNumberTxt.text = coinNumber.ToString();
    }

    public void GameOver()
    {
        isGameOver = true;
        gameOver.SetActive(true);
    }
}
