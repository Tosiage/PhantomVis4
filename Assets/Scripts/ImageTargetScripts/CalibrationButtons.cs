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
    ModelData md;

    void Start()
    {
        calibrate = GameObject.Find("Calibration").GetComponent<Calibrate>();
        md = model.GetComponent<ModelData>();

    }

    public void OnInputClicked(InputClickedEventData eventData)
    {

        if (this.name == "CalibrationTargetButton")
        {
            calibrate.FinishStep1();
        }

        //Save the temp to the offset for all targets currently visible
        if (this.name == "SavePositions")
        {

            calibrate.SaveOffset();

        }

        if (this.name == "Up")
        {

            md.posTempModel = new Vector3(md.posTempModel.x, md.posTempModel.y + 0.01f, md.posTempModel.z);
            Debug.Log("Up");

        }
        if (this.name == "Down")
        {
            md.posTempModel = new Vector3(md.posTempModel.x, md.posTempModel.y - 0.01f, md.posTempModel.z);
            Debug.Log("Down");
        }
        if (this.name == "Back")
        {
            md.posTempModel = new Vector3(md.posTempModel.x, md.posTempModel.y, md.posTempModel.z + 0.01f);
            Debug.Log("Back");
        }
        if (this.name == "Front")
        {
            md.posTempModel = new Vector3(md.posTempModel.x, md.posTempModel.y, md.posTempModel.z - 0.01f);
            Debug.Log("Front");
        }
        if (this.name == "Right")
        {
            md.posTempModel = new Vector3(md.posTempModel.x + 0.01f, md.posTempModel.y, md.posTempModel.z);
            Debug.Log("Right");
        }
        if (this.name == "Left")
        {
            md.posTempModel = new Vector3(md.posTempModel.x - 0.01f, md.posTempModel.y, md.posTempModel.z);
            Debug.Log("Left");
        }

        if (this.name == "RotateX")
        {
            model.transform.Rotate(5, 0, 0, Space.World);
            md.rotTempModel = model.transform.localRotation;
        }
        if (this.name == "RotateXm")
        {
            model.transform.Rotate(-5, 0, 0, Space.World);
            md.rotTempModel = model.transform.localRotation;
        }

        if (this.name == "RotateY")
        {
            model.transform.Rotate(0, 5, 0, Space.World);
            md.rotTempModel = model.transform.localRotation;
        }
        if (this.name == "RotateYm")
        {
            model.transform.Rotate(0, -5, 0, Space.World);
            md.rotTempModel = model.transform.localRotation;
        }

        if (this.name == "RotateZ")
        {
            model.transform.Rotate(0, 0, 5, Space.World);
            md.rotTempModel = model.transform.localRotation;
        }
        if (this.name == "RotateZm")
        {
            model.transform.Rotate(0, 0, -5, Space.World);
            md.rotTempModel = model.transform.localRotation;
        }
    }


}
