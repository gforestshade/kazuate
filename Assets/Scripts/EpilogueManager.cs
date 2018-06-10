using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;
using DG.Tweening;

public class EpilogueManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
    }

    // Update is called once per frame
    void Update () {
		
	}

    public void ShowStaffRoll()
    {
        GameObject staffRoll = GameObject.Find("/Canvas/StaffRoll");
        CanvasGroup cgGranma = GameObject.Find("/Canvas/GranmaGroup").GetComponent<CanvasGroup>();
        CanvasGroup cgBoy = GameObject.Find("/Canvas/BoyGroup").GetComponent<CanvasGroup>();
        CanvasGroup cgEndImage = GameObject.Find("/Canvas/EndImage").GetComponent<CanvasGroup>();
        TextMeshProUGUI guide = GameObject.Find("/Canvas/GuideText").GetComponent<TextMeshProUGUI>();

        float staffRollHeight = staffRoll.GetComponent<RectTransform>().sizeDelta.y;

        DOTween.Sequence()
            .Append(cgGranma.DOFade(0.0f, 1.0f))
            .Join(cgBoy.DOFade(0.0f, 1.0f))
            .Append(staffRoll.transform.DOLocalMoveY(staffRollHeight+550, 60.0f)
                             .SetRelative(true).SetEase(Ease.Linear))
            .Append(cgEndImage.DOFade(1.0f, 6.0f))
            .AppendInterval(2.0f)
            .Append(guide.DOFade(1.0f, 1.0f))
            .Play();
    }
}
