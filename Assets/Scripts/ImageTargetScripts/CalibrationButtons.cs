using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit;
using HoloToolkit.Unity.InputModule;
using HoloToolkit.Unity;

public class CalibrationButtons : MonoBehaviour, IInputClickHandler
{
    //model of phantom 
    public GameObject model;
    Calibrate calibrate;

    void Start()
    {
        calibrate = GameObject.Find("Calibration").GetComponent<Calibrate>();

    }

    public void OnInputClicked(InputClickedEventData eventData)
    {

        if (this.name == "CalibrationTargetButton")
        {
            calibrate.FinishStep1();
            //GameObject.Find("Translation").transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            //GameObject.Find("Rotation").transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        }

        //Save the temp to the offset for all targets currently visible
        if (this.name == "SavePositions")
        {
            calibrate.SaveToTemp();
            calibrate.SaveOffset();
        }

        if (this.name == "Up")
        {
            model.transform.position = new Vector3(model.transform.position.x, model.transform.position.y + 0.01f, model.transform.position.z);
            calibrate.SaveToTemp();
        }
        if (this.name == "Down")
        {
            model.transform.position = new Vector3(model.transform.position.x, model.transform.position.y - 0.01f, model.transform.position.z);
            calibrate.SaveToTemp();
        }
        if (this.name == "Back")
        {
            model.transform.position = new Vector3(model.transform.position.x, model.transform.position.y, model.transform.position.z + 0.01f);
            calibrate.SaveToTemp();
        }
        if (this.name == "Front")
        {
            model.transform.position = new Vector3(model.transform.position.x, model.transform.position.y, model.transform.position.z - 0.01f);
            calibrate.SaveToTemp();
        }
        if (this.name == "Right")
        {
            model.transform.position = new Vector3(model.transform.position.x + 0.01f, model.transform.position.y, model.transform.position.z);
            calibrate.SaveToTemp();
        }
        if (this.name == "Left")
        {
            model.transform.position = new Vector3(model.transform.position.x - 0.01f, model.transform.position.y, model.transform.position.z);
            calibrate.SaveToTemp();
        }

        if (this.name == "RotateX")
        {
            Debug.Log("RotateXbefore " + model.transform.rotation);
            model.transform.Rotate(5, 0, 0, Space.World);
            Debug.Log("RotateXafter " + model.transform.rotation);
            calibrate.SaveToTemp();
        }
        if (this.name == "RotateXm")
        {
            Debug.Log("RotateXbefore " + model.transform.rotation);
            model.transform.Rotate(-5, 0, 0, Space.World);
            Debug.Log("RotateXafter " + model.transform.rotation);
            calibrate.SaveToTemp();
        }

        if (this.name == "RotateY")
        {
            Debug.Log("RotateYbefore " + model.transform.rotation);
            model.transform.Rotate(0, 5, 0, Space.World);
            Debug.Log("RotateYafter " + model.transform.rotation);
            calibrate.SaveToTemp();
        }
        if (this.name == "RotateYm")
        {
            Debug.Log("RotateYbefore " + model.transform.rotation);
            model.transform.Rotate(0, -5, 0, Space.World);
            Debug.Log("RotateYafter " + model.transform.rotation);
            calibrate.SaveToTemp();
        }

        if (this.name == "RotateZ")
        {
            Debug.Log("RotateZbefore " + model.transform.rotation);
            model.transform.Rotate(0, 0, 5, Space.World);
            Debug.Log("RotateZafter " + model.transform.rotation);
            calibrate.SaveToTemp();
        }
        if (this.name == "RotateZm")
        {
            Debug.Log("RotateZbefore " + model.transform.rotation);
            model.transform.Rotate(0, 0, -5, Space.World);
            Debug.Log("RotateZafter " + model.transform.rotation);
            calibrate.SaveToTemp();
        }
    }


}
