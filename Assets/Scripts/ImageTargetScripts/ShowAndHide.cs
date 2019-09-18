using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//at the beginning the user can decite if he wants to use the box model, the phantom, or the polaris calibration (also phantom but without calibration interface)
//shows or hides the chosen model
public class ShowAndHide : MonoBehaviour {
    GameObject polarisPhantom;
    GameObject phantom;
    GameObject axisButtons;

	// Use this for initialization
	void Start () {
        polarisPhantom = GameObject.Find("PolarisPhantom"); 
        phantom = GameObject.Find("PhantomComplete");
        axisButtons = GameObject.Find("ActivateButtons"); 
	}
	
    public void HideOrgans()
    {
        if (polarisPhantom.activeSelf)
        {
            polarisPhantom.transform.GetChild(0).GetComponent<Renderer>().enabled = false;
            polarisPhantom.transform.GetChild(1).GetComponent<Renderer>().enabled = false;
            polarisPhantom.transform.GetChild(2).GetComponent<Renderer>().enabled = false;
            polarisPhantom.transform.GetChild(3).GetComponent<Renderer>().enabled = false;
            polarisPhantom.transform.GetChild(4).GetComponent<Renderer>().enabled = false;
            polarisPhantom.transform.GetChild(5).GetComponent<Renderer>().enabled = false;
            polarisPhantom.transform.GetChild(6).GetComponent<Renderer>().enabled = false;
        }
        else if (phantom.activeSelf)
        {
            phantom.transform.GetChild(0).GetComponent<Renderer>().enabled = false;
            phantom.transform.GetChild(1).GetComponent<Renderer>().enabled = false;
            phantom.transform.GetChild(2).GetComponent<Renderer>().enabled = false;
            phantom.transform.GetChild(3).GetComponent<Renderer>().enabled = false;
            phantom.transform.GetChild(4).GetComponent<Renderer>().enabled = false;
            phantom.transform.GetChild(5).GetComponent<Renderer>().enabled = false;
            phantom.transform.GetChild(6).GetComponent<Renderer>().enabled = false;
        }
    }

    public void HideTorso()
    {
        if (polarisPhantom.activeSelf)
        {
            polarisPhantom.transform.GetChild(7).GetComponent<Renderer>().enabled = false;
        }
        else if (phantom.activeSelf)
        {
            phantom.transform.GetChild(7).GetComponent<Renderer>().enabled = false;
        }
    }

    public void ShowOrgans()
    {
        if (polarisPhantom.activeSelf)
        {
            polarisPhantom.transform.GetChild(0).GetComponent<Renderer>().enabled = true;
            polarisPhantom.transform.GetChild(1).GetComponent<Renderer>().enabled = true;
            polarisPhantom.transform.GetChild(2).GetComponent<Renderer>().enabled = true;
            polarisPhantom.transform.GetChild(3).GetComponent<Renderer>().enabled = true;
            polarisPhantom.transform.GetChild(4).GetComponent<Renderer>().enabled = true;
            polarisPhantom.transform.GetChild(5).GetComponent<Renderer>().enabled = true;
            polarisPhantom.transform.GetChild(6).GetComponent<Renderer>().enabled = true;
        }
        else if (phantom.activeSelf)
        {
            phantom.transform.GetChild(0).GetComponent<Renderer>().enabled = true;
            phantom.transform.GetChild(1).GetComponent<Renderer>().enabled = true;
            phantom.transform.GetChild(2).GetComponent<Renderer>().enabled = true;
            phantom.transform.GetChild(3).GetComponent<Renderer>().enabled = true;
            phantom.transform.GetChild(4).GetComponent<Renderer>().enabled = true;
            phantom.transform.GetChild(5).GetComponent<Renderer>().enabled = true;
            phantom.transform.GetChild(6).GetComponent<Renderer>().enabled = true;
        }
    }

    public void ShowTorso()
    {
        if (polarisPhantom.activeSelf)
        {
            polarisPhantom.transform.GetChild(7).GetComponent<Renderer>().enabled = true;
        }
        else if (phantom.activeSelf)
        {
            phantom.transform.GetChild(7).GetComponent<Renderer>().enabled = true;
        }
    }

    public void HideCalibrationInterface()
    {
        axisButtons.transform.localScale = Vector3.zero;
        if (phantom.activeSelf)
        {
            phantom.transform.GetChild(8).localScale = Vector3.zero;
        }
    }

    public void ShowCalibrationInterface()
    {
        axisButtons.transform.localScale = Vector3.one;
        if (phantom.activeSelf)
        {
            phantom.transform.GetChild(8).localScale = new Vector3 (1000f,1000f,1000f);
        }
    }
}
