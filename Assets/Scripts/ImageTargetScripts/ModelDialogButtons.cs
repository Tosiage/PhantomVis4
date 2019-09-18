using HoloToolkit.Unity.InputModule;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//the buttons on the dialog where the used model can be chosen when the app is running
public class ModelDialogButtons : MonoBehaviour, IInputClickHandler {
    public GameObject box;
    public GameObject boxParent;
    public GameObject phantom;
    public GameObject phantomParent;
    public GameObject polarisModel;
    public TargetManager tm;
    public Calibrate c;
    public Polaris pc;
    public TargetData td;
    public GameObject calibTarget;
    public GameObject polaris;
    public ShowAndHide s;
    public void OnInputClicked(InputClickedEventData eventData)
    {
        if(this.name == "BoxButton") //the box model is chosen
        {
            tm.DeleteAllBorders();   //delete all borders from previous used calibration
            c.enabled = true; //enable the calibration via hololens
            pc.enabled = false; //disable the polaris calibration
            boxParent.SetActive(true); 
            boxParent.transform.localScale = Vector3.one;
            c.SetModelAndParent(box, boxParent); //tell the calibration gameobject, that we want to use the box as model
            td.relativePos = new Vector3(0f, -0.101f, 0.061f); //the relativePos for the not attached marker that is used for initial calibration
            phantomParent.SetActive(false);
            polarisModel.SetActive(false);
            c.calibrationStep1 = true;
            c.transform.GetChild(0).localScale = Vector3.one;
            calibTarget.transform.GetChild(1).localScale = Vector3.zero; //"save" button that is displayed on the not attached marker
            calibTarget.transform.GetChild(0).localScale = new Vector3(10f, 10f, 10f); //"start calibration" button that is displayed on the not attached marker 
            s.ShowCalibrationInterface();
        }
        if(this.name == "PhantomButton") //the phantom is chosen
        {
            tm.DeleteAllBorders();
            c.enabled = true;
            pc.enabled = false;
            phantomParent.SetActive(true);
            phantomParent.transform.localScale = Vector3.one;
            c.SetModelAndParent(phantom, phantomParent);
            td.relativePos = new Vector3(-0.002f, -0.174f, -0.103f);
            boxParent.SetActive(false);
            polarisModel.SetActive(false);
            c.calibrationStep1 = true;
            c.transform.GetChild(0).localScale = Vector3.one;
            calibTarget.transform.GetChild(1).localScale = Vector3.zero;
            calibTarget.transform.GetChild(0).localScale = new Vector3(10f, 10f, 10f);
            s.ShowCalibrationInterface();
            s.HideOrgans();
            s.ShowTorso();
        }
    }

    // Use this for initialization
    void Start () {
        box = GameObject.Find("Box");
        boxParent = GameObject.Find("ModelParent");
        phantom = GameObject.Find("Phantom");
        phantomParent = GameObject.Find("ModelParentPhantom");
        c = GameObject.Find("Calibration").GetComponent<Calibrate>();
        pc = GameObject.Find("Polaris").GetComponent<Polaris>();
        td = GameObject.Find("CalibrationTarget").GetComponent<TargetData>();
        polarisModel = GameObject.Find("PolarisPhantom");
        tm = GameObject.Find("TargetManager").GetComponent<TargetManager>();
        s = GameObject.Find("TargetManager").GetComponent<ShowAndHide>();
        calibTarget = GameObject.Find("CalibrationTarget");
        

    }
	
	
}
