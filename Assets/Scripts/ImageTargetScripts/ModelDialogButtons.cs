using HoloToolkit.Unity.InputModule;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelDialogButtons : MonoBehaviour, IInputClickHandler {
    public GameObject box;
    public GameObject boxParent;
    public GameObject phantom;
    public GameObject phantomParent;

    public Calibrate c;
    public TargetData td;

    public void OnInputClicked(InputClickedEventData eventData)
    {
        if(this.name == "BoxButton")
        {
            boxParent.SetActive(true);
            c.SetModelAndParent(box, boxParent);
            td.relativePos = new Vector3(0f, -0.101f, 0.061f);
            phantomParent.SetActive(false);
           
        }
        if(this.name == "PhantomButton")
        {
            phantomParent.SetActive(true);
            c.SetModelAndParent(phantom, phantomParent);
            td.relativePos = new Vector3(-0.022f, -0.261f, 0.388f);
            boxParent.SetActive(false);
        }
    }

    // Use this for initialization
    void Start () {
        box = GameObject.Find("Box");
        boxParent = GameObject.Find("ModelParent");
        phantom = GameObject.Find("TorsoM");
        phantomParent = GameObject.Find("ModelParentPhantom");
        c = GameObject.Find("Calibration").GetComponent<Calibrate>();
        td = GameObject.Find("CalibrationTarget").GetComponent<TargetData>();
        

    }
	
	
}
