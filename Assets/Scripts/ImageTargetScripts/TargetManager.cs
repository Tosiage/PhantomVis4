using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class TargetManager : MonoBehaviour
{
    public GameObject[] targets;
    [SerializeField]
    public List<TargetData> targetDatas;
    public bool atLeastOneVisible;
    public GameObject dirButton;
    public GameObject calibratedBorder;
    public List<GameObject> borders;

    // Use this for initialization
    void Awake()
    {

        atLeastOneVisible = false;
        targets = GameObject.FindGameObjectsWithTag("target");
        foreach (GameObject t in targets)
        {
            var data = t.GetComponent<TargetData>();
            targetDatas.Add(data);
        }
        borders = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        atLeastOneVisible = false;
        foreach (TargetData t in targetDatas)
        {
            if (t.isVisible)
            {
                atLeastOneVisible = true;
                return;
            }
        }
    }

    public void SaveData()
    {
        Debug.Log("SaveData");
        var pathDir = Path.Combine(Application.persistentDataPath, GetDirectories().Count.ToString());
        Directory.CreateDirectory(pathDir);

        foreach (TargetData t in targetDatas)
        {
            TargetDataSerializable targetDataSerializable = new TargetDataSerializable(t.relativePos, t.relativeRot, t.id, t.calibrated, System.DateTime.Now.ToString());
            var json = JsonUtility.ToJson(targetDataSerializable, true);
            var path = Path.Combine(pathDir, t.id + ".antonia");

            using (FileStream fs = new FileStream(path, FileMode.Create))
            {
                using (StreamWriter writer = new StreamWriter(fs, System.Text.Encoding.UTF8))
                {
                    writer.Write(json);
                }
            }
        }

    }

    public void SelectDataToLoad(Vector3 startPos, Quaternion startRot)
    {
        var dirs = GetDirectories();
        for (int i = 0; i < dirs.Count; i++)
        {
            var go = Instantiate(dirButton, startPos + new Vector3(0.2f, 0f, 0f), startRot);
            go.name = i.ToString();
            go.transform.GetChild(2).GetComponent<TextMesh>().text = i.ToString();
            go.AddComponent<JSONButtons>();
            startPos = go.transform.position;
        }
    }

    public void LoadData(string directory)
    {
        Debug.Log("LoadData");
        var pathDir = Path.Combine(Application.persistentDataPath, directory);
        if (!Directory.Exists(pathDir))
        {
            Debug.Log("Path doesn't exist :(");
            return;
        }
        for (int i = 0; i < targetDatas.Count; i++)
        {
            var path = Path.Combine(pathDir, targetDatas[i].id + ".antonia");

            using (FileStream fs = new FileStream(path, FileMode.Open))
            {
                using (StreamReader reader = new StreamReader(fs))
                {
                    var json = reader.ReadToEnd();
                    var td = JsonUtility.FromJson<TargetDataSerializable>(json);
                    targetDatas[i].relativePos = td.relativePos;
                    targetDatas[i].relativeRot = td.relativeRot;
                    targetDatas[i].id = td.id;
                    targetDatas[i].calibrated = td.calibrated;
                }
            }

        }
    }

    public List<string> GetDirectories()
    {
        var dirs = Directory.GetDirectories(Application.persistentDataPath);
        List<string> dirsList = new List<string>(dirs);
        dirsList.Remove(Path.Combine(Application.persistentDataPath, "Unity"));
        dirsList.Remove(Path.Combine(Application.persistentDataPath, "QCAR"));

        return dirsList;
    }

    //Creates a new border around a freshly calibrated target
    public void CreateNewBorder(TargetData t)
    {
        if (!t.calibrated)
        {
            t.calibrated = true;
            var go = Instantiate(calibratedBorder, t.transform.position, t.transform.localRotation);
            go.transform.parent = t.transform;
            StartCoroutine(RotateMe(Vector3.up * 90, 0.5f, go));
            borders.Add(go);

        }
    }

    //deletes all borders (that indicate if the target is calibrated) in the scene and sets calibrated to false
    public void DeleteAllBorders()
    {
        for (int i = 0; i < borders.Count; i++)
        {
            Destroy(borders[i]);
        }
        borders.Clear();
        for (int i = 0; i < targetDatas.Count; i++)
        {
            targetDatas[i].calibrated = false;
            targetDatas[i].initialCalibration = false;
        }
        
    }

    //Creates borders for all calibrated targets (this function is used when an old calibration is loaded)
    public void LoadBorders()
    {
        foreach (var t in targetDatas)
        {
            if (t.calibrated)
            {
                var go = Instantiate(calibratedBorder, t.transform.position, t.transform.localRotation);
                go.transform.parent = t.transform;
                StartCoroutine(RotateMe(Vector3.up * 90, 0.5f, go));
                borders.Add(go);
            }
        }
    }

    //rotates a freshly created border
    IEnumerator RotateMe(Vector3 byAngles, float inTime, GameObject go)
    {
        var fromAngle = go.transform.localRotation;
        var toAngle = Quaternion.Euler(go.transform.localEulerAngles + byAngles);
        for (var t = 0f; t < 1; t += Time.deltaTime / inTime)
        {
            go.transform.localRotation = Quaternion.Lerp(fromAngle, toAngle, t);
            yield return null;
        }
       // go.transform.localRotation = Quaternion.identity;
    }



}
