using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunnerGround : MonoBehaviour
{

    private void Update()
    {
        if (PlayerController.Instance.GetIsAlive() == true && GameManager.Instance.isStarted)
        {
            transform.Translate(Vector3.back * 10 * Time.deltaTime, Space.World);
        }
    }
}
