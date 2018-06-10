using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DG.Tweening;

public class GoodEndManager : MonoBehaviour
{
    private string[] emphasizeList = new string[] { "char name[20];", "gets(name);", "scanf(\"%d\", &kotae);" }; 

	// Use this for initialization
	void Start ()
    {
        GameObject code = GameObject.Find("/SourceCode");
        GameObject guideText = GameObject.Find("/Canvas/GuideText");

        string source = Resources.Load<TextAsset>("source").text;
        float slideDistance = 9.5f;
        float slideDuration = 60.0f;

        if (FlagManager.getSeeScene("good") >= 1)
        {
            foreach(string e in emphasizeList)
            {
                source = source.Replace(e, "<color=#ff7c5e>" + e + "</color>");
            }
            slideDistance = 7.0f;
            slideDuration = 10.0f;
        }
        code.GetComponent<TextMesh>().text = source;

        StartCoroutine(DelayMethod(0.8f, () =>
        {
            SlideText st = code.GetComponent<SlideText>();
            st.Slide(slideDistance, slideDuration);
        }));

        StartCoroutine(DelayMethod(slideDuration + 4.0f, () =>
        {
            BlinkTextMesh btm = guideText.GetComponent<BlinkTextMesh>();
            btm.Blink();
        }));

    }

    // Update is called once per frame
    void Update () {
		
	}

    /// <summary>
    /// 渡された処理を指定時間後に実行する
    /// </summary>
    /// <param name="waitTime">遅延時間[ミリ秒]</param>
    /// <param name="action">実行したい処理</param>
    /// <returns></returns>
    private IEnumerator DelayMethod(float waitTime, System.Action action)
    {
        yield return new WaitForSeconds(waitTime);
        action();
    }
}
