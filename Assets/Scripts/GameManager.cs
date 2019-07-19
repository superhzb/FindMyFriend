using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Canvas canvas;
    public NPC targetNPC;
    public AudioManager audioManager;

    private MiniTaskPanel miniTaskPanel;
    private AboutPanel aboutPanel;
    private Button btn_About;
    private GameObject titlePage;
    private CanvasGroup faderCanvasGroup;
    private Transform levelHolder;
    private Image correctHintImage;

    private Level currentLevel;
    private float fadeDuration = 1f;
    private float waitTime = 2f;
    private bool isFading;

    private static GameManager _instance;
    public static GameManager Instance{ get { return _instance; } }

    private void Awake()
    {
        _instance = this;

        miniTaskPanel = canvas.transform.Find("MiniTaskPanel").GetComponent<MiniTaskPanel>();
        aboutPanel = canvas.transform.Find("AboutPanel").GetComponent<AboutPanel>();
        btn_About = canvas.transform.Find("Btn_About").GetComponent<Button>();
        titlePage = canvas.transform.Find("Title").gameObject;
        faderCanvasGroup = canvas.transform.Find("FadeImage").GetComponent<CanvasGroup>();
        levelHolder = canvas.transform.Find("LevelHolder");
        correctHintImage = canvas.transform.Find("CorrectHint").GetComponent<Image>();
}

    private IEnumerator Start()
    {
        faderCanvasGroup.alpha = 1f;

        yield return StartCoroutine(Fade(0f));
        yield return StartCoroutine(Wait(waitTime));
        yield return StartCoroutine(FadeAndShowTitlePage());
        yield return StartCoroutine(Wait(waitTime));
        yield return StartCoroutine(LoadFirstLevel());

        audioManager.PlayBGMusic();
    }

    private IEnumerator Wait(float time)
    {
        yield return new WaitForSeconds(time);
    }

    private IEnumerator FadeAndShowTitlePage()
    {
        yield return StartCoroutine(Fade(1f));
        titlePage.SetActive(true);
        yield return StartCoroutine(Fade(0f));
    }

    private IEnumerator Fade(float finalAlpha)
    {
        isFading = true;
        faderCanvasGroup.blocksRaycasts = true;

        float fadeSpeed = Mathf.Abs(faderCanvasGroup.alpha - finalAlpha) / fadeDuration;

        while (!Mathf.Approximately(faderCanvasGroup.alpha, finalAlpha))
        {
            faderCanvasGroup.alpha = Mathf.MoveTowards(faderCanvasGroup.alpha, finalAlpha,
                fadeSpeed * Time.deltaTime);

            yield return null;
        }

        isFading = false;
        faderCanvasGroup.blocksRaycasts = false;
    }

    public void SetFirstTargetNPC()
    {
        targetNPC = currentLevel.PickATargetNPC();
        if (targetNPC != null)
        {
            miniTaskPanel.SetNPCSprites(targetNPC);
            miniTaskPanel.ShowMiniTaskPanel();
        }
    }

    public void SetNextTargetNPC(NPC lastNPC)
    {
        targetNPC = currentLevel.PickATargetNPC(lastNPC);
        if (targetNPC != null)
        {
            miniTaskPanel.LoadNewNPC();
        }
        else
        {
            StartCoroutine(LoadNextLevel(currentLevel));
        }
    }

    private IEnumerator LoadFirstLevel()
    {
        yield return StartCoroutine(Fade(1f));

        Level level = LevelPool.Instance.Get();
        level.transform.SetParent(levelHolder);
        level.transform.localPosition = levelHolder.localPosition;
        currentLevel = level;
        level.gameObject.SetActive(true);
        btn_About.gameObject.SetActive(true);

        yield return StartCoroutine(Fade(0f));
    }

    private IEnumerator LoadNextLevel(Level lastLevel)
    {
        miniTaskPanel.HideMiniTaskPanel();

        yield return StartCoroutine(Fade(1f));
        
        LevelPool.Instance.ReturnToPool(lastLevel);
        Level level = LevelPool.Instance.Get();
        level.transform.SetParent(levelHolder);
        level.transform.localPosition = levelHolder.localPosition;
        currentLevel = level;
        level.gameObject.SetActive(true);

        yield return StartCoroutine(Fade(0f));
    }

    public void OnAboutButtonClicked()
    {
        audioManager.PlayButtonAudioClip();
        aboutPanel.gameObject.SetActive(true);
    }

    public void TriggerCorrectHint()
    {
        correctHintImage.gameObject.SetActive(true);
    }
}
