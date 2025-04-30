using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    private PlayerMovement playerMovement;

    public Animator animator;
    
    public PlayerParameters playerParameters = new PlayerParameters();
    
    [SerializeField] Collider playerCollider;
    
    public bool isGrounded = false;
    [SerializeField] LayerMask groundLayer;

    [SerializeField] private Transform rcPoint;

    [SerializeField] float rcDistance;
    [SerializeField]  float rayCount;

    
    private ItemIndex checkItem;


    public void Init()
    {
        isGrounded = false;
        playerCollider = GetComponent<Collider>();
        playerMovement = gameObject.GetComponent<PlayerMovement>();
        animator = gameObject.GetComponentInChildren<Animator>();
        playerMovement.Init();
    }
    
    // private void Awake()
    // {
    //     isGrounded = false;
    //     playerCollider = GetComponent<Collider>();
    //     playerMovement = gameObject.GetComponent<PlayerMovement>();
    // }

    // Start is called before the first frame update
    // void Start()
    // {
    //     animator = gameObject.GetComponentInChildren<Animator>();
    // }

    // Update is called once per frame
    void Update()
    {
        CheckGrounded();
        //if(transform.position.y < -5)
        //{
        //    Die();
        //}
    }

    public void CheckGrounded()
    {
        float height = playerCollider.bounds.size.z;
        float step = height / (rayCount - 1);
        int groundHitCount = 0;
        for (int i = 0; i < rayCount; i++)
        {
            Vector3 origin = new  Vector3(rcPoint.position.x, rcPoint.position.y, rcPoint.position.z - height / 2 + i  * step);
            RaycastHit hit;
            bool test = Physics.Raycast(origin, Vector3.down, out hit, rcDistance,  groundLayer);
            if (test)
            {
                // Debug.Log(hit.transform.name);
                // Debug.Log(LayerMask.LayerToName(hit.collider.gameObject.layer));
                groundHitCount++;
            }
            
            Debug.DrawRay(origin, Vector3.down * rcDistance, test ? Color.green : Color.red);
            if (groundHitCount == 0)
            {
                if (!isGrounded)
                    continue;
                
                isGrounded = false;
                animator.SetBool(CONSTANT.Jump, true);
            }
            else
            {
                if (isGrounded)
                    continue;
                
                isGrounded = true;
                animator.SetBool(CONSTANT.Jump, false);
                GameController.Instance.SoundController.PlayOneShot(GameController.Instance.SoundController.jump_land);
            }
        }
    }

    private void OnDrawGizmos()
    {
        // float height = playerCollider.bounds.size.z;
        // float step = height / (rayCount - 1);
        // for (int i = 0; i < rayCount; i++)
        // {
        //     Vector3 origin = new  Vector3(rcPoint.position.x, rcPoint.position.y, rcPoint.position.z - height / 2 + i  * step);
        //     Debug.DrawRay(origin, Vector3.down * rcDistance, Color.red);
        // }
    }

    public void Die()
    {
        playerParameters.State = PlayerState.Dead;
        if (playerMovement.GetIsZPositive())
        {
            playerMovement.rb.AddForce(new Vector3(0, 1, -1) * 5, ForceMode.Impulse);
        }
        else
        {
            playerMovement.rb.AddForce(new Vector3(-1, 1, 0) * 5, ForceMode.Impulse);
        }
        playerMovement.rb.excludeLayers |= (1 << LayerMask.NameToLayer("Barrier"));
        GameController.Instance.SoundController.PlayOneShot(GameController.Instance.SoundController.death);
        animator.SetTrigger(CONSTANT.Death);
        GameOver();
    }

    public void GameOver()
    {
        GameManager.Instance.GameOver();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (playerParameters.IsAlive)
        {
            if (collision.gameObject.CompareTag(CONSTANT.BridgeTag))
            {
                playerMovement.BackToOldLane();
            }
            else if (collision.gameObject.CompareTag(CONSTANT.BounceTag))
            {
                GameController.Instance.SoundController.PlayOneShot(GameController.Instance.SoundController.bound);
                Animator Banimator = collision.gameObject.GetComponent<Animator>();
                Banimator.SetTrigger(CONSTANT.Activate);
                playerMovement.Bounce();
                animator.SetBool(CONSTANT.Jump, true);
            }

            checkItem = collision.gameObject.GetComponent<ItemIndex>();
            if (!checkItem)
                return;
            
            playerMovement.ResetCollider();
            GameController.Instance.SoundController.PlayOneShot(GameController.Instance.SoundController.atk_Sword);
            animator.SetTrigger(CONSTANT.Attack);
            collision.rigidbody.AddForce(new Vector3(1, 1, 0) * 25, ForceMode.Impulse);
            collision.rigidbody.excludeLayers |= (1 << LayerMask.NameToLayer("Player"));
            GameManager.Instance.ItemManager.ChangeItem(checkItem.index);
            GameManager.Instance.ItemController.ItemUseTime();
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        // if (playerParameters.IsAlive)
        // {
        //     if (collision.gameObject.CompareTag("Ground"))
        //     {
        //         isGrounded = true;
        //     }
        // }
    }

    private void OnCollisionExit(Collision collision)
    {
        // if (playerParameters.IsAlive)
        // {
        //     if (collision.gameObject.CompareTag("Ground"))
        //     {
        //         isGrounded = false;
        //     }
        // }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (playerParameters.IsAlive)
        {
            checkItem = other.gameObject.GetComponent<ItemIndex>();
            if (!checkItem)
                return;
            
            GameManager.Instance.ItemManager.ChangeItem(checkItem.index);
            GameManager.Instance.ItemController.ItemUseTime();
            other.gameObject.SetActive(false);
        }
    }
}
