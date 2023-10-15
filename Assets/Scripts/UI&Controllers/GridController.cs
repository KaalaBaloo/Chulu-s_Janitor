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
    [SerializeField] bool _playerTurn = true;

    int[,] _gridBase;
    int[,] _gridToClean;

    //GRID BASE
    // 0 --> Libre
    // 1 --> Personaje
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

    //Inicializa las grids de control general y la de control de "basura" que hay que limpiar
    private void InitializeGrids()
    {
        for (int y = 0; y < _tilesY; y++)
        {
            for (int x = 0; x < _tilesX; x++)
            {
                _gridBase[y, x] = 0;
            }
        }

        for (int y = 0; y < _tilesY; y++)
        {
            for (int x = 0; x < _tilesX; x++)
            {
                _gridToClean[y, x] = 0;
            }
        }
    }

    //Genera los bordes del tablero/grid
    private void SetBordersGrid()
    {
        for (int y = 0; y < _tilesY; y++)
        {
            for (int x = 0; x < _tilesX; x++)
            {
                if (y == 0 || y == _tilesY - 1 || x == 0 || x == _tilesX - 1)
                {
                    _gridBase[y, x] = 2;
                    Instantiate(_border, new Vector3(x, y, 0), Quaternion.identity);
                }
            }
        }
    }

    //Cambia algún dato de la grid de control general
    public void SetGrid(int sprite, int num_x, int num_y)
    {
        _gridBase[num_y, num_x] = sprite;
    }

    //Cambia algún dato de la grid de control de "basura" que hay que limpiar
    public void SetDirtOnGrid(int dirt, int num_x, int num_y)
    {
        _gridToClean[num_y, num_x] = dirt;
    }

    //Devuelve si es posible posicionarse en la celda elegida
    public bool CanMove(int num_x, int num_y)
    {
        if (_gridBase[num_y, num_x] == 0)
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
        if (_gridToClean[num_y, num_x] == 1)
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
    public bool GetTurn()
    {
        return _playerTurn;
    }

    //Cambia el turno
    public void ChangeTurn()
    {
        if (_playerTurn)
            _playerTurn = false;
        else
            _playerTurn = true;
    }

}
