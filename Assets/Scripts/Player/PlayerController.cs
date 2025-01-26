using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private static PlayerController instance;
    public static PlayerController Instance { get { return instance; } }

    public Animator animator;

    private bool isAlive = true;
    public void SetIsAlive(bool isAlive) { this.isAlive = isAlive; }
    public bool GetIsAlive() { return isAlive; }

    private bool isGrounded = false;
    public void SetIsGrounded(bool isGrounded) {  this.isGrounded = isGrounded;}
    public bool GetIsGrounded() {  return isGrounded; }

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
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.y < -5)
        {
            Die();
        }
    }

    public void Die()
    {
        isAlive = false;
        GameOver();
    }

    public void GameOver()
    {
        SceneController.Instance.LoadSceneWithName("SampleScene");
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            animator.SetBool("Jump", false);
            isGrounded = true;
        }
        else if (collision.gameObject.CompareTag("Obstacle"))
        {
            animator.SetTrigger("Attack");
            collision.rigidbody.AddForce(new Vector3(1, 1, 0) * 25, ForceMode.Impulse);
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
}
