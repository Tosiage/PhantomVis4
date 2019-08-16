using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonInteractable : MonoBehaviour {
    private TargetManager targetManager;
    private Collider c;
    private TextMesh textMesh;
	// Use this for initialization
	void Start () {
        targetManager = GameObject.Find("TargetManager").GetComponent<TargetManager>();
        c = this.gameObject.GetComponent<Collider>();
        textMesh = this.gameObject.GetComponentInChildren<TextMesh>();
	}
	
	// Update is called once per frame
	void Update () {
        if (!targetManager.atLeastOneVisible)
        {
            c.enabled = false;
            textMesh.color = Color.gray;
        }
        else
        {
            c.enabled = true;
            textMesh.color = Color.white;
        }
	}
}
