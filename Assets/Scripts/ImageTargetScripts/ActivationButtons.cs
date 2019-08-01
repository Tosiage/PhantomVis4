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
    // Use this for initialization
    void Start()
    {
        XAxis = GameObject.FindGameObjectsWithTag("X");
        YAxis = GameObject.FindGameObjectsWithTag("Y");
        ZAxis = GameObject.FindGameObjectsWithTag("Z");
        foreach (GameObject go in XAxis)
        {
            Debug.Log("X " + go.transform.tag + " " + go.transform.name);
        }
        
        foreach (GameObject go in YAxis)
        {
            Debug.Log("Y " + go.transform.tag + " " + go.transform.name);
        }
       
        foreach (GameObject go in ZAxis)
        {
            Debug.Log("Z " + go.transform.tag + " " + go.transform.name);
        }
        Debug.Log("X length " + XAxis.Length);
        Debug.Log("Y length " + YAxis.Length);
        Debug.Log("Z length " + ZAxis.Length);
    }

    public void OnInputClicked(InputClickedEventData eventData)
    {
        if(this.name == "ActivateX")
        {
            foreach(GameObject go in XAxis)
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
        }
        if(this.name == "ActivateY")
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
        }
        if (this.name == "ActivateZ")
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
        }
    }

   
	

}
