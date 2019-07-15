using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class CustomTrackableEventHandler : MonoBehaviour, ITrackableEventHandler {
    #region PROTECTED_MEMBER_VARIABLES

    protected TrackableBehaviour mTrackableBehaviour;
    protected TrackableBehaviour.Status m_PreviousStatus;
    protected TrackableBehaviour.Status m_NewStatus;
    private TargetData targetData;

    #endregion // PROTECTED_MEMBER_VARIABLES

    #region UNITY_MONOBEHAVIOUR_METHODS

    protected virtual void Start()
    {
        targetData = GetComponent<TargetData>();
        mTrackableBehaviour = GetComponent<TrackableBehaviour>();
        if (mTrackableBehaviour)
            mTrackableBehaviour.RegisterTrackableEventHandler(this);

    }

    protected virtual void OnDestroy()
    {
        if (mTrackableBehaviour)
            mTrackableBehaviour.UnregisterTrackableEventHandler(this);
    }

    #endregion // UNITY_MONOBEHAVIOUR_METHODS

    #region PUBLIC_METHODS

    /// <summary>
    ///     Implementation of the ITrackableEventHandler function called when the
    ///     tracking state changes.
    /// </summary>
    public void OnTrackableStateChanged(
        TrackableBehaviour.Status previousStatus,
        TrackableBehaviour.Status newStatus)
    {
        m_PreviousStatus = previousStatus;
        m_NewStatus = newStatus;

        if (newStatus == TrackableBehaviour.Status.DETECTED)
        {
            Debug.Log("Trackable " + mTrackableBehaviour.TrackableName + " detected " + newStatus.ToString());
            OnTrackingFound(Color.blue, false, false);
        }
        else if (newStatus == TrackableBehaviour.Status.TRACKED)
        {
            Debug.Log("Trackable " + mTrackableBehaviour.TrackableName + " tracked " + newStatus.ToString());
            OnTrackingFound(Color.green, true, false);
        }
        else if (newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED)
        {
            Debug.Log("Trackable " + mTrackableBehaviour.TrackableName + " extended tracked " + newStatus.ToString());
            OnTrackingFound(Color.yellow, false, true);
        }
        else if(newStatus == TrackableBehaviour.Status.LIMITED)
        {
            Debug.Log("Trackable " + mTrackableBehaviour.TrackableName + " limited " + newStatus.ToString());
            OnTrackingFound(Color.red, false, false);
        }
        else if (newStatus == TrackableBehaviour.Status.NO_POSE)
        {
            Debug.Log("Trackable " + mTrackableBehaviour.TrackableName + " lost" + newStatus.ToString());
            OnTrackingFound(Color.black, false, false);
            //
        }
        else
        {
            // For combo of previousStatus=UNKNOWN + newStatus=UNKNOWN|NOT_FOUND
            // Vuforia is starting, but tracking has not been lost or found yet
            // Call OnTrackingLost() to hide the augmentations
            OnTrackingLost(Color.black);
        }
    }

    #endregion // PUBLIC_METHODS

    #region PROTECTED_METHODS

    protected virtual void OnTrackingFound(Color MaterialColor, bool isVisible, bool isExtendedTracked)
    {
        var rendererComponents = GetComponentsInChildren<Renderer>(true);
        var colliderComponents = GetComponentsInChildren<Collider>(true);
        var canvasComponents = GetComponentsInChildren<Canvas>(true);

        targetData.isVisible = isVisible;
        targetData.isExtendedTracked = isExtendedTracked;
        // Enable rendering:
        foreach (var component in rendererComponents)
        {
            component.enabled = true;
            if(mTrackableBehaviour.TrackableName != "ar_marker17")
            {
                component.material.color = MaterialColor;
            }
            
        }    
            
        // Enable colliders:
        foreach (var component in colliderComponents)
            component.enabled = true;

        // Enable canvas':
        foreach (var component in canvasComponents)
            component.enabled = true;
    }


    protected virtual void OnTrackingLost(Color MaterialColor)
    { }

    #endregion // PROTECTED_METHODS
}
