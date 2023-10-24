using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridController : MonoBehaviour
{
    [SerializeField] int _dirtToClean = 3;
    [SerializeField] int _movValue = 2;
    [SerializeField] int _tilesX = 3;
    [SerializeField] int _tilesY = 3;
    [SerializeField] GameObject _border;
    AstarPath pathfinding;
    [SerializeField] int _playerTurn = 2;
    //0 --> Enemigo
    //1 --> Player
    //2 --> Cargando

    int[,] _gridBase;
    int[,] _gridToClean;

    //GRID BASE
    // 0 --> Libre
    // 1 --> Player
    // 2 --> Bloqueo
    // 3 --> Enemigo

    //GRID SUCIEDAD
    // 0 --> Limpio
    // 1 --> Suciedad


    private void Awake()
    {
        _gridBase = new int[_tilesY, _tilesX];
        _gridToClean = new int[_tilesY, _tilesX];
        pathfinding = GetComponent<AstarPath>();

       InitializeGrids();
       SetBordersGrid();
       pathfinding.Scan();

    }

    private void Start()
    {
        StartCoroutine(LoadingScreen());
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
                _gridToClean[x, y] = 0;
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
    public void SetDirtOnGrid(int dirt, int num_x, int num_y)
    {
        _gridToClean[num_x, num_y] = dirt;
    }

    //Devuelve si es posible posicionarse en la celda elegida
    public bool CanMove(int num_x, int num_y)
    {
        if (_gridBase[num_x, num_y] == 0)
        {
            return true;
        }
        else
        {
            Debug.Log(_gridBase[num_x, num_y]);
            return false;
        }
    }

    //Devuelve si hay algo que limpiar en la celda elegida
    public bool CanClean(int num_x, int num_y)
    {
        if (_gridToClean[num_x, num_y] == 1)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    //Devuelve el número de movimiento de una celda
    public int GetMovValue()
    { return _movValue;  }

    //Devuelve el número de "basura" que queda por limpiar
    public int GetDirtToClean()
    { return _dirtToClean; }

    //Resta 1 a la "basura" restante
    public void DirtCleaned()
    {
        if (_dirtToClean > 0)
            _dirtToClean -= 1;
        else
            Debug.Log("Error, suciedad = 0");
    }

    //Devuelve el booleano de quién es el turno
    public int GetTurn()
    {
        return _playerTurn;
    }

    //Cambia el turno
    public void ChangeTurn(int n)
    {
        _playerTurn = n;
    }

    protected IEnumerator LoadingScreen()
    {
        float seconds = 0;

        while (seconds < 2)
        {
            seconds += Time.deltaTime;
            yield return null;
        }
        _playerTurn = 1;
        yield return 0;
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

}
