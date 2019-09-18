using HoloToolkit.Unity.InputModule;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Loads the "default" calibration, the polaris calibration
public class DefaultButton : MonoBehaviour, IInputClickHandler {
    public GameObject calibration;
    public GameObject polaris;
    public GameObject polarisModel;
    public GameObject modelParent;
    public GameObject modelParentPhantom;
    public TargetManager tm;
    public Calibrate c;
    public Polaris pc;

    public void OnInputClicked(InputClickedEventData eventData)
    {
        tm.DeleteAllBorders(); //borders are the gameobjects signalising if a marker is calibrated
        //deleteallborders deletes the borders from a previous calibration
        c.transform.GetChild(0).localScale = Vector3.zero;
        c.enabled = false;
        pc.enabled = true;
        modelParent.SetActive(false);
        modelParentPhantom.SetActive(false);
        foreach(TargetData td in tm.targetDatas)
        {
            if (td.polarisCalibrated)
            {
                td.calibrated = true;
                td.relativePos = td.polarisPos;
                td.relativeRot = td.polarisRot;
            }
        }
        polarisModel.SetActive(true);
        polarisModel.transform.localScale = new Vector3(0.001f, 0.001f, 0.001f);
        tm.LoadBorders(); //show borders for all markers that are calibrated with polaris

    }

    // Use this for initialization
    void Start () {
        calibration = GameObject.Find("Calibration");
        polarisModel = GameObject.Find("PolarisPhantom");
        modelParent = GameObject.Find("ModelParent");
        modelParentPhantom = GameObject.Find("ModelParentPhantom");
        tm = GameObject.Find("TargetManager").GetComponent<TargetManager>();
        c = calibration.GetComponent<Calibrate>();
        polaris = GameObject.Find("Polaris");
        pc = polaris.GetComponent<Polaris>();
    }
	

}
