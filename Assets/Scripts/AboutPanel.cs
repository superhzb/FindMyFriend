using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AboutPanel : MonoBehaviour
{
    private Animator animator;

    private readonly int hashEnterPara = Animator.StringToHash("AboutEnter");
    private readonly int hashExitPara = Animator.StringToHash("AboutExit");

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        animator.Play(hashEnterPara);
    }

    public void OnCloseButtonClicked()
    {
        GameManager.Instance.audioManager.PlayButtonAudioClip();
        animator.Play(hashExitPara);
    }

    public void DisablePanel()
    {
        this.gameObject.SetActive(false);
    }
}
