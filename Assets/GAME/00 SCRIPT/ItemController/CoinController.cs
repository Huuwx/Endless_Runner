using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinController : MonoBehaviour
{
    public Animator animator;

    private int point = 1;

    private void Awake()
    {
        point = 1;
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag(CONSTANT.PlayerTag))
            return;
        
        GameManager.Instance.SoundController.PlayOneShot(GameManager.Instance.SoundController.pickUpCoin);
        GameManager.Instance.UpdateCoin(point);
        Destroy(gameObject);
    }

    public int GetPoint()
    {
        return point;
    }

    public void SetPoint(int value)
    {
        point = value;
    }
}
