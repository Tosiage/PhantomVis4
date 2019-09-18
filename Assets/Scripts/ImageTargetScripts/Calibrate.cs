using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//this is where the calibration magic happens

public class Calibrate : MonoBehaviour
{
    public List<TargetData> targetDatas;
    public GameObject[] targetManagerTargets;
    TargetData td;
    public GameObject model;
    public GameObject modelParent;
    public bool calibrationStep1;
    public GameObject calibrationPosition;
    public TargetManager tm;


   
    void Start()
    {
        model = GameObject.Find("Box");
        modelParent = GameObject.Find("ModelParent");
        calibrationPosition = GameObject.Find("CalibrationPosition");
        targetManagerTargets = GameObject.Find("TargetManager").GetComponent<TargetManager>().targets;
        targetDatas = GameObject.Find("TargetManager").GetComponent<TargetManager>().targetDatas;
        tm = GameObject.Find("TargetManager").GetComponent<TargetManager>();
        td = GameObject.Find("CalibrationTarget").GetComponent<TargetData>();
        calibrationStep1 = true;
        foreach (TargetData t in targetDatas)
        {
            t.initialCalibration = false;
            t.calibrated = false;
            t.relativePos = Vector3.zero;
            t.relativeRot = Quaternion.identity;
        }
    }

    private void Update()
    {
        if (td.isVisible && calibrationStep1)
        {
            PositionPhantomWithInitialMarker();
        }
        if(model.name == "Box")
        {
            this.transform.position = modelParent.transform.position; //position where the calibration gameobject is put
            this.transform.rotation = modelParent.transform.rotation;
        }
        else
        {
            this.transform.position = calibrationPosition.transform.position; //position where the calibration gameobject is put when the model used is the phantom
            this.transform.rotation = calibrationPosition.transform.rotation;
        }
        
    }

    //sets the correct model and parent (because the user can choose between box and phantom)
    public void SetModelAndParent(GameObject modelObject, GameObject modelParentObject)
    {
        model = modelObject;
        modelParent = modelParentObject;
    }

    //step1 is the part where the user can position the hologram with the help of one marker that is not attached to the phantom/box
    //if the start calibration button is pushed, finishstep1 is called
    //for the currently visible targets is the initial calibration set to true
    //these targets are now used for positioning of hologram instead of the not attached marker from the beginning
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

    //used for positioning of the hologram
    //initial marker is a marker that is not attached to the phantom or box
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

    //saves the offset between the hologram and the markers that where visible when the calibration is started (finishstep1)
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
    //was used for a different approach where every target has its own tempoffset
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
    
    //saves the offset between hologram and target for each currently visible target
    //saves said offset in the targetData script for each target
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
                tm.CreateNewBorder(t);
            }
            if (t.initialCalibration)
            {
                t.initialCalibration = false; //if the marker is one of the markers that where visible in finishstep1, set them to calibrated and set initalcalibration to false
            }
        }
        model.transform.localPosition = Vector3.zero;
        model.transform.localRotation = Quaternion.identity;
        GameObject.Find("XAxis").GetComponent<Renderer>().enabled = true;
        GameObject.Find("YAxis").GetComponent<Renderer>().enabled = true;
        GameObject.Find("ZAxis").GetComponent<Renderer>().enabled = true;
    }


 





}

