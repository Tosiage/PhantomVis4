using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;
using HoloToolkit;
using HoloToolkit.Unity.InputModule;

public class ReTrack : MonoBehaviour, IInputClickHandler {
    
    public void OnInputClicked(InputClickedEventData eventData)
    {
        Debug.Log("clicked");

    }
}
