using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainCharacter : Sprites 
{ 
    Rigidbody2D _rb;
    bool _destroyDirt = false;
    [SerializeField] int _lives = 1;
    [SerializeField] int _suciedad = 0;
    [SerializeField] GameObject _sprite;
    [SerializeField] GameObject _sangre;
    [SerializeField] GameObject _pentUpRight;
    [SerializeField] GameObject _pentUpLeft;
    [SerializeField] GameObject _pentDownRight;
    [SerializeField] GameObject _pentDownLeft;
    [SerializeField] GameObject _pentDown;
    Animator _animator;

    [SerializeField] GameObject _VFXClean;
    [SerializeField] GameObject _VFXDirty;
    [SerializeField] GameObject _VFXNone;
    [SerializeField] GameObject _VFXWash;

    AudioSource _audio;
    [SerializeField] AudioClip _limpiar;
    [SerializeField] AudioClip _enjuagar;

    GameObject _UI;
    UIController _uiController;

    protected override void Awake()
    {
        base.Awake();
        _rb = GetComponent<Rigidbody2D>();
        _animator = _sprite.GetComponent<Animator>();
        _audio = GetComponent<AudioSource>();
        _audio.volume = GeneralSettings.SFXVOLUME / 100;
        _UI = GameObject.FindWithTag("_ui");
        _uiController = _UI.GetComponent<UIController>();
    }

    void Start()
    {
        _spriteNumber = 1;
        _gridController.SetGrid(_spriteNumber, _tileNumX, _tileNumY);
    }

    void Update()
    {
        if(!_uiController.GetPaused())
        {
            Movement();
        }
    }

    public int GetSuciedad()
    {
        return _suciedad;
    }


    //Calcula si es posible moverse a la celda elegida y, de serlo, mueve al personaje y cambia los datos de la grid y el turno
    private void Movement()
    {
        if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))&& _gridController.CanMove(_tileNumX, _tileNumY + 1) && _gridController.GetTurn() == 0)
        {
            _gridController.ChangeTurn(2);
            StartCoroutine(PositionCoroutine(_rb, new Vector2(0, 1))); //Up
            _animator.SetTrigger("Slide");
        }
        if ((Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) && _gridController.CanMove(_tileNumX - 1, _tileNumY) && _gridController.GetTurn() == 0)
        {
            _gridController.ChangeTurn(2);
            StartCoroutine(PositionCoroutine(_rb, new Vector2(-1 , 0))); //Left
            _animator.SetTrigger("Slide");
        }
        if ((Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)) && _gridController.CanMove(_tileNumX, _tileNumY - 1) && _gridController.GetTurn() == 0)  
        {
            _gridController.ChangeTurn(2);
            StartCoroutine(PositionCoroutine(_rb, new Vector2(0, -1))); //Down
            _animator.SetTrigger("Slide");
        }
        if ((Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) && _gridController.CanMove(_tileNumX + 1, _tileNumY) && _gridController.GetTurn() == 0)
        {
            _gridController.ChangeTurn(2);
            StartCoroutine(PositionCoroutine(_rb, new Vector2(1, 0))); //Right
            _animator.SetTrigger("Slide");
        }
        if (Input.GetKeyDown(KeyCode.Space) && _gridController.GetTurn() == 0)
        {
            _gridController.ChangeTurn(2);
            StartCoroutine(CleanCoroutine());
        }
    }

    //Elimina el sprite de "basura" sobre el que está el personaje
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (_destroyDirt && collision.CompareTag("Dirt"))
        {
            _destroyDirt = false;
            Destroy(collision.gameObject); 
        }
    }

    public int GetX()
    {
        return _tileNumX;
    }

    public int GetY()
    {
        return _tileNumY;
    }

    public int GetLives()
    {
        return _lives;
    }

    public void SubstractLife(int damage)
    {
        _lives -= damage;
    }

    public void GameOver()
    {
        Destroy(gameObject);
    }

    override protected IEnumerator PositionCoroutine(Rigidbody2D rb, Vector2 position)
    {
        _gridController.SetGrid(0, _tileNumX, _tileNumY);
        _tileNumX += Mathf.RoundToInt(position.x);
        _tileNumY += Mathf.RoundToInt(position.y);
        Vector2 endingposition = new Vector2(Mathf.RoundToInt(rb.position.x + position.x), Mathf.RoundToInt(rb.position.y + position.y));
        if (transform.position.x > endingposition.x)
        {
            while (transform.position.x > endingposition.x)
            {
                rb.velocity = (endingposition - rb.position).normalized * _speed;
                yield return null;
            }
        }
        else if (transform.position.x < endingposition.x)
        {
            while (transform.position.x < endingposition.x)
            {
                rb.velocity = (endingposition - rb.position).normalized * _speed;
                yield return null;
            }
        }
        else if (transform.position.y > endingposition.y)
        {
            while (transform.position.y > endingposition.y)
            {
                rb.velocity = (endingposition - rb.position).normalized * _speed;
                yield return null;
            }
        }
        else
        {
            while (transform.position.y < endingposition.y)
            {
                rb.velocity = (endingposition - rb.position).normalized * _speed;
                yield return null;
            }
        }
        rb.velocity = Vector2.zero;
        transform.position = endingposition;
        _gridController.SetGrid(_spriteNumber, _tileNumX, _tileNumY);
        _gridController.ChangeTurn(1);
        yield return 0;
    }

    protected IEnumerator PushCoroutine(Rigidbody2D rb, Vector2 position)
    {
        _gridController.SetGrid(0, _tileNumX, _tileNumY);
        _tileNumX += Mathf.RoundToInt(position.x);
        _tileNumY += Mathf.RoundToInt(position.y);
        Vector2 endingposition = new Vector2(Mathf.RoundToInt(rb.position.x + position.x), Mathf.RoundToInt(rb.position.y + position.y));
        if (transform.position.x > endingposition.x)
        {
            while (transform.position.x > endingposition.x)
            {
                rb.velocity = (endingposition - rb.position).normalized * _speed;
                yield return null;
            }
        }
        else if (transform.position.x < endingposition.x)
        {
            while (transform.position.x < endingposition.x)
            {
                rb.velocity = (endingposition - rb.position).normalized * _speed;
                yield return null;
            }
        }
        else if (transform.position.y > endingposition.y)
        {
            while (transform.position.y > endingposition.y)
            {
                rb.velocity = (endingposition - rb.position).normalized * _speed;
                yield return null;
            }
        }
        else
        {
            while (transform.position.y < endingposition.y)
            {
                rb.velocity = (endingposition - rb.position).normalized * _speed;
                yield return null;
            }
        }
        rb.velocity = Vector2.zero;
        transform.position = endingposition;
        _gridController.SetGrid(_spriteNumber, _tileNumX, _tileNumY);
        yield return 0;
    }

    public void Push(int x, int y)
    {
        StartCoroutine(PushCoroutine(_rb, new Vector2(x, y)));
    }

    protected IEnumerator CleanCoroutine()
    {
        float t = 0;
        _animator.SetTrigger("Clean");
        if (_gridController.CanClean(_tileNumX, _tileNumY) == 1 && _suciedad < 3) // Suciedad Normal
        {
            _gridController.SetInteractive(0, _tileNumX, _tileNumY);
            AudioPlay(_limpiar);
            Instantiate(_VFXClean, transform.position, Quaternion.identity);
            _gridController.DirtCleaned();
            _destroyDirt = true;
            _suciedad++;
        }
        else if (_gridController.CanClean(_tileNumX, _tileNumY) == 2 && _suciedad < 3) // Mancha grande
        {
            _gridController.SetInteractive(1, _tileNumX, _tileNumY);
            AudioPlay(_limpiar);
            Instantiate(_VFXClean, transform.position, Quaternion.identity);
            Instantiate(_sangre, transform.position, Quaternion.identity);
            _destroyDirt = true;
            _suciedad++;
        }
        else if (_gridController.CanClean(_tileNumX, _tileNumY) == 3) // Cubo
        {
            AudioPlay(_enjuagar);
            Instantiate(_VFXWash, transform.position, Quaternion.identity);
            _suciedad = 0;
        }
        else if (_gridController.CanClean(_tileNumX, _tileNumY) == 4 && _suciedad < 3) // Sangre ritual
        {
            _gridController.SetInteractive(0, _tileNumX, _tileNumY);
            AudioPlay(_limpiar);
            Instantiate(_VFXClean, transform.position, Quaternion.identity);
            _gridController.DirtCleaned();
            if (_tileNumX == 3 & _tileNumY == 5)
            {
                Instantiate(_pentUpLeft, new Vector3(5.5f, 3.5f, 0), Quaternion.identity);
            }
            else if (_tileNumX == 6 & _tileNumY == 6)
            {
                Instantiate(_pentUpRight, new Vector3(5.5f, 3.5f, 0), Quaternion.identity);
            }
            else if (_tileNumX == 4 & _tileNumY == 2)
            {
                Instantiate(_pentDownLeft, new Vector3(5.5f, 3.5f, 0), Quaternion.identity);
            }
            else if (_tileNumX == 8 & _tileNumY == 4)
            {
                Instantiate(_pentDownRight, new Vector3(5.5f, 3.5f, 0), Quaternion.identity);
            }
            else
            {
                Instantiate(_pentDown, new Vector3(5.5f, 3.5f, 0), Quaternion.identity);
            }
            _destroyDirt = true;
            _suciedad++;
        }
        else
        {
            if (_suciedad < 3) // Nada
            {
                AudioPlay(_limpiar);
                Instantiate(_VFXNone, transform.position, Quaternion.identity);
            }
            else // Fregona sucia
            {
                AudioPlay(_limpiar);
                Instantiate(_VFXDirty, transform.position, Quaternion.identity);
            }
        }
        while (t < 0.5)
        {
            t += Time.deltaTime;
            yield return null;
        }
        _gridController.ChangeTurn(1);
        yield return 0;
    }

    void AudioPlay(AudioClip name)
    {
        _audio.volume = GeneralSettings.SFXVOLUME / 100;
        if (!GeneralSettings.MUTED)
        {
            _audio.clip = name;
            _audio.Play();
        }
    }

    public void Teleport(int x, int y)
    {
        transform.position = new Vector3(x, y, 0);
        _tileNumX = x;
        _tileNumY = y;
        _gridController.SetGrid(_spriteNumber, _tileNumX, _tileNumY);
    }
}
