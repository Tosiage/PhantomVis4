using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit;
using HoloToolkit.Unity.InputModule;
using HoloToolkit.Unity;
public class ActivationButtons : MonoBehaviour, IInputClickHandler {
    public GameObject[] XAxis;
    public GameObject[] YAxis;
    public GameObject[] ZAxis;
    public bool xClicked;
    public bool yClicked;
    public bool zClicked;
    public GameObject xAxisBox;
    public GameObject yAxisBox;
    public GameObject zAxisBox;
    public GameObject xAxisPhantom;
    public GameObject yAxisPhantom;
    public GameObject zAxisPhantom;

    // Use this for initialization
    void Start()
    {
        xClicked = false;
        yClicked = false;
        zClicked = false;
        XAxis = GameObject.FindGameObjectsWithTag("X");
        YAxis = GameObject.FindGameObjectsWithTag("Y");
        ZAxis = GameObject.FindGameObjectsWithTag("Z");
        xAxisBox = GameObject.Find("XAxis");
        yAxisBox = GameObject.Find("YAxis");
        zAxisBox = GameObject.Find("ZAxis");
        xAxisPhantom = GameObject.Find("XAxisPhantom");
        yAxisPhantom = GameObject.Find("YAxisPhantom");
        zAxisPhantom = GameObject.Find("ZAxisPhantom");
    }

    public void OnInputClicked(InputClickedEventData eventData)
    {
        if(this.name == "ActivateX")
        {
            if (!xClicked)
            {
                foreach (GameObject go in XAxis)
                {
                    go.GetComponent<Renderer>().enabled = true;
                    go.GetComponent<Collider>().enabled = true;
                }
                foreach (GameObject go in YAxis)
                {
                    go.GetComponent<Renderer>().enabled = false;
                    go.GetComponent<Collider>().enabled = false;
                }
                foreach (GameObject go in ZAxis)
                {
                    go.GetComponent<Renderer>().enabled = false;
                    go.GetComponent<Collider>().enabled = false;
                }
                xClicked = true;
            }
            else if (xClicked)
            {
                foreach (GameObject go in XAxis)
                {
                    go.GetComponent<Renderer>().enabled = false;
                    go.GetComponent<Collider>().enabled = false;
                }
                foreach (GameObject go in YAxis)
                {
                    go.GetComponent<Renderer>().enabled = false;
                    go.GetComponent<Collider>().enabled = false;
                }
                foreach (GameObject go in ZAxis)
                {
                    go.GetComponent<Renderer>().enabled = false;
                    go.GetComponent<Collider>().enabled = false;
                }
                xAxisBox.GetComponent<Renderer>().enabled = true;
                yAxisBox.GetComponent<Renderer>().enabled = true;
                zAxisBox.GetComponent<Renderer>().enabled = true;
                xAxisPhantom.GetComponent<Renderer>().enabled = true;
                yAxisPhantom.GetComponent<Renderer>().enabled = true;
                zAxisPhantom.GetComponent<Renderer>().enabled = true;
                xClicked = false; 
            }
           
        }
        if(this.name == "ActivateY")
        {
            if (!yClicked)
            {
                foreach (GameObject go in XAxis)
                {
                    go.GetComponent<Renderer>().enabled = false;
                    go.GetComponent<Collider>().enabled = false;
                }
                foreach (GameObject go in YAxis)
                {
                    go.GetComponent<Renderer>().enabled = true;
                    go.GetComponent<Collider>().enabled = true;
                }
                foreach (GameObject go in ZAxis)
                {
                    go.GetComponent<Renderer>().enabled = false;
                    go.GetComponent<Collider>().enabled = false;
                }
                yClicked = true;
            }
            else if (yClicked)
            {
                foreach (GameObject go in XAxis)
                {
                    go.GetComponent<Renderer>().enabled = false;
                    go.GetComponent<Collider>().enabled = false;
                }
                foreach (GameObject go in YAxis)
                {
                    go.GetComponent<Renderer>().enabled = false;
                    go.GetComponent<Collider>().enabled = false;
                }
                foreach (GameObject go in ZAxis)
                {
                    go.GetComponent<Renderer>().enabled = false;
                    go.GetComponent<Collider>().enabled = false;
                }
                xAxisBox.GetComponent<Renderer>().enabled = true;
                yAxisBox.GetComponent<Renderer>().enabled = true;
                zAxisBox.GetComponent<Renderer>().enabled = true;
                xAxisPhantom.GetComponent<Renderer>().enabled = true;
                yAxisPhantom.GetComponent<Renderer>().enabled = true;
                zAxisPhantom.GetComponent<Renderer>().enabled = true;
                yClicked = false;
            }
           
        }
        if (this.name == "ActivateZ")
        {
            if (!zClicked)
            {
                foreach (GameObject go in XAxis)
                {
                    go.GetComponent<Renderer>().enabled = false;
                    go.GetComponent<Collider>().enabled = false;
                }
                foreach (GameObject go in YAxis)
                {
                    go.GetComponent<Renderer>().enabled = false;
                    go.GetComponent<Collider>().enabled = false;
                }
                foreach (GameObject go in ZAxis)
                {
                    go.GetComponent<Renderer>().enabled = true;
                    go.GetComponent<Collider>().enabled = true;
                }
                zClicked = true;
            }
           else if (zClicked)
            {
                foreach (GameObject go in XAxis)
                {
                    go.GetComponent<Renderer>().enabled = false;
                    go.GetComponent<Collider>().enabled = false;
                }
                foreach (GameObject go in YAxis)
                {
                    go.GetComponent<Renderer>().enabled = false;
                    go.GetComponent<Collider>().enabled = false;
                }
                foreach (GameObject go in ZAxis)
                {
                    go.GetComponent<Renderer>().enabled = false;
                    go.GetComponent<Collider>().enabled = false;
                }
           
                xAxisBox.GetComponent<Renderer>().enabled = true;
                yAxisBox.GetComponent<Renderer>().enabled = true;
                zAxisBox.GetComponent<Renderer>().enabled = true;
                xAxisPhantom.GetComponent<Renderer>().enabled = true;
                yAxisPhantom.GetComponent<Renderer>().enabled = true;
                zAxisPhantom.GetComponent<Renderer>().enabled = true;
                zClicked = false;
            }
           
        }
    }

   
	

}
