using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;

public class TitleManager : MonoBehaviour
{
    private GameObject buttonStory;
    private GameObject buttonStart;
    private GameObject buttonPrologue;
    private GameObject buttonGoodEnd;
    private GameObject buttonNormalEnd;
    private GameObject buttonBadEnd1;
    private GameObject buttonBadEnd2;
    private GameObject buttonBadEnd3;
    private GameObject buttonTrueEnd;
    private GameObject startButtons;
    private GameObject storyButtons;

	// Use this for initialization
	void Start ()
    {
        buttonStory = GameObject.Find("/Canvas/StartButtons/ButtonStory");
        buttonStart = GameObject.Find("/Canvas/StartButtons/ButtonStart");
        buttonPrologue = GameObject.Find("/Canvas/StoryButtons/ButtonPrologue");
        buttonGoodEnd = GameObject.Find("/Canvas/StoryButtons/ButtonGoodEnd");
        buttonNormalEnd = GameObject.Find("/Canvas/StoryButtons/ButtonNormalEnd");
        buttonBadEnd1 = GameObject.Find("/Canvas/StoryButtons/ButtonBadEnd1");
        buttonBadEnd2 = GameObject.Find("/Canvas/StoryButtons/ButtonBadEnd2");
        buttonBadEnd3 = GameObject.Find("/Canvas/StoryButtons/ButtonBadEnd3");
        buttonTrueEnd = GameObject.Find("/Canvas/StoryButtons/ButtonTrueEnd");
        startButtons = GameObject.Find("/Canvas/StartButtons");
        storyButtons = GameObject.Find("/Canvas/StoryButtons");

        storyButtons.GetComponent<CanvasGroup>().alpha = 0.0f;
        buttonStart.GetComponent<Button>().Select();

        bool trueEndFlag = true;

        if (FlagManager.getSeeScene("good") == 0)
        {
            buttonGoodEnd.GetComponent<Button>().interactable = false;
            trueEndFlag = false;
        }
        if (FlagManager.getSeeScene("normal") == 0)
        {
            buttonNormalEnd.GetComponent<Button>().interactable = false;
            trueEndFlag = false;
        }
        if (FlagManager.getSeeScene("format") == 0)
        {
            buttonBadEnd1.GetComponent<Button>().interactable = false;
            trueEndFlag = false;
        }
        if (FlagManager.getSeeScene("overflow") == 0)
        {
            buttonBadEnd2.GetComponent<Button>().interactable = false;
            trueEndFlag = false;
        }
        if (FlagManager.getSeeScene("overrun") == 0)
        {
            buttonBadEnd3.GetComponent<Button>().interactable = false;
            trueEndFlag = false;
        }

        if (trueEndFlag)
        {
            buttonTrueEnd.GetComponent<Button>().interactable = true;
            buttonTrueEnd.GetComponentInChildren<Text>().text = "Epilogue";
        }
    }

    // Update is called once per frame
    void Update () {
		
	}

    public void GameStart()
    {
        SceneManager.LoadScene("scene1");
    }

    public void ToggleStoryButtons()
    {
        var cg = storyButtons.GetComponent<CanvasGroup>();
        if (cg.interactable)
        {
            cg.alpha = 0.0f;
            cg.blocksRaycasts = false;
            cg.interactable = false;
            buttonStart.GetComponent<Button>().interactable = true;
        }
        else
        {
            cg.alpha = 1.0f;
            cg.blocksRaycasts = true;
            cg.interactable = true;

            Vector3 dir = storyButtons.transform.position;
            Vector3 pos = dir + Vector3.right * 0.5f;

            DOTween.Sequence()
                .Join(storyButtons.transform.DOMove(pos, 0.4f).From())
                .Join(storyButtons.GetComponent<CanvasGroup>().DOFade(0.0f, 0.4f).From())
                .Play();

            buttonStart.GetComponent<Button>().interactable = false;
            //buttonPrologue.GetComponent<Button>().Select();
        }
    }

    public void ShowPrologue()
    {
        FlagManager.sceneNo = 4;
        SceneManager.LoadScene("normalend");
    }

    public void ShowGoodEnd()
    {
        SceneManager.LoadScene("goodend");
    }

    public void ShowNormalEnd()
    {
        FlagManager.sceneNo = 5;
        SceneManager.LoadScene("normalend");
    }

    public void ShowBadEnd1()
    {
        FlagManager.sceneNo = 1;
        SceneManager.LoadScene("badend");
    }

    public void ShowBadEnd2()
    {
        FlagManager.sceneNo = 2;
        SceneManager.LoadScene("badend");
    }

    public void ShowBadEnd3()
    {
        FlagManager.sceneNo = 3;
        SceneManager.LoadScene("badend");
    }

    public void ShowTrueEnd()
    {
        SceneManager.LoadScene("epilogue");
    }
}
