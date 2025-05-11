using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    [SerializeField] Animator transitionAnim;
    [SerializeField] GameObject imgTransition;
    [SerializeField] private TextMeshProUGUI countdownText;
    private float countdownDuration = 3f;
    
    private CinemachineBrain cinemachineBrain;
    
    //[SerializeField] private CinemachineVirtualCamera virtualCamera;
    
    private void Awake()
    {
        cinemachineBrain = Camera.main.GetComponent<CinemachineBrain>();
    }

    private void Start()
    {
        // if (Camera.main != null)
        // {
        //     var brain = Camera.main.GetComponent<CinemachineBrain>();
        //     if (brain != null)
        //     {
        //         brain.m_UpdateMethod = CinemachineBrain.UpdateMethod.LateUpdate;
        //     }
        // }
    }

    public void LoadSceneWithName(string name)
    {
        SceneManager.LoadScene(name);
    }

    public IEnumerator LoadLevel(string name)
    {
        imgTransition.SetActive(true);
        transitionAnim.SetTrigger("End");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(name);
        transitionAnim.SetTrigger("Start");
        yield return new WaitForSeconds(1f);
        imgTransition.SetActive(false);
    }

    public IEnumerator WaitForRevivePlayer()
    {
        Time.timeScale = 0;
        imgTransition.SetActive(true);
        transitionAnim.SetTrigger("End");
        yield return new WaitForSecondsRealtime(1f);
        
        GameManager.Instance.Player.Revive();
        
        if (cinemachineBrain != null)
        {
            cinemachineBrain.m_IgnoreTimeScale = true;
        }
        yield return new WaitForSecondsRealtime(0.3f);
        
        transitionAnim.SetTrigger("Start");
        yield return new WaitForSecondsRealtime(1f);
        
        imgTransition.SetActive(false);
        
        countdownText.gameObject.SetActive(true);
        float timeLeft = countdownDuration;
        while (timeLeft > 0)
        {
            timeLeft -= Time.unscaledDeltaTime;
            countdownText.text = Mathf.CeilToInt(timeLeft).ToString();
            yield return null;
        }
        countdownText.gameObject.SetActive(false);
        
        Time.timeScale = 1;
        
        if (cinemachineBrain != null)
        {
            cinemachineBrain.m_IgnoreTimeScale = false;
        }
        
    }
}
