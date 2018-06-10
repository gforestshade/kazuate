using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DG.Tweening;
using TMPro;

public class BlinkTextMesh : MonoBehaviour
{
    public float fadeDalay;
    public float fadeDuration;

	// Use this for initialization
	void Start ()
    {
        GetComponent<TextMeshProUGUI>().DOFade(0.0f, 0.0f);
    }

    // Update is called once per frame
    void Update () {
		
	}

    public void Blink()
    {
        GetComponent<TextMeshProUGUI>()
            .DOFade(1.0f, fadeDuration)
            .SetEase(Ease.OutSine)
            .SetLoops(-1, LoopType.Yoyo)
            .Play();
    }
}
