using System.Collections;
using TMPro;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System.Threading;

public class GridController : MonoBehaviour, IDataPersistence
{
    static public bool GAMEOVER = false;
    static public int LEVELS_UNLOCKED = 0;

    [SerializeField] int _dirtToClean = 0;
    [SerializeField] int _tilesX = 3;
    [SerializeField] int _tilesY = 3;
    [SerializeField] GameObject _border;
    TMP_Text _textBloodLeft;
    GameObject _fadeBlack;
    [SerializeField] int _turn = 2; 
    //0 --> Player
    //1 --> Enemies
    //2 --> Waiting

    [SerializeField] int _enemies = 0;
    [SerializeField] int _enemiesCount = 1;
    [SerializeField] bool _ritualOn = false;
    [SerializeField] bool _endOn = false;
    bool _loadingScreen = false;
    float _time = 0;

    AudioSource _audio;
    [SerializeField] AudioClip _win;
    [SerializeField] AudioClip _gameOver;

    int[,] _gridBase;
    int[,] _gridInteractive;

    //GRID BASE
    // 0 --> Libre
    // 1 --> Player
    // 2 --> Bloqueo
    // 3 --> Enemigo

    //GRID INTERACTUABLES
    // 0 --> Nada
    // 1 --> Suciedad
    // 2 --> Pinchos
    // 3 --> TP
    // 4 --> Mancha grande
    // 5 --> Cubo
    // 6 --> Sangre Ritual Boss


    private void Awake()
    {
        _gridBase = new int[_tilesX, _tilesY];
        _gridInteractive = new int[_tilesX, _tilesY];

        InitializeGrids();
       SetBordersGrid();
       _turn = 2;

        _textBloodLeft = GameObject.FindWithTag("_textBlood").GetComponent<TMP_Text>();
        _fadeBlack = GameObject.FindWithTag("_blackFade");
    }

    private void Start()
    {
        StartCoroutine(FadefromBlack());
        GAMEOVER = false;
        _loadingScreen = true;
        _textBloodLeft.text = _dirtToClean.ToString();
        Cursor.visible = false;
        _audio = GetComponent<AudioSource>();
        _audio.volume = GeneralSettings.SFXVOLUME / 100;
    }

    void Update()
    {
        if (_loadingScreen)
        {
            _time += Time.deltaTime;
            LoadingScreen(1);
            _textBloodLeft.text = _dirtToClean.ToString();
        }
        if (_dirtToClean <= 0 && SceneManager.GetActiveScene().name != "20" && SceneManager.GetActiveScene().name != "20_Battle" && !GAMEOVER)
        {
            int level;

            GAMEOVER = true;
            Debug.Log("Win");
            AudioPlay(_win);

            int.TryParse(SceneManager.GetActiveScene().name, out level);
            if (level > LEVELS_UNLOCKED)
                LEVELS_UNLOCKED++;

            StartCoroutine(ChangeScene("LevelSelector"));
        }
        else if (_dirtToClean <= 0 && SceneManager.GetActiveScene().name == "20" && !GAMEOVER)
        {
            int level;

            GAMEOVER = true;
            Debug.Log("Win");
            AudioPlay(_win);

            int.TryParse(SceneManager.GetActiveScene().name, out level);
            if (level > LEVELS_UNLOCKED)
                LEVELS_UNLOCKED++;

            StartCoroutine(RitualAnimation());
        }
        else if (_dirtToClean <= 0 && SceneManager.GetActiveScene().name == "20_Battle" && !GAMEOVER)
        {
            GAMEOVER = true;
            Debug.Log("Win");
            AudioPlay(_win);
            StartCoroutine(EndAnimation());
        }
        if (_enemies == _enemiesCount && _turn == 1)
        {
            _enemiesCount = 0;
            _turn = 0;
        }
    }

    protected IEnumerator RitualAnimation()
    {
        float t = 0;
        while (t < 10) 
        {
            if (t > 1)
                _ritualOn = true;
            t += Time.deltaTime;
            yield return null;
        }
        StartCoroutine(FadetoBlack("20_Battle"));
        yield return null;
    }

    protected IEnumerator EndAnimation()
    {
        Transform _camara = GameObject.FindWithTag("MainCamera").transform;
        float t = 0;
        bool left = true;

        while (t < 2)
        {
            if (t > 1)
                _endOn = true;
            t += Time.deltaTime;
            yield return null;
        }

        while (t < 5)
        {
            if (left)
                _camara.transform.position -= new Vector3( 10, 0, 0) * Time.deltaTime;
            else
                _camara.transform.position += new Vector3( 10, 0, 0) * Time.deltaTime;

            if (_camara.transform.position.x <= 5f)
                left = false;
            else if (_camara.transform.position.x >= 6f)
                left = true;

            t += Time.deltaTime;
            yield return null;
        }

        StartCoroutine(FadetoBlack("Comic_2"));
        yield return null;
    }

    public int GetDirt()
    {
        return _dirtToClean;
    }

    public bool GetRitual()
    {
        return _ritualOn;
    }

    public bool GetEnd()
    {
        return _endOn;
    }

    //Inicializa las grids de control general y la de control de "basura" que hay que limpiar
    private void InitializeGrids()
    {
        for (int x = 0; x < _tilesX; x++)
        {
            for (int y = 0; y < _tilesY; y++)
            {
                _gridBase[x, y] = 0;
            }
        }

        for (int x = 0; x < _tilesX; x++)
        {
            for (int y = 0; y < _tilesY; y++)
            {
                _gridInteractive[x, y] = 0;
            }
        }
    }

    //Genera los bordes del tablero/grid
    private void SetBordersGrid()
    {
        for (int x = 0; x < _tilesX; x++)
        {
            for (int y = 0; y < _tilesY; y++)
            {
                if (x == 0 || x == _tilesX - 1 || y == 0 || y == _tilesY - 1)
                {
                    _gridBase[x, y] = 2;
                    Instantiate(_border, new Vector3(x, y, 0), Quaternion.identity);
                }
            }
        }
    }

    //Cambia alg�n dato de la grid de control general
    public void SetGrid(int sprite, int num_x, int num_y)
    {
        _gridBase[num_x, num_y] = sprite;
    }

    //Cambia alg�n dato de la grid de control de "basura" que hay que limpiar
    public void SetInteractive(int interactive, int num_x, int num_y)
    {
        _gridInteractive[num_x, num_y] = interactive;
    }

    //Devuelve si es posible posicionarse en la celda elegida
    public bool CanMove(int num_x, int num_y)
    {
        try {
            if (_gridBase[num_x, num_y] == 0)
            {
                return true;
            }
            else
            { 
                return false;
            }
        }
        catch
        {
            return false;
        }
        
    }
    public bool EnemyCanMove(int num_x, int num_y)
    {
        try
        {
            if (_gridInteractive[num_x, num_y] == 2 || (_gridBase[num_x, num_y] != 0 && _gridBase[num_x, num_y] != 1))
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        catch
        {
            return false;
        }

    }

    //Devuelve que hay interactuable en la celda elegida
    public int GetGridTile(int num_x, int num_y)
    {
        try
        {
            return _gridBase[num_x, num_y];
        }
        catch
        {
            return 2;
        }
    }

    //Devuelve si hay algo que limpiar en la celda elegida
    public int CanClean(int num_x, int num_y)
    {
        if (_gridInteractive[num_x, num_y] == 1)
        {
            return 1;
        }
        else if (_gridInteractive[num_x, num_y] == 4)
        {
            return 2;
        }
        else if (_gridInteractive[num_x, num_y] == 5)
        {
            return 3;
        }
        else if (_gridInteractive[num_x, num_y] == 6)
        {
            return 4;
        }
        else
        {
            return 0;
        }
    }

    //Resta 1 a la "basura" restante
    public void DirtCleaned()
    {
        if (_dirtToClean > 0)
        {
            _dirtToClean -= 1;
            UpdateBlood();
        }
        else
            Debug.Log("Error, suciedad = 0");
    }

    public void AddDirt()
    {
        _dirtToClean++;
        UpdateBlood();
    }

    //Devuelve el booleano de qui�n es el turno
    public int GetTurn()
    {
        return _turn;
    }

    //Cambia el turno
    public void ChangeTurn(int turn)
    {
        _turn = turn;
    }

    void LoadingScreen(int seconds)
    {
        if (_time >= seconds)
        {
            _loadingScreen = false;
            Debug.Log("Finished Loading");
            _turn = 0;
        } 
    }

    public bool GetPlayerVertical(int x)
    {
        for (int i = 0; i < _tilesY; i++)
        {
            if (_gridBase[x, i] == 1)
                return true;
        }
        return false;
    }

    public bool GetPlayerHorizontal(int y)
    {
        for (int i = 0; i < _tilesX; i++)
        {
            if (_gridBase[i, y] == 1)
                return true;
        }
        return false;
    }

    public bool GetPlayerVerticalNoObstacle(int x, int y)
    {
        for (int i = y; i < _tilesY; i++)
        {
            if (_gridBase[x, i] == 1)
                return true;
            if (_gridBase[x, i] == 2)
                break;
        }
        for (int i = y; i > 0; i--)
        {
            if (_gridBase[x, i] == 1)
                return true;
            if (_gridBase[x, i] == 2)
                break;
        }
        return false;
    }

    public bool GetPlayerHorizontalNoObstacle(int x, int y)
    {
        for (int i = x; i < _tilesX; i++)
        {
            if (_gridBase[i, y] == 1)
                return true;
            if (_gridBase[i, y] == 2)
                break;
        }
        for (int i = x; i > 0; i--)
        {
            if (_gridBase[i, y] == 1)
                return true;
            if (_gridBase[i, y] == 2)
                break;
        }
        return false;
    }

    public bool GetPlayerNear(int x, int y)
    {
        if (_gridBase[x-1, y-1] == 1 || _gridBase[x+1, y+1] == 1 || _gridBase[x-1, y+1] == 1 || _gridBase[x+1, y-1] == 1)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void CreateEnemy()
    {
        _enemies++;
    }

    public void EnemyMoved()
    {
        _enemiesCount++;
        ChangeTurn(1);
    }

    public int GetEnemyMoved()
    {
        return _enemiesCount;
    }

    public void UpdateBlood()
    {
        _textBloodLeft.text = _dirtToClean.ToString();
    }

    public void GameOver()
    {
        Debug.Log("GameOver");
        AudioPlay(_gameOver);
        StartCoroutine(ChangeScene(SceneManager.GetActiveScene().name));
    }

    protected IEnumerator ChangeScene(string scene)
    {
        float t = 0;
        while (t < 1)
        {
            t += Time.deltaTime;
            yield return null;
        }
        GameObject.FindGameObjectWithTag("_save").GetComponent<DataPersistenceManager>().SaveGame();
        StartCoroutine(FadetoBlack(scene));
        yield return null;
    }

    protected IEnumerator FadetoBlack(string scene, int fadeSpeed = 5)
    {
        Color color = _fadeBlack.GetComponent<SpriteRenderer>().color;
        float fadeAmount;

        while (_fadeBlack.GetComponent<SpriteRenderer>().color.a < 1)
        {
            fadeAmount = color.a + (fadeSpeed * Time.deltaTime);
            color = new Color(color.r, color.g, color.b, fadeAmount);
            _fadeBlack.GetComponent<SpriteRenderer>().color = color;
            yield return null;
        }

        SceneManager.LoadScene(scene);
        yield return null;
    }

    protected IEnumerator FadefromBlack(int fadeSpeed = 8)
    {
        Color color = _fadeBlack.GetComponent<SpriteRenderer>().color;
        float fadeAmount;

        while (_fadeBlack.GetComponent<SpriteRenderer>().color.a > 0)
        {
            fadeAmount = color.a - (fadeSpeed * Time.deltaTime);
            color = new Color(color.r, color.g, color.b, fadeAmount);
            _fadeBlack.GetComponent<SpriteRenderer>().color = color;
            yield return null;
        }
    }

    void AudioPlay(AudioClip name)
    {
        _audio.volume = GeneralSettings.SFXVOLUME / 100;
        if(!GeneralSettings.MUTED)
        {
            _audio.clip = name;
            _audio.Play();
        }
    }

    public void LoadData(GameData data)
    {
        LEVELS_UNLOCKED = data._levelsUnlocked;
        GeneralSettings.MUSICVOLUME = data._musicVolume;
        GeneralSettings.SFXVOLUME = data._sfxVolume;
        GeneralSettings.FULLSCREEN = data._fullscreen;
        GeneralSettings.MUTED = data._muted;
    }

    public void SaveData(ref GameData data)
    {
        data._levelsUnlocked = LEVELS_UNLOCKED;
        data._musicVolume = GeneralSettings.MUSICVOLUME;
        data._sfxVolume = GeneralSettings.SFXVOLUME;
        data._fullscreen = GeneralSettings.FULLSCREEN;
        data._muted = GeneralSettings.MUTED;
    }
}
