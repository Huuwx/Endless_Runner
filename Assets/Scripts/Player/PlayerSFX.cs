using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSFX : MonoBehaviour
{
    public void FootStep()
    {
        SoundController.Instance.PlayOneShot(SoundController.Instance.run);
    }
}
