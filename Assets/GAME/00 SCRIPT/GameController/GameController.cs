using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private static GameController instance;
    public static GameController Instance { get { return instance; } }
    
    [Header("-----SINGLETON-----")]
    [SerializeField] SoundController soundController;
    public SoundController SoundController { get { return soundController; } }
    
    [SerializeField] SceneController _sceneController;
    public SceneController SceneController { get { return _sceneController; } }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
