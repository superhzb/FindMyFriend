using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class MiniTaskPanel : MonoBehaviour
{
    public RawImage bodyImage;
    public RawImage faceImage;
    public RawImage hairImage;
    public RawImage kitImage;

    private Tween panelTweenDown;
    private Tween panelTweenUp;

    void Start()
    {
        panelTweenDown = transform.DOLocalMoveY(180f, 0.5f);
        panelTweenDown.SetAutoKill(false);
        panelTweenDown.Pause();

        panelTweenUp = transform.DOLocalMoveY(550f, 0.5f);
        panelTweenUp.SetAutoKill(false);
        panelTweenUp.Pause();
    }

    public void LoadNewNPC()
    {
        panelTweenUp.Restart();
        panelTweenUp.OnComplete(
            () => 
            {
                if (GameManager.Instance.targetNPC != null)
                {
                    SetNPCSprites(GameManager.Instance.targetNPC);
                    panelTweenDown.Restart();
                }
            });
    }

    public void SetNPCSprites(NPC npc)
    {
        bodyImage.texture = npc.bodyImage.texture;
        faceImage.texture = npc.faceImage.texture;
        hairImage.texture = npc.hairImage.texture;
        kitImage.texture = npc.kitImage.texture;
    }

    public void ShowMiniTaskPanel()
    {
        panelTweenDown.Restart();
    }
    
    public void HideMiniTaskPanel()
    {
        panelTweenUp.Restart();
        
    }

}
