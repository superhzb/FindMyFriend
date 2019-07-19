using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPC : MonoBehaviour
{
    public RawImage bodyImage;
    public RawImage faceImage;
    public RawImage hairImage;
    public RawImage kitImage;
    public bool isTarget = false;

    private Animator animator;

    public GameObject[] dialogs = new GameObject[2]; //0 LeftDialog, 1 RightDialog
    private bool isLeftDialog = false;

    private readonly int hashCorrectPara = Animator.StringToHash("Correct");
    private readonly int hashIncorrectPara = Animator.StringToHash("Incorrect");

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void InitNPC(Texture body, Texture face, Texture hair, Texture kit)
    {
        isTarget = false;

        bodyImage.texture = body;
        faceImage.texture = face;
        hairImage.texture = hair;
        kitImage.texture = kit;
    }

    private void OnEnable()
    {
        float startTime = Random.Range(2f, 8f);
        float repeatRate = Random.Range(5f, 12f);

        InvokeRepeating("NextDialog", startTime, repeatRate);
    }

    private void OnDisable()
    {
        CancelInvoke();
    }

    public void NextDialog()
    {
        bool isShowDialog = Random.Range(0, 100) > 60 ? true : false;

        if (isShowDialog)
        {
            if (isLeftDialog)
            {
                dialogs[0].SetActive(true);
                dialogs[0].GetComponent<Animator>().Play(0);
                dialogs[1].SetActive(false);
            }
            else
            {
                dialogs[1].SetActive(true);
                dialogs[1].GetComponent<Animator>().Play(0);
                dialogs[0].SetActive(false);
            }

            isLeftDialog = !isLeftDialog;
        }
    }

    public void SetAsTarget()
    {
        isTarget = true;
    }

    public void OnNPCClicked()
    {
        if (isTarget)
        {
            GameManager.Instance.audioManager.PlayCorrectClip();
            GameManager.Instance.TriggerCorrectHint();

            CancelInvoke();
            dialogs[0].SetActive(false);
            dialogs[1].SetActive(false);

            animator.SetTrigger(hashCorrectPara);
        }
        else
        {
            GameManager.Instance.audioManager.PlayIncorrectClip();
            animator.SetTrigger(hashIncorrectPara);
        }
    }

    public void OnCorrectAnimationFinished()
    {
        NPCPool.Instance.ReturnToPool(this);
        GameManager.Instance.SetNextTargetNPC(this);
    }
}
