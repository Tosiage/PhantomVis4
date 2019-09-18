using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit;
using HoloToolkit.Unity.InputModule;
using HoloToolkit.Unity;

//For User Interface (Interface with arrows for moving the Hologram)
//All GameObjects that are part of the user interface have a tag
//x-tag: arrows (rotation and translation buttons) and axis that move the hologram along the x-axis (same for y and z tags)
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
            //sets all the arrows and the axis-gameobject that are used to move the hologram along the x-axis active
            //sets gameobjects for z- and y-axis inactive
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
            //sets all arrows inactive and the axis-gameobjects active
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
            //sets buttons and axis-gameobject for y-axis active
            //sets buttons and axis-gamebjects for x- and z- axis inactive
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
            //sets all buttons inactive and all axis-gameobjects active
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
            //set all buttons and axis-gameobject for z-axis active
            //sets all buttons and axis-gameobject for x and y-axis inactive
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
            //sets all buttons for z-axis inactive
            //sets all axis-gameobjects active
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
