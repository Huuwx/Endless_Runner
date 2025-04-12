using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    private static ItemController instance;
    public static ItemController Instance { get { return instance; } }
    
    [SerializeField] ItemBase _item;

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

    public void SetItem(ItemBase item)
    {
        _item = item;
    }
    
    public void ItemtUseTime()
    {
        _item.gameObject.SetActive(true);
    }

    // // Coin Magnet
    // public void MagnetUseTime()
    // {
    //     magnet.SetActive(true);
    // }
    //
    // public void ResetUseMagnetTime()
    // {
    //     magnetController.ResetUseTime();
    // }
    //
    // public void OutOfTimeToUseMagnet()
    // {
    //     magnetController.ClearUseTime();
    //     magnet.SetActive(false);
    // }
    //
    // // X2
    // public void ResetX2UseTime()
    // {
    //     x2CoinItemController.ResetUseTime();
    // }
    //
    // // Magic Shield
    // public void MagicShieldUseTime()
    // {
    //     magicShield.SetActive(true);
    // }
    //
    // public void ResetMagicShieldUseTime()
    // {
    //     magicShieldController.ResetUseTime();
    // }
    //
    // public void OutOfTimeToUseMagicShield()
    // {
    //     magicShieldController.ClearUseTime();
    //     magicShield.SetActive(false);
    // }
}
