using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DataPersistenceManager : MonoBehaviour
{
    [Header("File Storage Config")]
    [SerializeField] private string _fileName;


    GameData _gameData;

    FileDataHandler _dataHandler;

    List<IDataPersistence> _dataPersistenceObjects;

    public static DataPersistenceManager instance { get; private set; }

    private void Awake()
    {
        if (instance != null)
        {
            Debug.Log("Found more than one Persistence Manage in the scene");
        }
        instance = null;
    }

    private void Start()
    {
        Debug.Log(Application.persistentDataPath);
        this._dataHandler = new FileDataHandler(Application.persistentDataPath, _fileName);
        this._dataPersistenceObjects = FindAllDataPersistenceObjects();
        LoadGame();
    }

    public void NewGame()
    {
        this._gameData = new GameData();
    }

    public void LoadGame()
    {
        //Load saved data from file using Data Handler
        this._gameData = _dataHandler.Load();

        //If no data saved, load new save
        if (this._gameData == null)
        {
            Debug.Log("No data was found. Initializing new save file");
            NewGame();
        }

        //Load Data on the other scripts
        foreach (IDataPersistence dataPersistenceObj in _dataPersistenceObjects)
        {
            dataPersistenceObj.LoadData(_gameData);
        }
    }

    public void SaveGame()
    {
        //Pass data to other scripts to update
        foreach (IDataPersistence dataPersistenceObj in _dataPersistenceObjects)
        {
            dataPersistenceObj.SaveData(ref _gameData);
        }

        //Save data using Data Handler
        _dataHandler.Save(_gameData);
    }

    void OnApplicationQuit()
    {
        SaveGame();
    }

    List<IDataPersistence> FindAllDataPersistenceObjects()
    {
        IEnumerable<IDataPersistence> _dataPersistenceObjects = FindObjectsOfType<MonoBehaviour>().OfType<IDataPersistence>();

        return new List<IDataPersistence>(_dataPersistenceObjects);
    }

}
