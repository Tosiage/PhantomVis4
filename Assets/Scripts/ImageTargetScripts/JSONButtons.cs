using HoloToolkit.Unity.InputModule;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//for saving and loading the user calibrations
public class JSONButtons : MonoBehaviour, IInputClickHandler
{
    public TargetManager tm;
    public ShowAndHide s;
    public void OnInputClicked(InputClickedEventData eventData)
    {
        if (this.name == "DataSave")
        {
            s.HideCalibrationInterface();
            s.ShowOrgans();
            tm.SaveData();
        }
        if (this.name == "DataLoad")
        {
            tm.SelectDataToLoad(this.transform.position, this.transform.rotation); 
        }
        else
        {
            if (this.name != "DataSave" && this.name != "DataLoad")
            {
                tm.DeleteAllBorders();
                tm.LoadData(this.name);
                tm.LoadBorders();
            }

        }
    }

    // Use this for initialization
    void Start()
    {
        tm = GameObject.Find("TargetManager").GetComponent<TargetManager>();
        s = GameObject.Find("TargetManager").GetComponent<ShowAndHide>();
    }


}
