using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonInteractable : MonoBehaviour {
    private TargetManager targetManager;
    private Collider collider;
    private TextMesh textMesh;
	// Use this for initialization
	void Start () {
        targetManager = GameObject.Find("TargetManager").GetComponent<TargetManager>();
        collider = this.gameObject.GetComponent<Collider>();
        textMesh = this.gameObject.GetComponentInChildren<TextMesh>();
	}
	
	// Update is called once per frame
	void Update () {
        if (!targetManager.atLeastOneVisible)
        {
            collider.enabled = false;
            textMesh.color = Color.gray;
        }
        else
        {
            collider.enabled = true;
            textMesh.color = Color.white;
        }
	}
}
