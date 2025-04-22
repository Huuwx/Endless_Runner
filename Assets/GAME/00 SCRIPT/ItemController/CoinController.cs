using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinController : MonoBehaviour
{
    Vector3 startPos;
    
    public Animator animator;

    private int point = 1;

    private void Awake()
    {
        point = 1;
        animator = GetComponent<Animator>();
        startPos = transform.localPosition;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag(CONSTANT.PlayerTag))
            return;
        
        GameManager.Instance.SoundController.PlayOneShot(GameManager.Instance.SoundController.pickUpCoin);
        GameManager.Instance.UpdateCoin(point);
        gameObject.SetActive(false);
    }

    public int GetPoint()
    {
        return point;
    }

    public void SetPoint(int value)
    {
        point = value;
    }

    public void Activate()
    {
        transform.localPosition = startPos;
        animator.SetBool(CONSTANT.X2, false);
        this.gameObject.SetActive(true);
    }
}
