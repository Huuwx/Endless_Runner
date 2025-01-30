using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    private static ItemController instance;
    public static ItemController Instance { get { return instance; } }

    [SerializeField] GameObject magnet;
    [SerializeField] MagnetCoinController magnetController;

    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void UseTime()
    {
        magnet.SetActive(true);
    }

    public void ResetUseMagnetTime()
    {
        magnetController.ResetUseTime();
    }

    public void OutOfTimeToUseMagnet()
    {
        magnet.SetActive(false);
    }

    //public IEnumerator ActiveMagnet()
    //{
    //    magnet.SetActive(true);
    //    yield return new WaitForSeconds(timeToUse);
    //    magnet.SetActive(false);
    //}
}
