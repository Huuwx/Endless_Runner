using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerParameters : MonoBehaviour
{
    public static PlayerParameters Instance {  get; private set; }

    [Header("Player Stats")]
    [SerializeField] float jumpForce = 17f;

    [Header("Player State")]
    private bool immortal = false;
    private bool isAlive = true;
    private bool isGrounded = false;

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

    public float GetJumpForce()
    {
        return jumpForce;
    }

    public bool GetImmortal()
    {
        return immortal;
    }

    public bool GetIsAlive()
    {
        return isAlive;
    }

    public bool GetIsGrounded()
    {
        return isGrounded;
    }


    public void SetImmortal(bool value)
    {
        immortal = value;
    }

    public void SetIsAlive(bool value)
    {
        isAlive = value;
    }

    public void SetIsGrounded(bool value)
    {
        isGrounded = value;
    }
}
