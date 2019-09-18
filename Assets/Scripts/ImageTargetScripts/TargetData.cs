using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TargetData : MonoBehaviour
{
    public Vector3 relativePos; //position offset between this marker and the hologram
    public Quaternion relativeRot; //rotation offset between this marker and the hologram
    public float angleX; //euler angles if you want to put the offset rotation manually in the unity editor. 
    public float angleY;
    public float angleZ;
    public bool isVisible; //if the marker is currently visible
    [HideInInspector] public bool isExtendedTracked; //if vuforia extended tracking is active 
    public bool calibrated; //if the marker is already calibrated
    public bool initialCalibration; //if the marker was visible at the beginning when the not attached marker was used to start the calibration
    public bool polarisCalibrated; //if this marker was also calibrated with the polaris
    public Matrix4x4 matrix;
    public Quaternion rotPMTold; //used to convert the polaris coordinates in unity coordinates
    public Vector3 posPMTold; //used to convert the polaris coordinates in untiy coordinates
    public Vector3 polarisPos; //position offset calibrated with polaris
    public Quaternion polarisRot; //rotation offset calibrated with polaris
    public string id; //name of the marker this script is attached to

    //Durch Kalibrierung mit der Polaris werden die Werte für die relative Position und relative Rotation gesetzt
    public TargetData(Vector3 relativePos, Quaternion relativeRot, int angleX, int angleY, int angleZ,
        bool isVisible, bool isExtendedTracked, bool calibrated, bool initialCalibration, bool polarisCalibrated,
        Matrix4x4 matrix, Vector3 posPMTold, Quaternion rotPMTold, Vector3 polarisPos, Quaternion polarisRot, string id)
    {
        this.relativePos = relativePos;
        this.relativeRot = relativeRot;
        this.isVisible = isVisible;
        this.isExtendedTracked = isExtendedTracked;
        this.calibrated = calibrated;
        this.initialCalibration = initialCalibration;
        this.polarisCalibrated = polarisCalibrated;
        this.angleX = angleX;
        this.angleY = angleY;
        this.angleZ = angleZ;
        this.matrix = matrix;
        this.posPMTold = posPMTold;
        this.rotPMTold = rotPMTold;
        this.polarisPos = polarisPos;
        this.polarisRot = polarisRot;
        this.id = id;
    }

}
