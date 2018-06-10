using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;
using DG.Tweening;
using UnityEngine.Events;

[System.Serializable]
public class Message
{
    public string chara;
    public string text;
    public float interval;
}

[System.Serializable]
public class MessageArray
{
    public float fadeInDuration;
    public float fadeOutDuration;
    public Message[] messages;
}

public class TextManager : MonoBehaviour
{
    public TextAsset textAsset;
    public UnityEvent onComplete;

	// Use this for initialization
	void Start ()
    {
        GameObject granmaText = GameObject.Find("/Canvas/GranmaGroup/GranmaText");
        GameObject boyText = GameObject.Find("/Canvas/BoyGroup/BoyText");
        GameObject guideText = GameObject.Find("/Canvas/GuideText");

        var gt = granmaText.GetComponent<TextMeshProUGUI>();
        var bt = boyText.GetComponent<TextMeshProUGUI>();
        var guide = guideText.GetComponent<BlinkTextMesh>();

        Color c1 = gt.color;
        Color c0 = new Color(c1.r, c1.g, c1.b, 0.0f);

        bt.color = c0;
        gt.color = c0;

        var json = JsonUtility.FromJson<MessageArray>(textAsset.text);

        DOTween.Init();
        DOTween.defaultAutoPlay = AutoPlay.AutoPlayTweeners;

        var seq = DOTween.Sequence();
        foreach (Message msg in json.messages)
        {
            TextMeshProUGUI tm = null;
            if (msg.chara == "granma")
            {
                tm = granmaText.GetComponent<TextMeshProUGUI>();
            }
            else if (msg.chara == "boy")
            {
                tm = boyText.GetComponent<TextMeshProUGUI>();
            }

            seq.Append(tm.DOFade(0.0f, json.fadeOutDuration));
            seq.AppendCallback(() => {
                tm.text = msg.text;
            });
            seq.Append(tm.DOFade(1.0f, json.fadeInDuration));
            seq.AppendInterval(msg.interval);
        }
        seq.OnComplete(() => {
            onComplete.Invoke();
       });
        seq.Play();
    }

// Update is called once per frame
void Update () {
		
	}
}
