using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetManager : MonoBehaviour {
    public GameObject[] targets;
    public List<TargetData> targetDatas;

    // Use this for initialization
    void Start () {
        targets = GameObject.FindGameObjectsWithTag("targets");
        foreach (GameObject t in targets)
        {
            var data = t.GetComponent<TargetData>();
            targetDatas.Add(data);
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
