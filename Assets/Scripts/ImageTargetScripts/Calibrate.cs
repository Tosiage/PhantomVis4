using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Calibrate : MonoBehaviour
{
    public List<TargetData> targetDatas;
    public GameObject[] targetManagerTargets;
    TargetData td;
    public GameObject model;
    public GameObject modelParent;
    public bool calibrationStep1;
   // ModelData md;


    // Use this for initialization
    void Start()
    {
        targetManagerTargets = GameObject.Find("TargetManager").GetComponent<TargetManager>().targets;
        targetDatas = GameObject.Find("TargetManager").GetComponent<TargetManager>().targetDatas;
        td = GameObject.Find("CalibrationTarget").GetComponent<TargetData>();
      //  md = model.GetComponent<ModelData>();
        calibrationStep1 = true;
        foreach (TargetData t in targetDatas)
        {
            t.initialCalibration = false;
            t.calibrated = false;
            t.relativePos = Vector3.zero;
            // t.relativePosTemp = Vector3.zero;
            t.relativeRot = Quaternion.identity;
            //t.relativeRotTemp = Quaternion.identity;
        }
      //  md.posTempModel = Vector3.zero;
        //md.rotTempModel = Quaternion.identity;
    }

    private void Update()
    {
        if (td.isVisible && calibrationStep1)
        {
            PositionPhantomWithInitialMarker();
        }

        this.transform.position = modelParent.transform.position;
        this.transform.rotation = modelParent.transform.rotation;
    }

    public void FinishStep1()
    {
        calibrationStep1 = false;
        modelParent.GetComponent<ModelPositionUpdater>().enabled = true;

        foreach (TargetData t in targetDatas)
        {
            if (t.isVisible)
            {
                t.initialCalibration = true;
                SaveOffsetInitial(t);

            }
        }

    }

    private void PositionPhantomWithInitialMarker()
    {
        var currentTransform = td.transform;
        Quaternion targetRot = Quaternion.Euler(td.angleX, td.angleY, td.angleZ);
        Matrix4x4 Mrot = Matrix4x4.Rotate(currentTransform.rotation * targetRot);
        Matrix4x4 Mtra = Matrix4x4.Translate(currentTransform.position);
        Vector3 posfinal = (Vector3)(Mtra * Mrot * new Vector4(td.relativePos.x, td.relativePos.y, td.relativePos.z, 1));
        modelParent.transform.position = posfinal;
        modelParent.transform.rotation = currentTransform.rotation * targetRot;
    }

    public void SaveOffsetInitial(TargetData t)
    {
        //vector from target position to model
        Vector3 distance = model.transform.position - t.transform.position;

        //InverseTransformDirection transforms direction from world space into local space, unaffected by scale
        Vector3 relativePosition = t.transform.InverseTransformDirection(distance);
        t.relativePos = relativePosition;
        Quaternion offsetRot = Quaternion.Inverse(t.transform.rotation) * model.transform.rotation;
        t.relativeRot = offsetRot;
        t.initialCalibration = true;
    }

    //Vektor zeigt von Target zu Model
    public void SaveToTemp()
    {
        /*
        foreach (TargetData t in targetDatas)
        {
            if (t.isVisible)
            {

                Vector3 distance = model.transform.position - t.transform.position;
                Vector3 relativePosition = t.transform.InverseTransformDirection(distance);
                t.relativePosTemp = relativePosition - t.relativePos;
                Quaternion offsetRot = Quaternion.Inverse(t.transform.rotation) * modelParent.transform.rotation;
                t.relativeRotTemp =Quaternion.Inverse(t.relativeRot) * offsetRot;
                t.initialCalibration = false;
                //Debug.Log(t.transform.name + " t.relativePos = " + t.relativePos);
                //Debug.Log(t.transform.name + " t.relativePosTemp = " + t.relativePosTemp);
                //Debug.Log(t.transform.name + " t.relativeRot = " + t.relativeRot);
                //Debug.Log(t.transform.name + " t.relativeRotTemp = " + t.relativeRotTemp);

            }
        }*/
        //md.posTempModel = model.transform.InverseTransformDirection(model.transform.position - modelParent.transform.position);
        // md.posTempModel = model.transform.position - modelParent.transform.position;
        //




    }

    public void SaveOffset()
    {
        foreach (TargetData t in targetDatas)
        {

            if (t.isVisible)
            {
                //vector from target position to model
                Vector3 distance = model.transform.position - t.transform.position;
                //InverseTransformDirection transforms direction from world space into local space, unaffected by scale
                Vector3 relativePosition = t.transform.InverseTransformDirection(distance);
                t.relativePos = relativePosition;
                Quaternion offsetRot = Quaternion.Inverse(t.transform.rotation) * model.transform.rotation;
                t.relativeRot = offsetRot;
                t.calibrated = true;
                /*
                t.calibrated = true;
                t.relativePos = t.relativePos + t.relativePosTemp;
                t.relativeRot = t.relativeRot * t.relativeRotTemp;*/

            }
            if (t.initialCalibration)
            {
                t.initialCalibration = false;
            }
            //t.relativePosTemp = Vector3.zero;
            //t.relativeRotTemp = Quaternion.identity;
        }
        model.transform.localPosition = Vector3.zero;
        model.transform.localRotation = Quaternion.identity;
      //  md.posTempModel = Vector3.zero;
       // md.rotTempModel = Quaternion.identity;
    }





}

