using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using DG.Tweening;

using TMPro;

public class FadeText : MonoBehaviour
{
    private float fadeDuration = 0.3f;
    private float moveDuration = 0.9f;

    private Vector3 origPos;

	// Use this for initialization
	void Start ()
    {
        origPos = transform.position;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Jiwa(string s)
    {
        var text = GetComponent<Text>();
        Color ncolor = text.color;
        ncolor.a = .0f;
        text.color = ncolor;
        text.text = s;
        transform.position = origPos;

        var seq = DOTween.Sequence()
            .Insert(0.0f,
                DOTween.ToAlpha(
                () => text.color,
                color => text.color = color,
                1.0f, fadeDuration))
            .Insert(moveDuration - fadeDuration, 
                DOTween.ToAlpha(
                () => text.color,
                color => text.color = color,
                0.0f, fadeDuration))
            .Insert(0.0f, 
                transform.DOMove(Vector3.up*0.5f, moveDuration)
                .SetRelative(true)
                .SetEase(Ease.InOutCubic))
            ;

        seq.Play();
    }
}
