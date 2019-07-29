using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelData : MonoBehaviour
{
    public Vector3 posTempModel;
    public Quaternion rotTempModel;

    public ModelData(Vector3 posTempModel, Quaternion rotTempModel)
    {
        this.posTempModel = posTempModel;
        this.rotTempModel = rotTempModel;
    }


}
