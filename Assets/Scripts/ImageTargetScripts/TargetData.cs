using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetData : MonoBehaviour
{
    public Vector3 relativePos;
    public Vector3 relativePosTemp;
    public Quaternion relativeRot;
    public Quaternion relativeRotTemp;
    public float angleX;
    public float angleY;
    public float angleZ;
    public bool isVisible;
    [HideInInspector] public bool isExtendedTracked;
    public bool calibrated;
    public bool initialCalibration;
    public Matrix4x4 matrix;
    public Quaternion rotPMTold;
    public Vector3 posPMTold;

    //Durch Kalibrierung mit der Polaris werden die Werte für die relative Position und relative Rotation gesetzt
    public TargetData(Vector3 relativePos, Vector3 relativePosTemp, Quaternion relativeRot, Quaternion relativeRotTemp, int angleX, int angleY, int angleZ,
        bool isVisible, bool isExtendedTracked, bool calibrated, bool initialCalibration, Matrix4x4 matrix, Vector3 posPMTold, Quaternion rotPMTold)
    {
        this.relativePos = relativePos;
        this.relativePosTemp = relativePosTemp;
        this.relativeRot = relativeRot;
        this.relativeRotTemp = relativeRotTemp;
        this.isVisible = isVisible;
        this.isExtendedTracked = isExtendedTracked;
        this.calibrated = calibrated;
        this.initialCalibration = initialCalibration;
        this.angleX = angleX;
        this.angleY = angleY;
        this.angleZ = angleZ;
        this.matrix = matrix;
        this.posPMTold = posPMTold;
        this.rotPMTold = rotPMTold;
    }

}
