using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit;
using HoloToolkit.Unity.InputModule;
using HoloToolkit.Unity;

//only used for calibration target button

public class ButtonScript : MonoBehaviour, IInputClickHandler
{
    private Calibrate calibrate;

    public void OnInputClicked(InputClickedEventData eventData)
    {
        calibrate = GameObject.Find("Calibration").GetComponent<Calibrate>();

        if (this.name == "ButtonStart")
        {
            this.transform.localScale = Vector3.zero;
            GameObject.Find("TitleMessage").transform.localScale = Vector3.zero;
            GameObject.Find("TitleMessage3").transform.localScale = new Vector3(0.00215f, 0.00215f, 0.00215f);
            GameObject.Find("ButtonWeiter").transform.localScale = new Vector3(1f, 1f, 1f);
            GameObject.Find("Dialog").GetComponent<SolverConstantViewSize>().enabled = false;
            GameObject.Find("Dialog").GetComponent<SolverRadialView>().enabled = false;

            GameObject.FindGameObjectWithTag("hologram").GetComponent<ModelPositionUpdater>().enabled = false;
            GameObject.Find("Calibration").transform.localScale = Vector3.one;
            GameObject.Find("Calibration").GetComponent<Calibrate>().enabled = true;
            
        }
        if (this.name == "ButtonFinish")
        {
            this.transform.localScale = Vector3.zero;
            GameObject.Find("TitleMessage3").transform.localScale = Vector3.zero;
            GameObject.Find("TitleMessage").transform.localScale = new Vector3(0.00215f, 0.00215f, 0.00215f);
            GameObject.Find("ButtonStart").transform.localScale = new Vector3(1f, 1f, 1f);

            GameObject.FindGameObjectWithTag("hologram").GetComponent<ModelPositionUpdater>().enabled = true;
            GameObject.Find("Calibration").transform.localScale = Vector3.zero;
            GameObject.Find("Calibration").GetComponent<Calibrate>().enabled = false;
        }
        if (this.name == "ButtonWeiter")
        {
            this.transform.localScale = Vector3.zero;
            GameObject.Find("TitleMessage3").transform.localScale = Vector3.zero;
            GameObject.Find("TitleMessage").transform.localScale = new Vector3(0.00215f, 0.00215f, 0.00215f);
            GameObject.Find("ButtonStart").transform.localScale = new Vector3(1f, 1f, 1f);
            GameObject.Find("Calibration").GetComponent<Calibrate>().calibrationStep1 = false;

            
        }
        if(this.name == "CalibrationTargetButton")
        {
            calibrate.FinishStep1();
            GameObject.Find("Translation").transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            GameObject.Find("Rotation").transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        }
    }

   
}
