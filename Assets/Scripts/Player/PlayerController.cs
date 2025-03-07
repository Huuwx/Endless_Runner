using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    private static PlayerController instance;
    public static PlayerController Instance { get { return instance; } }

    private PlayerMovement playerMovement;

    public Animator animator;

    //private bool immortal = false;
    //public void SetImmortal(bool immortal) { this.immortal = immortal; }
    //public bool GetImmortal() { return immortal; }

    //private bool isAlive = true;
    //public void SetIsAlive(bool isAlive) { this.isAlive = isAlive; }
    //public bool GetIsAlive() { return isAlive; }

    //private bool isGrounded = false;
    //public void SetIsGrounded(bool isGrounded) { this.isGrounded = isGrounded; }
    //public bool GetIsGrounded() { return isGrounded; }

    private void Awake()
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

    // Start is called before the first frame update
    void Start()
    {
        animator = gameObject.GetComponentInChildren<Animator>();
        playerMovement = gameObject.GetComponent<PlayerMovement>();
        PlayerParameters.Instance.SetImmortal(false);
        PlayerParameters.Instance.SetIsAlive(true);
        PlayerParameters.Instance.SetIsGrounded(false);
    }

    // Update is called once per frame
    void Update()
    {
        //if(transform.position.y < -5)
        //{
        //    Die();
        //}
    }

    public void Die()
    {
        PlayerParameters.Instance.SetIsAlive(false);
        if (playerMovement.GetIsZPositive())
        {
            playerMovement.rb.AddForce(new Vector3(0, 1, -1) * 5, ForceMode.Impulse);
        }
        else
        {
            playerMovement.rb.AddForce(new Vector3(-1, 1, 0) * 5, ForceMode.Impulse);
        }
        playerMovement.rb.excludeLayers |= (1 << LayerMask.NameToLayer("Barrier"));
        SoundController.Instance.PlayOneShot(SoundController.Instance.death);
        animator.SetTrigger("Death");
        GameOver();
    }

    public void GameOver()
    {
        GameManager.Instance.GameOver();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (PlayerParameters.Instance.GetIsAlive())
        {
            if (collision.gameObject.CompareTag("Ground"))
            {

                //SoundController.Instance.PlayOneShot(SoundController.Instance.jump_land);

                animator.SetBool("Jump", false);
                PlayerParameters.Instance.SetIsGrounded(true);
            }
            else if (collision.gameObject.CompareTag("Obstacle"))
            {
                playerMovement.ResetCollider();
                SoundController.Instance.PlayOneShot(SoundController.Instance.atk_Sword);
                animator.SetTrigger("Attack");
                collision.rigidbody.AddForce(new Vector3(1, 1, 0) * 25, ForceMode.Impulse);
                collision.rigidbody.excludeLayers |= (1 << LayerMask.NameToLayer("Player"));
                ItemController.Instance.MagnetUseTime();
                ItemController.Instance.ResetUseMagnetTime();
            }
            else if (collision.gameObject.CompareTag("Bridge"))
            {
                playerMovement.BackToOldLane();
            }
            else if (collision.gameObject.CompareTag("Bounce"))
            {
                SoundController.Instance.PlayOneShot(SoundController.Instance.bound);
                Animator Banimator = collision.gameObject.GetComponent<Animator>();
                Banimator.SetTrigger("Activate");
                playerMovement.Bounce();
                animator.SetBool("Jump", true);
            }
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (PlayerParameters.Instance.GetIsAlive())
        {
            if (collision.gameObject.CompareTag("Ground"))
            {
                PlayerParameters.Instance.SetIsGrounded(true);
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (PlayerParameters.Instance.GetIsAlive())
        {
            if (collision.gameObject.CompareTag("Ground"))
            {
                PlayerParameters.Instance.SetIsGrounded(false);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (PlayerParameters.Instance.GetIsAlive())
        {
            if (other.gameObject.CompareTag("Shield"))
            {
                ItemController.Instance.MagicShieldUseTime();
                ItemController.Instance.ResetMagicShieldUseTime();
                Destroy(other.gameObject);
            }
            else if (other.gameObject.CompareTag("X2"))
            {
                ItemController.Instance.ResetX2UseTime();
                Destroy(other.gameObject);
            }
        }
    }
}
