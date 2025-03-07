using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinController : MonoBehaviour
{
    public Animator animator;

    [SerializeField] float rotationSpeed = 100f;
    private int point = 1;

    private void Start()
    {
        point = 1;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, rotationSpeed  * Time.deltaTime, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            SoundController.Instance.PlayOneShot(SoundController.Instance.pickUpCoin);
            GameManager.Instance.UpdateCoin(point);
            Destroy(gameObject);
        }
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
