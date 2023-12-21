using System.Collections;
using TMPro;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class GridController : MonoBehaviour
{
    [SerializeField] int _dirtToClean = 0;
    [SerializeField] int _tilesX = 3;
    [SerializeField] int _tilesY = 3;
    [SerializeField] GameObject _border;
    [SerializeField] GameObject _BloodLeft;
    TMP_Text _textBloodLeft;
    [SerializeField] int _turn = 2; 
    //0 --> Player
    //1 --> Enemies
    //2 --> Waiting

    [SerializeField] int _enemies = 0;
    [SerializeField] int _enemiesCount = 1;
    bool _loadingScreen = false;
    float _time = 0;

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


    private void Awake()
    {
        _gridBase = new int[_tilesX, _tilesY];
        _gridInteractive = new int[_tilesX, _tilesY];

        InitializeGrids();
       SetBordersGrid();
       _turn = 2;

        _textBloodLeft = _BloodLeft.GetComponent<TMP_Text>();
    }

    private void Start()
    {
        _loadingScreen = true;
        _textBloodLeft.text = _dirtToClean.ToString();
    }

    private void Update()
    {


        if (_loadingScreen)
        {
            _time += Time.deltaTime;
            LoadingScreen(1);
            _textBloodLeft.text = _dirtToClean.ToString();
        }
        if (_dirtToClean <= 0)
        {
            Debug.Log("Win");
            SceneManager.LoadScene("Main");
        }

        if (_enemies == _enemiesCount && _turn == 1)
        {
            _enemiesCount = 0;
            _turn = 0;
        }
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

    //Cambia algún dato de la grid de control general
    public void SetGrid(int sprite, int num_x, int num_y)
    {
        _gridBase[num_x, num_y] = sprite;
    }

    //Cambia algún dato de la grid de control de "basura" que hay que limpiar
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

    //Devuelve el booleano de quién es el turno
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
        for (int i = y; i < _tilesY - 1; i++)
        {
            if (_gridBase[x, i] == 1)
                return true;
            if (_gridBase[x, i] == 2)
                return false;
        }
        for (int i = y; i > 0; i--)
        {
            if (_gridBase[x, i] == 1)
                return true;
            if (_gridBase[x, i] == 2)
                return false;
        }
        return false;
    }

    public bool GetPlayerHorizontalNoObstacle(int x, int y)
    {
        for (int i = x; i < _tilesX - 1; i++)
        {
            if (_gridBase[i, y] == 1)
            {
                return true;
            }
            if (_gridBase[i, y] == 2)
            {
                return false;
            } 
        }
        for (int i = x; i > 0; i--)
        {
            if (_gridBase[i, y] == 1)
            {
                return true;
            }
            if (_gridBase[i, y] == 2)
            {
                return false;
            }
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
    }

    public int GetEnemyMoved()
    {
        return _enemiesCount;
    }

    public void UpdateBlood()
    {
        _textBloodLeft.text = _dirtToClean.ToString();
    }

    public void Restart()
    {
        Debug.Log("GameOver");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
