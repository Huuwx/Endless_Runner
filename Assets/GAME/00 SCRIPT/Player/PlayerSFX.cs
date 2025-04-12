using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSFX : MonoBehaviour
{
    public void FootStep()
    {
        GameManager.Instance.SoundController.PlayOneShot(GameManager.Instance.SoundController.run);
    }
}
