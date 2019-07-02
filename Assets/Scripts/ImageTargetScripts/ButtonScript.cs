using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit;
using HoloToolkit.Unity.InputModule;
using HoloToolkit.Unity;

public class ButtonScript : MonoBehaviour, IInputClickHandler
{
    
    public void OnInputClicked(InputClickedEventData eventData)
    {
        if(this.name == "ButtonStart")
        {
            this.transform.localScale = Vector3.zero;
            GameObject.Find("TitleMessage").transform.localScale = Vector3.zero;
            GameObject.Find("TitleMessage2").transform.localScale = new Vector3(0.00215f, 0.00215f, 0.00215f);
            GameObject.Find("ButtonFinish").transform.localScale = new Vector3(1f, 1f, 1f);
            GameObject.Find("Dialog").GetComponent<SolverConstantViewSize>().enabled = false;
            GameObject.Find("Dialog").GetComponent<SolverRadialView>().enabled = false;
        }
        if (this.name == "ButtonFinish")
        {
            this.transform.localScale = Vector3.zero;
            GameObject.Find("TitleMessage2").transform.localScale = Vector3.zero;
            GameObject.Find("TitleMessage").transform.localScale = new Vector3(0.00215f, 0.00215f, 0.00215f);
            GameObject.Find("ButtonStart").transform.localScale = new Vector3(1f, 1f, 1f);
        }
    }
}
