using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DG.Tweening;


public class SlideText : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Slide(float distance, float duration)
    {
        float xdegree = transform.localEulerAngles.x;
        float dirz = distance * (float)System.Math.Tan(System.Math.PI * xdegree / 180);
        Vector3 dir = new Vector3(0.0f, distance, dirz);
        DOTween.Sequence()
            .AppendInterval(4.0f)
            .Append(transform.DOMove(dir, duration).SetRelative(true).SetEase(Ease.Linear))
            .Play();
    }
}
