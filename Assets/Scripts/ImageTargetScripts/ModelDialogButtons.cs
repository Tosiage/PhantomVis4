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
   

    public void OnInputClicked(InputClickedEventData eventData)
    {
        if(this.name == "BoxButton")
        {
            boxParent.SetActive(true);
            c.SetModelAndParent(box, boxParent);
            phantomParent.SetActive(false);
           
        }
        if(this.name == "PhantomButton")
        {
            phantomParent.SetActive(true);
            c.SetModelAndParent(phantom, phantomParent);
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
        

    }
	
	
}
