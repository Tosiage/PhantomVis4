using HoloToolkit.Unity.InputModule;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelDialogButtons : MonoBehaviour, IInputClickHandler {
    public GameObject box;
    public GameObject boxParent;
    public GameObject phantom;
    public GameObject phantomParent;
    public GameObject polarisModel;
    public TargetManager tm;
    public Calibrate c;
    public PolarisCalibration pc;
    public TargetData td;
    public GameObject calibTarget;
    public void OnInputClicked(InputClickedEventData eventData)
    {
        if(this.name == "BoxButton")
        {
            tm.DeleteAllBorders();   
            c.enabled = true;
            pc.enabled = false;
            boxParent.SetActive(true);
            boxParent.transform.localScale = Vector3.one;
            c.SetModelAndParent(box, boxParent);
            td.relativePos = new Vector3(0f, -0.101f, 0.061f);
            phantomParent.SetActive(false);
            polarisModel.SetActive(false);
            c.calibrationStep1 = true;
            c.transform.GetChild(0).localScale = Vector3.one;
            c.transform.GetChild(1).localScale = new Vector3(0.5f, 0.5f, 0.5f);
            calibTarget.transform.GetChild(1).localScale = Vector3.zero;
            calibTarget.transform.GetChild(0).localScale = new Vector3(10f, 10f, 10f);
        }
        if(this.name == "PhantomButton")
        {
            tm.DeleteAllBorders();
            c.enabled = true;
            pc.enabled = false;
            phantomParent.SetActive(true);
            phantomParent.transform.localScale = Vector3.one;
            c.SetModelAndParent(phantom, phantomParent);
            td.relativePos = new Vector3(-0.022f, -0.261f, 0.388f);
            boxParent.SetActive(false);
            polarisModel.SetActive(false);
            c.calibrationStep1 = true;
            c.transform.GetChild(0).localScale = Vector3.one;
            c.transform.GetChild(1).localScale = new Vector3(0.5f, 0.5f, 0.5f);
            calibTarget.transform.GetChild(1).localScale = Vector3.zero;
            calibTarget.transform.GetChild(0).localScale = new Vector3(10f, 10f, 10f);
        }
    }

    // Use this for initialization
    void Start () {
        box = GameObject.Find("Box");
        boxParent = GameObject.Find("ModelParent");
        phantom = GameObject.Find("TorsoM");
        phantomParent = GameObject.Find("ModelParentPhantom");
        c = GameObject.Find("Calibration").GetComponent<Calibrate>();
        pc = GameObject.Find("Calibration").GetComponent<PolarisCalibration>();
        td = GameObject.Find("CalibrationTarget").GetComponent<TargetData>();
        polarisModel = GameObject.Find("PolarisBox");
        tm = GameObject.Find("TargetManager").GetComponent<TargetManager>();
        calibTarget = GameObject.Find("CalibrationTarget");
        

    }
	
	
}
