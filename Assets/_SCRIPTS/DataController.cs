using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DataController : MonoBehaviour
{
    private Informations informations = new Informations();

    private string dataFileName = "/StreamingAssets/informacoes.json";
    public Informations LoadInformations()
    {
        //string filePath = Application.dataPath + dataFileName;

        //if (File.Exists(filePath))
        //{
        //    // Read the json from the file into a string
        //    string dataAsJson = File.ReadAllText(filePath);
        //    // Pass the json to JsonUtility, and tell it to create a GameData object from it
        //    informations = JsonUtility.FromJson<Informations>(dataAsJson);
        //}
        //else
        //{
        //    SaveInformations();
        //}

        var jsonTextFile = Resources.Load<TextAsset>("informacoes");
        informations = JsonUtility.FromJson<Informations>(jsonTextFile.text);

        return informations;
    }

    public void SaveInformations()
    {
        string dataAsJson = JsonUtility.ToJson(informations, true);
        string filePath = Application.dataPath + dataFileName;
        File.WriteAllText(filePath, dataAsJson);
    }
}
