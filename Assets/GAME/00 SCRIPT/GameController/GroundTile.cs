using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundTile : MonoBehaviour
{
    [SerializeField] List<SpikeTrapController> spikeTrapController;
    [SerializeField] List<CannonController> cannonController;

    //private void OnTriggerExit(Collider other)
    //{
    //    if (other.gameObject.tag == "Player")
    //    {
    //        GroundSpawner.Instance.SpawnTile();
    //        Destroy(gameObject, 2);
    //    }
    //}

    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag(CONSTANT.PlayerTag))
            return;
        
        if (spikeTrapController != null)
        {
            foreach (var controller in spikeTrapController)
            {
                controller.animator.SetTrigger(CONSTANT.Activate);
            }
        }
        if (cannonController != null)
        {
            foreach (var controller in cannonController)
            {
                controller.SetIsActive(true);
            }
        }
    }
}
