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
    public GameObject calibratedBorder;


    // Use this for initialization
    void Start()
    {
        model = GameObject.Find("Box");
        modelParent = GameObject.Find("ModelParent");
        targetManagerTargets = GameObject.Find("TargetManager").GetComponent<TargetManager>().targets;
        targetDatas = GameObject.Find("TargetManager").GetComponent<TargetManager>().targetDatas;
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

        this.transform.position = modelParent.transform.position;
        this.transform.rotation = modelParent.transform.rotation;
    }

    public void SetModelAndParent(GameObject modelObject, GameObject modelParentObject)
    {
        model = modelObject;
        modelParent = modelParentObject;
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
                if (!t.calibrated)
                {
                    t.calibrated = true;
                    var go = Instantiate(calibratedBorder, t.transform.position, t.transform.localRotation);
                    go.transform.parent = t.transform;
                    StartCoroutine(RotateMe(Vector3.up * 90, 0.5f, go));


                }
            }
            if (t.initialCalibration)
            {
                t.initialCalibration = false;
            }
        }
        model.transform.localPosition = Vector3.zero;
        model.transform.localRotation = Quaternion.identity;
        GameObject.Find("XAxis").GetComponent<Renderer>().enabled = true;
        GameObject.Find("YAxis").GetComponent<Renderer>().enabled = true;
        GameObject.Find("ZAxis").GetComponent<Renderer>().enabled = true;
    }


    IEnumerator RotateMe(Vector3 byAngles, float inTime, GameObject go)
    {
        var fromAngle = go.transform.localRotation;
        var toAngle = Quaternion.Euler(go.transform.localEulerAngles + byAngles);
        for (var t = 0f; t < 1; t += Time.deltaTime / inTime)
        {
            go.transform.localRotation = Quaternion.Lerp(fromAngle, toAngle, t);
            yield return null;
        }
    }





}

