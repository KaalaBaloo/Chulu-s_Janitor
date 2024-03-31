using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class FileDataHandler
{
    string _dataDirPath = "";
    string _dataFileName = "";

    public FileDataHandler(string dataDirPath, string dataFileName) 
    {
        this._dataDirPath = dataDirPath;
        this._dataFileName = dataFileName;
    }

    public GameData Load()
    {
        //Use Path.Combine for different OS
        string fullPath = Path.Combine(_dataDirPath, _dataFileName);
        Debug.Log("Loading data");

        GameData loadedData = null;
        if(File.Exists(fullPath))
        {
            try
            {
                //Load serialized data from file
                string dataToLoad = "";
                using (FileStream stream = new FileStream(fullPath, FileMode.Open))
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        dataToLoad = reader.ReadToEnd();
                    }
                }

                //deserialize the data from Json to C#
                loadedData = JsonUtility.FromJson<GameData>(dataToLoad);

            }
            catch (Exception e)
            {
                Debug.LogError("Error ocurred when trying to load data from file: " + fullPath + "\n" + e);
            }
        }
        return loadedData;
    }

    public void Save(GameData data)
    {
        //Use Path.Combine for different OS
        string fullPath = Path.Combine(_dataDirPath, _dataFileName);
        Debug.Log("Saving data");

        try
        {
            //Create directory path if not exist
            Directory.CreateDirectory(Path.GetDirectoryName(fullPath));

            //Serialize the C# into JSON
            string dataToStore = JsonUtility.ToJson(data, true);

            //Write the file
            using (FileStream stream = new FileStream(fullPath, FileMode.Create))
            {
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    writer.Write(dataToStore);
                }
            }
        }
        catch (Exception e)
        {
            Debug.LogError("Error ocurred when trying to save data to file: " + fullPath + "\n" + e);
        }

    }

}
