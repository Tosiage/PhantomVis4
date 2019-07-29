using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildPositionUpdater : MonoBehaviour
{
    ModelData md;
    public GameObject modelParent;

    private void Start()
    {
        md = this.gameObject.GetComponent<ModelData>();


    }

    // Update is called once per frame
    void LateUpdate()
    {

        this.gameObject.transform.position = modelParent.transform.position + md.posTempModel;
        this.gameObject.transform.rotation = modelParent.transform.rotation * md.rotTempModel;

    }
}
