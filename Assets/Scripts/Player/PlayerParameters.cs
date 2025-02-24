using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerParameters : MonoBehaviour
{
    public static PlayerParameters Instance {  get; private set; }

    [Header("Player Stats")]
    public float jumpForce = 17f;

    [Header("Player State")]
    public bool immortal = false;
    public bool isAlive = true;
    public bool isGrounded = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            /*DontDestroyOnLoad(gameObject);*/
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
