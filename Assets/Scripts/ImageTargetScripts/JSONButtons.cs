using HoloToolkit.Unity.InputModule;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JSONButtons : MonoBehaviour, IInputClickHandler
{
    public TargetManager tm;
    public void OnInputClicked(InputClickedEventData eventData)
    {
        if (this.name == "DataSave")
        {
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
                tm.LoadData(this.name);
            }

        }
    }

    // Use this for initialization
    void Start()
    {
        tm = GameObject.Find("TargetManager").GetComponent<TargetManager>();
    }


}
