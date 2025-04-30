using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSFX : MonoBehaviour
{
    public void FootStep()
    {
        GameController.Instance.SoundController.PlayOneShot(GameController.Instance.SoundController.run);
    }
}
