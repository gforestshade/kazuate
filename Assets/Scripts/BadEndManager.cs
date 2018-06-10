using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;

public class BadEndManager : MonoBehaviour
{

    private GameObject granmaGroup;
    private GameObject granmaImage;
    private GameObject granmaText;
    private GameObject boyGroup;
    private GameObject boyImage;
    private GameObject boyText;
    private GameObject black;

    public Sprite boy2;

	// Use this for initialization
	void Start ()
    {
        granmaGroup = GameObject.Find("/Canvas/GranmaGroup");
        granmaImage = GameObject.Find("/Canvas/GranmaGroup/Granma");
        granmaText = GameObject.Find("/Canvas/GranmaGroup/GranmaText");
        boyGroup = GameObject.Find("/Canvas/BoyGroup");
        boyImage = GameObject.Find("/Canvas/BoyGroup/Boy");
        boyText = GameObject.Find("/Canvas/BoyGroup/BoyText");
        black = GameObject.Find("/Canvas/Black");

        int end = FlagManager.sceneNo;

        if (end == 1)
        {
            DoBadEndFormat();
        }
        else if (end == 2)
        {
            DoBadEndOverflow();
        }
        else if (end == 3)
        {
            DoBadEndOverrun();
        }
	}

    private void DoBadEndFormat()
    {

        string granma_text_1 = @"たかしや...
scanf()のd変換指定子は、数字でない文字に出会ったら
それ以上標準入力から読むことをやめて制御を返すの。
その状態でもう一度scanf()を呼び出しても、
さっきの状態からポインタは動いていないから、
結局また数字でない文字に出会うことになるわ。
標準入力が残っとるから、新しい入力もできない。
そうして、ゲームが崩壊したのね。";
        string granma_text_2 = @"<size=32>ぼうや、
これが本当のプログラミングさ</size>";
        string boy_text_1 = "えっ...なにいまの...";
        string boy_text_2 = "えっえっ";
        string boy_text_3 = "<size=32>そんなぁー</size>";

        Image blackImage = black.GetComponent<Image>();


        granmaGroup.transform.localScale = Vector3.zero;
        boyGroup.transform.localScale = Vector3.zero;

        DOTween.Sequence()
            .Append(boyGroup.transform.DOScale(1.0f, 1.5f).SetEase(Ease.InExpo))
            .Append(boyText.GetComponent<Text>().DOText(boy_text_1, boy_text_1.Length * 0.25f).SetEase(Ease.Linear))
            .AppendInterval(1.6f)
            .Append(granmaGroup.transform.DOScale(1.0f, 1.5f).SetEase(Ease.OutBounce))
            .Append(granmaText.GetComponent<Text>().DOText(granma_text_1, granma_text_1.Length * 0.02f).SetEase(Ease.Linear))
            .AppendInterval(3.0f)
            .AppendCallback(() => { boyText.GetComponent<Text>().text = ""; })
            .AppendInterval(0.6f)
            .Append(boyText.GetComponent<Text>().DOText(boy_text_2, boy_text_2.Length * 0.25f).SetEase(Ease.Linear))
            .AppendInterval(4.0f)
            .AppendCallback(() =>
            {
                granmaText.GetComponent<Text>().text = "";
                granmaImage.transform.DOScale(3.0f, 25.0f);
            })
            .AppendInterval(0.6f)
            .Append(granmaText.GetComponent<Text>().DOText(granma_text_2, granma_text_2.Length * 0.20f).SetEase(Ease.Linear))
            .AppendInterval(1.0f)
            .AppendCallback(() =>
            {
                boyText.GetComponent<Text>().text = "";
                boyImage.GetComponent<Image>().sprite = boy2;
            })
            .AppendInterval(0.6f)
            .Append(boyText.GetComponent<Text>().DOText(boy_text_3, boy_text_3.Length * 0.12f).SetEase(Ease.Linear))
            .Append(DOTween.ToAlpha(() => blackImage.color, color => blackImage.color = color, 1.0f, 3.0f))
            .AppendInterval(1.5f)
            .OnComplete(() =>
            {
                FlagManager.addSeeScene("format");
                SceneManager.LoadScene("title");
            })
            .Play();
            ;
    }

    private void DoBadEndOverflow()
    {
        string granma_text_1 = @"たかしや...
ひとつの変数に収められる数値には限界があるの。
たかしの処理系ではintは32bitじゃから、
2147483647が上限になるわね。
それを超えると、オーバーフローという現象が起こるの。
ぐるっと回って、全く違う値になってしまうのよ。
たかしのプログラムでは入力値を答えと比較しているだけだから
大きなバグにはなっていないけれどねぇ、
掛け算の結果が意図せずオーバーフローして、
計算結果を壊してしまうことはよくあるんだよ。";
        string granma_text_2 = @"<size=32>ぼうや、
これが本当のプログラミングさ</size>";
        string boy_text_1 = "えっ...なに...あんなに大きな数字...?";
        string boy_text_2 = "えっえっ";
        string boy_text_3 = "<size=32>そんなぁー</size>";

        Image blackImage = black.GetComponent<Image>();


        granmaGroup.transform.localScale = Vector3.zero;
        boyGroup.transform.localScale = Vector3.zero;


        DOTween.Sequence()
            .Append(boyGroup.transform.DOScale(1.0f, 1.5f).SetEase(Ease.InExpo))
            .Append(boyText.GetComponent<Text>().DOText(boy_text_1, boy_text_1.Length * 0.10f).SetEase(Ease.Linear))
            .AppendInterval(1.6f)
            .Append(granmaGroup.transform.DOScale(1.0f, 1.5f).SetEase(Ease.OutBounce))
            .Append(granmaText.GetComponent<Text>().DOText(granma_text_1, granma_text_1.Length * 0.02f).SetEase(Ease.Linear))
            .AppendInterval(3.0f)
            .AppendCallback(() => { boyText.GetComponent<Text>().text = ""; })
            .AppendInterval(0.6f)
            .Append(boyText.GetComponent<Text>().DOText(boy_text_2, boy_text_2.Length * 0.25f).SetEase(Ease.Linear))
            .AppendInterval(4.0f)
            .AppendCallback(() => {
                granmaText.GetComponent<Text>().text = "";
                granmaImage.transform.DOScale(3.0f, 25.0f);
            })
            .AppendInterval(0.6f)
            .Append(granmaText.GetComponent<Text>().DOText(granma_text_2, granma_text_2.Length * 0.20f).SetEase(Ease.Linear))
            .AppendInterval(1.0f)
            .AppendCallback(() => {
                boyText.GetComponent<Text>().text = "";
                boyImage.GetComponent<Image>().sprite = boy2;
            })
            .AppendInterval(0.6f)
            .Append(boyText.GetComponent<Text>().DOText(boy_text_3, boy_text_3.Length * 0.12f).SetEase(Ease.Linear))
            .Append(DOTween.ToAlpha(() => blackImage.color, color => blackImage.color = color, 1.0f, 3.0f))
            .AppendInterval(1.5f)
            .OnComplete(() => {
                FlagManager.addSeeScene("overflow");
                SceneManager.LoadScene("title");
            })
            .Play();
        ;

    }
    
    private void DoBadEndOverrun()
    {
        string granma_text_1 = @"たかしや...
あんたは固定長でchar配列を宣言したね。
20文字もあれば十分だと思ったのかもしれないけれど、
もしそれを超える入力が来たらどうなると思うかい...？
用意されている境界を越えて、そのまま書き込まれてしまうの。
この過程で「幸運にも」触れてはいけないはずの領域に触ったから、
Segmentation Faultで強制終了されたのね。
ほっほ、スタックを破壊してリターンアドレスを書き換えられれば
任意コード実行の可能性まであるからねぇ。";
        string granma_text_2 = @"<size=32>ぼうや、
これが本当のプログラミングさ</size>";
        string boy_text_1 = "えっ...なにいまの...";
        string boy_text_2 = "えっえっ";
        string boy_text_3 = "<size=32>そんなぁー</size>";

        Image blackImage = black.GetComponent<Image>();


        granmaGroup.transform.localScale = Vector3.zero;
        boyGroup.transform.localScale = Vector3.zero;

        DOTween.Sequence()
            .Append(boyGroup.transform.DOScale(1.0f, 1.5f).SetEase(Ease.InExpo))
            .Append(boyText.GetComponent<Text>().DOText(boy_text_1, boy_text_1.Length * 0.10f).SetEase(Ease.Linear))
            .AppendInterval(1.6f)
            .Append(granmaGroup.transform.DOScale(1.0f, 1.5f).SetEase(Ease.OutBounce))
            .Append(granmaText.GetComponent<Text>().DOText(granma_text_1, granma_text_1.Length * 0.02f).SetEase(Ease.Linear))
            .AppendInterval(3.0f)
            .AppendCallback(() => { boyText.GetComponent<Text>().text = ""; })
            .AppendInterval(0.6f)
            .Append(boyText.GetComponent<Text>().DOText(boy_text_2, boy_text_2.Length * 0.25f).SetEase(Ease.Linear))
            .AppendInterval(4.0f)
            .AppendCallback(() => {
                granmaText.GetComponent<Text>().text = "";
                granmaImage.transform.DOScale(3.0f, 25.0f);
            })
            .AppendInterval(0.6f)
            .Append(granmaText.GetComponent<Text>().DOText(granma_text_2, granma_text_2.Length * 0.20f).SetEase(Ease.Linear))
            .AppendInterval(1.0f)
            .AppendCallback(() => {
                boyText.GetComponent<Text>().text = "";
                boyImage.GetComponent<Image>().sprite = boy2;
            })
            .AppendInterval(0.6f)
            .Append(boyText.GetComponent<Text>().DOText(boy_text_3, boy_text_3.Length * 0.12f).SetEase(Ease.Linear))
            .Append(DOTween.ToAlpha(() => blackImage.color, color => blackImage.color = color, 1.0f, 3.0f))
            .AppendInterval(1.5f)
            .OnComplete(() => {
                FlagManager.addSeeScene("overrun");
                SceneManager.LoadScene("title");
            })
            .Play();
        ;

    }

    // Update is called once per frame
    void Update () {
	}
}
