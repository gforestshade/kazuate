using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class MainText : MonoBehaviour {

    public GameObject scrollView;
    public GameObject gameManager;

    string showingMessage = "";
    int showedPos;

    Text t;

    public float textTimeOut;
    private float timeElapsed;

    public GameObject audioSource;

    public string ShowingMessage
    {
        get
        {
            return showingMessage;
        }

        private set
        {
            showingMessage = value;
        }
    }

    // Use this for initialization
    void Start ()
    {
        t = GetComponent<Text>();
        timeElapsed = .0f;
    }
	
	// Update is called once per frame
	void Update ()
    {
        timeElapsed += Time.deltaTime;
        if( timeElapsed >= textTimeOut && showedPos < ShowingMessage.Length )
        {
            showedPos++;
            t.text = ShowingMessage.Substring(0, showedPos);
            timeElapsed = .0f;

            gameObject.GetComponent<ContentSizeFitter>().SetLayoutVertical();
            scrollView.GetComponent<ScrollRect>().verticalNormalizedPosition = 0.0f;

            if (showedPos == ShowingMessage.Length)
            {
                audioSource.GetComponent<AudioSource>().Stop();
                gameManager.GetComponent<GameManager>().onEndShowingText();
            }
        }
    }

    public void AddMessage(string message)
    {
        ShowingMessage += message;
        audioSource.GetComponent<AudioSource>().Play();
    }

}
