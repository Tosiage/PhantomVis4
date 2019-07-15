using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetManager : MonoBehaviour {
    public GameObject[] targets;
    public List<TargetData> targetDatas;
    public bool atLeastOneVisible;

    // Use this for initialization
    void Start () {

        atLeastOneVisible = false;
        targets = GameObject.FindGameObjectsWithTag("target");
        foreach (GameObject t in targets)
        {
            var data = t.GetComponent<TargetData>();
            targetDatas.Add(data);
        }
    }
	
	// Update is called once per frame
	void Update () {
        atLeastOneVisible = false;
        foreach (TargetData t in targetDatas)
        {
            if (t.isVisible)
            {
                atLeastOneVisible = true;
                return;
            }
        }
    }
}
