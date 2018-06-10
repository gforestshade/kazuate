using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class AnyKeyToExit : MonoBehaviour
{
    public string sceneStr = "";

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.anyKeyDown)
        {
            if (sceneStr != "")
            {
                FlagManager.addSeeScene(sceneStr);
            }
            SceneManager.LoadScene("title");
        }
	}
}
