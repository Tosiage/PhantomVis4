using HoloToolkit.Unity.InputModule;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//shows or hides the organs and the torso of the phantom
//in the app the user can toggle this via buttons
public class ShowHideButtons : MonoBehaviour, IInputClickHandler
{
    ShowAndHide s;
    public void OnInputClicked(InputClickedEventData eventData)
    {
        if(this.name == "ShowOrgans")
        {
            s.ShowOrgans();
        }
        else if (this.name == "HideOrgans")
        {
            s.HideOrgans();
        }
        else if (this.name == "ShowTorso")
        {
            s.ShowTorso();
        }
        else if (this.name == "HideTorso")
        {
            s.HideTorso();
        }
    }


    // Use this for initialization
    void Start () {
        s = GameObject.Find("TargetManager").GetComponent<ShowAndHide>();
	}
	
	
}
