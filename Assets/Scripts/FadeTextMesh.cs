using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DG.Tweening;
using TMPro;

public class FadeTextMesh : MonoBehaviour
{
    public float fadeInDalay;
    public float fadeDuration;
    public float fadeOutDalay;

	// Use this for initialization
	void Start ()
    {
        var o = GetComponent<TextMeshProUGUI>();
        DOTween.Sequence()
            .Append(o.DOFade(0.0f, 0.0f))
            .AppendInterval(fadeInDalay)
            .Append(o.DOFade(1.0f, fadeDuration))
            .AppendInterval(fadeOutDalay)
            .Append(o.DOFade(0.0f, fadeDuration))
            ;
	}

    // Update is called once per frame
    void Update () {
		
	}
}
