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



}
