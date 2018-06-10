using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalEndManager : MonoBehaviour {

    void Awake()
    {
        int sno = FlagManager.sceneNo;
        TextManager tm = GetComponent<TextManager>();
        AnyKeyToExit ae = GetComponent<AnyKeyToExit>();

        if (sno == 4)
        {
            tm.textAsset = Resources.Load<TextAsset>("Prologue");
            //ae.sceneStr = "prologue";
        }
        else if (sno == 5)
        {
            tm.textAsset = Resources.Load<TextAsset>("NormalEnd");
            ae.sceneStr = "normal";
        }
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
