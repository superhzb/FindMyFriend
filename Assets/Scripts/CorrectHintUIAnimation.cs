using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CorrectHintUIAnimation : MonoBehaviour
{

    public Sprite[] sprites;

    private Image image;
    private Tween enterTween;

    private void Awake()
    {
        image = GetComponent<Image>();

        enterTween = transform.DOLocalMoveY(150f, 0.6f);
            
        enterTween.SetAutoKill(false);
        enterTween.Pause();
    }

    private void OnEnable()
    {
        image.sprite = sprites[Random.Range(0, sprites.Length)];

        enterTween.Restart();
        enterTween.OnComplete(
            () => this.gameObject.SetActive(false)
            );
    }
}
