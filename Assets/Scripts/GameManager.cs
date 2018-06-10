using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

using UnityEngine.SceneManagement;
using UnityEngine.UI;

using System.Text.RegularExpressions;

public class GameManager : MonoBehaviour
{
    private int goal;
    private int n;

    public GameObject mainText;
    public GameObject mainInputField;
    public GameObject recognizedText;

    public GameObject bgmSource;
    public GameObject audioSource;
    public AudioClip badTone;

    enum JudgeResult
    {
        ONEMORE,
        SUCCESS,
        FAILIRE,
    }

    enum Progress
    {
        NAME,
        NUMBER,
        GOODEND,
        NORMALEND,
        BADEND_OVERRUN,
        BADEND_FORMAT,
        BADEND_OVERFLOW,
    }

    Progress progress;

	// Use this for initialization
	void Start ()
    {
        n = 7;
        Roll(100);
        progress = Progress.NAME;

        mainText.GetComponent<MainText>().AddMessage("あなたの名前を入力してください。\n");
	}
	
	// Update is called once per frame
	void Update ()
    {
    }

    void Roll(int max)
    {
        goal = Random.Range(0, max-1);
    }

    JudgeResult Judge(int i)
    {
        if (goal == i)
        {
            mainText.GetComponent<MainText>().AddMessage("あたり！");
            return JudgeResult.SUCCESS;
        }
        else if (--n > 0)
        {
            if (goal < i)
            {
                mainText.GetComponent<MainText>().AddMessage("もっと小さいです\nわたしが思い浮かべた数字を入力してください。(残り" + n + "回)\n");
                return JudgeResult.ONEMORE;
            }
            else
            {
                mainText.GetComponent<MainText>().AddMessage("もっと大きいです\nわたしが思い浮かべた数字を入力してください。(残り" + n + "回)\n");
                return JudgeResult.ONEMORE;
            }
        }
        else
        {
            mainText.GetComponent<MainText>().AddMessage("時間切れになりました。残念です。\n");
            return JudgeResult.FAILIRE;
        }
    }

    /// <summary>
    /// 本体 
    /// 入力が確定されたとき
    /// </summary>
    /// <param name="s"></param>
    public void onEndEdit(string s)
    {
        // 空白のみなら入力を差し戻す
        if (Regex.IsMatch(s, @"^\s*$"))
        {
            mainInputField.GetComponent<InputField>().text = "";
            mainInputField.GetComponent<InputField>().ActivateInputField();
            return;
        }

        // 入力内容を画面に映す
        MainText mt = mainText.GetComponent<MainText>();
        mt.AddMessage(s + "\n");
        // InputFieldクリア
        mainInputField.GetComponent<InputField>().text = "";


        switch (progress)
        {
            case Progress.NAME:
                ParseInputName(s);
                break;
            case Progress.NUMBER:
                ParseInputNumber(s);
                break;
        }
    }

    private void ParseInputName(string s)
    {
        //完成品ではエンコード周りがうまく動かないので、妥協
        //var enc = System.Text.Encoding.UTF8;
        //if (enc.GetByteCount(s) > 20)
        if (s.Length > 20)
        {
            DoError(Progress.BADEND_OVERRUN, "Overrun!");
            mainText.GetComponent<MainText>().AddMessage("Segmentation Fault");
            return;
        }

        string fmt = "こんにちは、あなたは{0}さんですね。\n" +
            "いまからわたしが0から99までの数をひとつ思い浮かべます。\n" +
            "わたしが思い浮かべた数字を入力してください。(残り{1}回)\n";

        mainText.GetComponent<MainText>().AddMessage( string.Format(fmt, s, n) );
        progress = Progress.NUMBER;
    }

    private void ParseInputNumber(string s)
    {
        Regex r = new Regex(@"^\s*[+-]?\d+");
        Match m = r.Match(s, 0);

        bool cinLeft = false;

        if (m.Length < s.Length)
        {
            if (System.Char.IsWhiteSpace(s[m.Length]))
            {
                cinLeft = true;
            }
            else
            {
                DoError(Progress.BADEND_FORMAT, "Format Error!");

                int answer;
                System.Int32.TryParse(m.Value, out answer);

                while (Judge(answer) == 0) ;
                return;
            }
        }

        try
        {
            int answer = System.Int32.Parse(m.Value);
            JudgeResult j = Judge(answer);
            if (j == JudgeResult.SUCCESS)
            {
                progress = Progress.GOODEND;
                StartCoroutine(DelayMethod(1.5f, () =>
                {
                    SceneManager.LoadScene("goodend");
                }));
            }
            else if (j == JudgeResult.FAILIRE)
            {
                if (progress < Progress.NORMALEND)
                {
                    progress = Progress.NORMALEND;
                }
            }
            recognizedText.GetComponent<FadeText>().Jiwa(m.Value);
        }
        catch (System.OverflowException)
        {
            DoError(Progress.BADEND_OVERFLOW, "Overflow!");
            Judge(-1);
        }

        if (progress < Progress.GOODEND && cinLeft)
        {
            ParseInputNumber(s.Substring(m.Length));
        }
    }

    private void DoError(Progress p, string msg)
    {
        recognizedText.GetComponent<Text>().color = Color.red;
        recognizedText.GetComponent<FadeText>().Jiwa(msg);
        audioSource.GetComponent<AudioSource>().PlayOneShot(badTone);
        bgmSource.GetComponent<AudioSource>().Stop();
        progress = p;
    }

    public void onEndShowingText()
    {
        if (progress < Progress.GOODEND)
        {
            mainInputField.GetComponent<InputField>().ActivateInputField();
        }
        else if (progress == Progress.NORMALEND)
        {
            StartCoroutine(DelayMethod(1.7f, () =>
            {
                FlagManager.sceneNo = 5;
                SceneManager.LoadScene("normalend");
            }));
        }
        else if (progress == Progress.BADEND_FORMAT)
        {
            StartCoroutine(DelayMethod(1.0f, () =>
            {
                FlagManager.sceneNo = 1;
                SceneManager.LoadScene("badend");
            }));
        }
        else if (progress == Progress.BADEND_OVERFLOW)
        {
            StartCoroutine(DelayMethod(1.0f, () =>
            {
                FlagManager.sceneNo = 2;
                SceneManager.LoadScene("badend");
            }));
        }
        else if (progress == Progress.BADEND_OVERRUN)
        {
            StartCoroutine(DelayMethod(3.0f, () =>
            {
                FlagManager.sceneNo = 3;
                SceneManager.LoadScene("badend");
            }));
        }
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
