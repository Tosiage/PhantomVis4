using HoloToolkit.Unity.InputModule;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultButton : MonoBehaviour, IInputClickHandler {
    public GameObject calibration;
    public GameObject polarisModel;
    public GameObject modelParent;
    public GameObject modelParentPhantom;
    public TargetManager tm;
    public Calibrate c;
    public PolarisCalibration pc;

    public void OnInputClicked(InputClickedEventData eventData)
    {
        tm.DeleteAllBorders();
        c.enabled = false;
        pc.enabled = true;
        modelParent.SetActive(false);
        modelParentPhantom.SetActive(false);
        foreach(TargetData td in tm.targetDatas)
        {
            if (td.polarisCalibrated)
            {
                td.calibrated = true;
            }
        }
        polarisModel.SetActive(true);
        polarisModel.transform.localScale = new Vector3(0.18f, 0.345f, 0.525f);
        tm.LoadBorders();

    }

    // Use this for initialization
    void Start () {
        calibration = GameObject.Find("Calibration");
        polarisModel = GameObject.Find("PolarisBox");
        modelParent = GameObject.Find("ModelParent");
        modelParentPhantom = GameObject.Find("ModelParentPhantom");
        tm = GameObject.Find("TargetManager").GetComponent<TargetManager>();
        c = calibration.GetComponent<Calibrate>();
        pc = calibration.GetComponent<PolarisCalibration>();
    }
	

}
