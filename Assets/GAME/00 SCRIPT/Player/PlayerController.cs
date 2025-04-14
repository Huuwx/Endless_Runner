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

    private void Awake()
    {
        isGrounded = false;
        playerCollider = GetComponent<Collider>();
    }

    // Start is called before the first frame update
    void Start()
    {
        animator = gameObject.GetComponentInChildren<Animator>();
        playerMovement = gameObject.GetComponent<PlayerMovement>();
    }

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
                isGrounded = false;
            }
            else
            {
                isGrounded = true;
            }
        }
        
        if (!isGrounded)
        {
            animator.SetBool("Jump", true);
            return;
        }
        
        animator.SetBool("Jump", false);
    }

    private void OnDrawGizmos()
    {
        // float height = playerCollider.bounds.size.z;
        // float step = height / (rayCount - 1);
        // for (int i = 0; i <= 9; i++)
        // {
        //     Vector3 origin = new  Vector3(testt.position.x, testt.position.y, testt.position.z - height / 2 + i  * step);
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
        GameManager.Instance.SoundController.PlayOneShot(GameManager.Instance.SoundController.death);
        animator.SetTrigger("Death");
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
            if (collision.gameObject.CompareTag(CONSTANT.BoxTag))
            {
                playerMovement.ResetCollider();
                GameManager.Instance.SoundController.PlayOneShot(GameManager.Instance.SoundController.atk_Sword);
                animator.SetTrigger("Attack");
                collision.rigidbody.AddForce(new Vector3(1, 1, 0) * 25, ForceMode.Impulse);
                collision.rigidbody.excludeLayers |= (1 << LayerMask.NameToLayer("Player"));
                GameManager.Instance.ItemManager.ChangeItem(collision.gameObject.GetComponent<ItemIndex>().index);
                GameManager.Instance.ItemController.ItemUseTime();
            }
            else if (collision.gameObject.CompareTag(CONSTANT.BridgeTag))
            {
                playerMovement.BackToOldLane();
            }
            else if (collision.gameObject.CompareTag(CONSTANT.BounceTag))
            {
                GameManager.Instance.SoundController.PlayOneShot(GameManager.Instance.SoundController.bound);
                Animator Banimator = collision.gameObject.GetComponent<Animator>();
                Banimator.SetTrigger("Activate");
                playerMovement.Bounce();
                animator.SetBool("Jump", true);
            }
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
            if (other.gameObject.CompareTag(CONSTANT.ItemTag))
            {
                GameManager.Instance.ItemManager.ChangeItem(other.gameObject.GetComponent<ItemIndex>().index);
                GameManager.Instance.ItemController.ItemUseTime();
                other.gameObject.SetActive(false);
            }
        }
    }
}
