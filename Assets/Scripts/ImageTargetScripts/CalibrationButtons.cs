using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit;
using HoloToolkit.Unity.InputModule;
using HoloToolkit.Unity;

public class CalibrationButtons : MonoBehaviour, IInputClickHandler {
    //model of phantom 
    public GameObject model;
    Calibrate calibrate;

    void Start()
    {
        calibrate = GameObject.Find("Calibration").GetComponent<Calibrate>();
        
    }

    public void OnInputClicked(InputClickedEventData eventData)
    {

        //Translation Buttons
        if(this.name == "arrowYPositive")
        {
            model.transform.position = new Vector3(model.transform.position.x, model.transform.position.y + 0.01f, model.transform.position.z);
            calibrate.SaveToTemp();
        }
        if (this.name == "arrowYNegative")
        {
            model.transform.position = new Vector3(model.transform.position.x, model.transform.position.y - 0.01f, model.transform.position.z);
            calibrate.SaveToTemp();
        }
        if (this.name == "arrowXPositive")
        {
            model.transform.position = new Vector3(model.transform.position.x + 0.01f, model.transform.position.y, model.transform.position.z);
            calibrate.SaveToTemp();
        }
        if (this.name == "arrowXNegative")
        {
            model.transform.position = new Vector3(model.transform.position.x - 0.01f, model.transform.position.y, model.transform.position.z);
            calibrate.SaveToTemp();
        }
        if (this.name == "arrowZPositive")
        {
            model.transform.position = new Vector3(model.transform.position.x, model.transform.position.y, model.transform.position.z + 0.01f);
            calibrate.SaveToTemp();
        }
        if (this.name == "arrowZNegative")
        {
            model.transform.position = new Vector3(model.transform.position.x, model.transform.position.y, model.transform.position.z - 0.01f);
            calibrate.SaveToTemp();
        }

        //Rotation Buttons
        if (this.name == "buttonYAxis")
        {
            model.transform.Rotate(0, 5, 0);
            calibrate.SaveToTemp();
        }
        if (this.name == "buttonXAxis")
        {
            model.transform.Rotate(5, 0, 0);
            calibrate.SaveToTemp();
        }
        if (this.name == "buttonZAxis")
        {
            model.transform.Rotate(0, 0, 5);
            calibrate.SaveToTemp();
        }

        //Save the temp to the offset for all targets currently visible
        if(this.name == "SavePositions")
        {
            calibrate.SaveToTemp();
            calibrate.SaveOffset();
        }
    }

 
}
