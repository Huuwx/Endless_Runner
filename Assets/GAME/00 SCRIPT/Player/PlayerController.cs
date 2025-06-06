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
    public Collider PlayerCollider
    {
        get { return playerCollider; }
        set { playerCollider = value; }
    }
    
    [SerializeField] BoxCollider boxCollider;
    public BoxCollider BoxCollider
    {
        get { return boxCollider; }
        set { boxCollider = value; }
    }
    
    private Rigidbody rb;
    public Rigidbody Rb
    {
        get { return rb; }
        set { rb = value; }
    }
    
    public bool isGrounded = false;
    [SerializeField] LayerMask groundLayer;

    [SerializeField] private Transform rcPoint;
    
    private Vector3 revivePoint;
    private float distanceOfRvPoint = 8f;

    [SerializeField] float rcDistance;
    [SerializeField]  float rayCount;

    
    private ItemIndex checkItem;


    public void Init()
    {
        isGrounded = false;
        rb = GetComponent<Rigidbody>();
        playerCollider = GetComponent<Collider>();
        boxCollider = playerCollider as BoxCollider;
        playerMovement = gameObject.GetComponent<PlayerMovement>();
        animator = gameObject.GetComponentInChildren<Animator>();
        playerMovement.Init();
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
            bool checkGround = Physics.Raycast(origin, Vector3.down, out hit, rcDistance,  groundLayer);
            if (checkGround)
            {
                groundHitCount++;
            }
            
            Debug.DrawRay(origin, Vector3.down * rcDistance, checkGround ? Color.green : Color.red);
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
            rb.AddForce(new Vector3(0, 1, -1) * 5, ForceMode.Impulse);
        }
        else
        {
            rb.AddForce(new Vector3(-1, 1, 0) * 5, ForceMode.Impulse);
        }
        rb.excludeLayers |= (1 << LayerMask.NameToLayer("Barrier"));
        GameController.Instance.SoundController.PlayOneShot(GameController.Instance.SoundController.death);
        animator.SetTrigger(CONSTANT.Death);
        GameManager.Instance.GameOver();
    }
    
    public void Revive()
    {
        playerParameters.Lives -= 1;
        
        // Calculate revive point first
        if(playerMovement.IsZPositive)
        {
            if (playerMovement.CanTurn)
            {
                revivePoint = transform.position + Vector3.right * (distanceOfRvPoint/2);
            }
            else
            {
                revivePoint = transform.position + Vector3.forward * distanceOfRvPoint;
            }
        }
        else
        {
            if (playerMovement.CanTurn)
            {
                revivePoint = transform.position + Vector3.forward * (distanceOfRvPoint/2);
            }
            else
            {
                revivePoint = transform.position + Vector3.right * distanceOfRvPoint;
            }
        }
    
        // Set position first
        transform.position = revivePoint;
    
        // Then handle turns
        if(playerMovement.IsZPositive && playerMovement.CanTurn)
        {
            playerMovement.TurnRight();
        }
        else if(!playerMovement.IsZPositive && playerMovement.CanTurn)
        {
            playerMovement.TurnLeft();
        }
    
        animator.SetTrigger("Revive");
        playerParameters.State = PlayerState.Normal;
        rb.excludeLayers &= ~(1 << LayerMask.NameToLayer("Barrier"));
    }
    
    public void SetColliderSize(float x, float y, float z, float size)
    {
        if (boxCollider != null)
        {
            boxCollider.size = new Vector3(x, y, z);
            boxCollider.center = new Vector3(0, size, 0);
        }
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
            GameManager.Instance.ItemAdapter.ItemUseTime();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (playerParameters.IsAlive)
        {
            checkItem = other.gameObject.GetComponent<ItemIndex>();
            if (!checkItem)
                return;
            
            GameManager.Instance.ItemManager.ChangeItem(checkItem.index);
            GameManager.Instance.ItemAdapter.ItemUseTime();
            other.gameObject.SetActive(false);
        }
    }
}
