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
            this.transform.localScale = Vector3.zero;
            this.transform.parent.GetChild(1).localScale = new Vector3(10f, 10f, 10f); 
        }

        //Save the temp to the offset for all targets currently visible
        if (this.name == "SavePositions")
        {

            calibrate.SaveOffset();

        }

        if(this.name == "Up")
        {
            model.transform.localPosition = new Vector3(model.transform.localPosition.x, model.transform.localPosition.y + 0.01f, model.transform.localPosition.z);
          
        }
        if (this.name == "Down")
        {
            model.transform.localPosition = new Vector3(model.transform.localPosition.x, model.transform.localPosition.y - 0.01f, model.transform.localPosition.z);
        }
        if (this.name == "Back")
        {
            model.transform.localPosition = new Vector3(model.transform.localPosition.x, model.transform.localPosition.y, model.transform.localPosition.z + 0.01f);
        }
        if (this.name == "Front")
        {
            model.transform.localPosition = new Vector3(model.transform.localPosition.x, model.transform.localPosition.y, model.transform.localPosition.z - 0.01f);
        }
        if (this.name == "Right")
        {
            model.transform.localPosition = new Vector3(model.transform.localPosition.x + 0.01f, model.transform.localPosition.y, model.transform.localPosition.z);
        }
        if (this.name == "Left")
        {
            model.transform.localPosition = new Vector3(model.transform.localPosition.x - 0.01f, model.transform.localPosition.y, model.transform.localPosition.z);
        }

        if (this.name == "RotateX")
        {
            model.transform.Rotate(1, 0, 0, Space.Self);
        }
        if (this.name == "RotateXm")
        {
            model.transform.Rotate(-1, 0, 0, Space.Self);
        }

        if (this.name == "RotateY")
        {
            model.transform.Rotate(0, 1, 0, Space.Self);
        }
        if (this.name == "RotateYm")
        {
            model.transform.Rotate(0, -1, 0, Space.Self);
        }

        if (this.name == "RotateZ")
        {
            model.transform.Rotate(0, 0, 1, Space.Self);
        }
        if (this.name == "RotateZm")
        {
            model.transform.Rotate(0, 0, -1, Space.Self);
        }
    }


}
