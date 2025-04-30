using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinController : MonoBehaviour
{
    Vector3 startPos;

    private int point = 1;
    
    SkinnedMeshRenderer skinnedMeshRenderer;
    [SerializeField] Material baseMaterial;
    [SerializeField] Material x2Material;

    public void Init()
    {
        point = 1;
        startPos = transform.localPosition;
        skinnedMeshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
    }
    
    // private void Awake()
    // {
    //     point = 1;
    //     startPos = transform.localPosition;
    //     skinnedMeshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
    // }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag(CONSTANT.PlayerTag))
            return;
        
        GameController.Instance.SoundController.PlayOneShot(GameController.Instance.SoundController.pickUpCoin);
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
        transform.rotation = Quaternion.identity;
        point = 1;
        x2Deactivate();
        this.gameObject.SetActive(true);
    }

    public void x2Activate()
    {
        Material[] mats = skinnedMeshRenderer.materials;
        mats[0] = x2Material;
        skinnedMeshRenderer.materials = mats;
    }

    public void x2Deactivate()
    {
        Material[] mats = skinnedMeshRenderer.materials;
        mats[0] = baseMaterial;
        skinnedMeshRenderer.materials = mats;
    }
}
