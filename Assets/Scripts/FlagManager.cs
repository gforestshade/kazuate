using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagManager : MonoBehaviour
{
    private static Dictionary<string, int> nSeeScenes = new Dictionary<string, int>(); 
    public static int sceneNo;

    public static int addSeeScene(string scene, int change = 1)
    {
        int n;
        if (nSeeScenes.TryGetValue(scene, out n))
        {
            n += change;
            nSeeScenes[scene] = n;
            return n;
        }
        else
        {
            nSeeScenes.Add(scene, change);
            return 0;
        }
    }

    public static int getSeeScene(string scene)
    {
        int n;
        if (nSeeScenes.TryGetValue(scene, out n))
        {
            return n;
        }
        else
        {
            nSeeScenes.Add(scene, 0);
            return 0;
        }
    }

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
