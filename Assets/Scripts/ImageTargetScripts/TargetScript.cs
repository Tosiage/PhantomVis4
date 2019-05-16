using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class TargetScript : MonoBehaviour, ITrackableEventHandler {

    private TrackableBehaviour mTrackableBehaviour;
    public bool targetTrackingFound = false;
    public GameObject modelToShow;
    // Use this for initialization
    void Start()
    {
        modelToShow = GameObject.Find("greenCube");
        mTrackableBehaviour = GetComponent<TrackableBehaviour>();
        if (mTrackableBehaviour)
        {
            mTrackableBehaviour.RegisterTrackableEventHandler(this);
        }
    }
    // Update is called once per frame
    void Update()
    {
    }
    public void OnTrackableStateChanged(
      TrackableBehaviour.Status previousStatus,
      TrackableBehaviour.Status newStatus)
    {
        if (newStatus == TrackableBehaviour.Status.DETECTED ||
            newStatus == TrackableBehaviour.Status.TRACKED ||
            newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED)
        {
            OnTrackingFound();
        }
        else if (previousStatus == TrackableBehaviour.Status.TRACKED &&
               newStatus == TrackableBehaviour.Status.NO_POSE)
        {
            Debug.Log("Trackable " + mTrackableBehaviour.TrackableName + " lost");
            OnTrackingLost();
        }
        else
        {
            // For combo of previousStatus=UNKNOWN + newStatus=UNKNOWN|NOT_FOUND
            // Vuforia is starting, but tracking has not been lost or found yet
            // Call OnTrackingLost() to hide the augmentations
            OnTrackingLost();
        }
    }
    private void OnTrackingFound()
    {
        targetTrackingFound = true;
        modelToShow.GetComponent<TorsoPosition>().targetStatus = targetTrackingFound;
    }

    private void OnTrackingLost()
    {
        targetTrackingFound = false;
    }
}
