using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//rechnet die polaris-koordinaten, die in den targetdatas gespeichert sind in unity-koordinaten um
public class Polaris : MonoBehaviour {

    private List<TargetData> targetDatas;


	// Use this for initialization
	void Start () {
        targetDatas = GameObject.Find("TargetManager").GetComponent<TargetManager>().targetDatas;
        
        var phantomPos = new Vector3 (181.573007f,	49.172991f,	-2322.327383f);
		var phantomRot = new Quaternion (0.186428f, -0.771821f,	-0.461278f,	0.395929f);

		GameObject polarisMarker = new GameObject ("polarisMarker");
		GameObject polarisPhantom = new GameObject ("polarisPhantom");
		GameObject polarisVuforiaMarker = new GameObject ("polarisVuforiaMarker");
		polarisMarker.transform.SetParent (transform,false);
		polarisPhantom.transform.SetParent (polarisMarker.transform,false);
		polarisVuforiaMarker.transform.SetParent (polarisMarker.transform,false);

		GameObject tmpWorldSpaceVuforiaMarker = new GameObject("tmpWorldSpaceVuforiaMarker");
		GameObject tmpWorldSpacePhantom = new GameObject("tmpWorldSpacePhantom");


        Debug.Log("Polaris Start");
        foreach (var td in targetDatas)
        {
            var markerPos = td.posPMTold;
            var markerRot = td.rotPMTold;

            polarisVuforiaMarker.transform.localPosition = markerPos;
            polarisVuforiaMarker.transform.localRotation = markerRot;
            polarisPhantom.transform.localPosition = phantomPos;
            polarisPhantom.transform.localRotation = phantomRot;

            var p = polarisVuforiaMarker.transform.InverseTransformPoint(polarisPhantom.transform.position);
            var r = Quaternion.Inverse(polarisVuforiaMarker.transform.rotation) * polarisPhantom.transform.rotation;

            tmpWorldSpaceVuforiaMarker.transform.position = polarisVuforiaMarker.transform.position;
            tmpWorldSpaceVuforiaMarker.transform.rotation = polarisVuforiaMarker.transform.rotation * Quaternion.AngleAxis(90f, new Vector3(1, 0, 0));
            tmpWorldSpacePhantom.transform.position = polarisPhantom.transform.position;
            tmpWorldSpacePhantom.transform.rotation = polarisPhantom.transform.rotation * Quaternion.AngleAxis(180f, new Vector3(0, 0, 1));

            var relP = tmpWorldSpaceVuforiaMarker.transform.InverseTransformPoint(tmpWorldSpacePhantom.transform.position);
            var relR = Quaternion.Inverse(tmpWorldSpaceVuforiaMarker.transform.rotation) * tmpWorldSpacePhantom.transform.rotation;

            td.polarisPos = relP;
            td.polarisRot = relR;
            Debug.Log("relP " + relP);
            Debug.Log("relR " + relR);
            Debug.Log(td.id + " " + td.relativeRot);
            Debug.Log(td.id + " " + td.relativePos);
        }
		
	}
}
