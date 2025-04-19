using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UIElements;

public class Boss : Enemy
{
    [SerializeField] int _random = 0;
    bool _endStart = false;
    float t = 0;

    [SerializeField] GameObject _VFXTP;
    [SerializeField] GameObject _VFXDying;
    [SerializeField] GameObject _VFXDead;
    [SerializeField] GameObject _spriteChulu;

    AudioSource _audio;
    AudioSource _audioTransform;
    AudioSource _audioRumble;
    AudioSource _audioChulu;
    [SerializeField] GameObject _ext1;
    [SerializeField] GameObject _ext2;
    [SerializeField] GameObject _ext3;
    [SerializeField] AudioClip _tp;

    [SerializeField] bool _phase2 = false;

    protected override void Awake()
    {
        base.Awake();
        GetReferences();


        _audio = GetComponent<AudioSource>();
        _audio.volume = GeneralSettings.SFXVOLUME / 100;
        _audioTransform = _ext1.GetComponent<AudioSource>();
        _audioTransform.volume = GeneralSettings.SFXVOLUME / 100;
        _audioRumble = _ext2.GetComponent<AudioSource>();
        _audioRumble.volume = GeneralSettings.SFXVOLUME / 100;
        _audioChulu = _ext3.GetComponent<AudioSource>();
        _audioChulu.volume = GeneralSettings.SFXVOLUME / 100;
    }

    protected override void Start()
    {
        _spriteNumber = 3;
        _gridController.SetGrid(_spriteNumber, _tileNumX, _tileNumY);
        transform.position = new Vector2(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y));
        _gridController.CreateEnemy();
        _characterLastTurn = _character.transform.position;

        _move = SoundManager.Instance.GetCharacterSFX("Boss", "move");
        _tp = SoundManager.Instance.GetCharacterSFX("Boss", "teleport");
        _audioTransform.clip = SoundManager.Instance.GetCharacterSFX("Boss", "transform");
        _audioRumble.clip = SoundManager.Instance.GetCharacterSFX("Boss", "rumble");
        _audioChulu.clip = SoundManager.Instance.GetCharacterSFX("Chulu", "main");
    }

    private void Update()
    {
        _audioSource.volume = GeneralSettings.SFXVOLUME / 100;
        if (_gridController.GetDirt() != 0)
        {
            MovePathFinding();
        }

        if (_gridController.GetEnd() && !_endStart)
        {
            _audioTransform.volume = GeneralSettings.SFXVOLUME / 100;
            if (!GeneralSettings.MUTED)
            {
                _audioTransform.Play();
            }
            _endStart = true;
            Instantiate(_VFXDying, transform.position, Quaternion.identity);
            _audioRumble.volume = GeneralSettings.SFXVOLUME / 100;
            if (!GeneralSettings.MUTED)
            {
                _audioRumble.Play();
            }
        }

        if(!_phase2 && _gridController.GetDirt() <= 3)
        {
            _phase2 = true;
        }

    }

    protected override void Attack()
    {
        _character.SubstractLife(_damage);
        StartCoroutine(AttackCoroutine(_rb, new Vector2(Mathf.RoundToInt(_character.transform.position.x) * _characterMovements,
            Mathf.RoundToInt(_character.transform.position.y) * _characterMovements)));
    }

    override protected void MovePathFinding()
    {
        if (_gridController.GetTurn() == 1 && _character != null && _gridController.GetEnemyMoved() == _enemyNumber)
        {
            _gridController.ChangeTurn(2);
            if (((Mathf.Abs(Mathf.RoundToInt(transform.position.x - _character.transform.position.x)) == 1
                        && Mathf.RoundToInt(transform.position.y) == Mathf.RoundToInt(_character.transform.position.y))
            ||
                        (Mathf.Abs(Mathf.RoundToInt(transform.position.y - _character.transform.position.y)) == 1)
                        && Mathf.RoundToInt(transform.position.x) == Mathf.RoundToInt(_character.transform.position.x)))
            {
                Attack();
            }
            else
            {
                if (!_phase2)
                {
                    _random = Random.Range(1, 5);
                    if (_random == 1)
                    {
                        StartCoroutine(TPCoroutine());
                    }
                    else
                    {
                        if (_blockedView)
                        {
                            Debug.Log("PFBV");
                            PathFindingBlockView(_character.transform.position);
                        }
                        else
                        {
                            Debug.Log("PF");
                            PathFinding(_character.transform.position);
                        }
                    }
                }
                else
                {
                    _random = Random.Range(1, 20);
                    if (_random == 1 || _random == 2 || _random == 3)
                    {
                        StartCoroutine(TPCoroutine());
                    }
                    else if (_random == 4 || _random == 5 || _random == 6)
                    {
                        StartCoroutine(ChangeCoroutine());
                    }
                    else
                    {
                        if (_blockedView)
                        {
                            Debug.Log("PFBV");
                            PathFindingBlockView(_character.transform.position);
                        }
                        else
                        {
                            Debug.Log("PF");
                            PathFinding(_character.transform.position);
                        }
                    }
                }
            }
        }
    }

    virtual protected IEnumerator TPCoroutine()
    {
        bool tp = false;
        int x = Random.Range(1, 10);
        int y = Random.Range(1, 6);
        while (!tp)
        {
            x = Random.Range(1, 10);
            y = Random.Range(1, 6);
            if(_gridController.CanMove(x,y))
            {
                tp = true;
            }
            else 
            {
                yield return 0;
            }
        }
        _gridController.SetGrid(0, _tileNumX, _tileNumY);
        _tileNumX = Mathf.RoundToInt(x);
        _tileNumY = Mathf.RoundToInt(y);
        _audio.clip = _tp;
        _audio.volume = GeneralSettings.SFXVOLUME / 100;
        if (!GeneralSettings.MUTED)
        {
            _audio.Play();
        }
        transform.position = new Vector3(x,y,0);
        _gridController.SetGrid(_spriteNumber, _tileNumX, _tileNumY);
        Instantiate(_VFXTP, transform.position, Quaternion.identity);
        SetChangeTurn();
        yield return 0;
    }

    virtual protected IEnumerator ChangeCoroutine()
    {
        int x = _tileNumX;
        int y = _tileNumY;
        Instantiate(_VFXTP, transform.position, Quaternion.identity);
        transform.position = _character.transform.position;
        _character.Teleport(x, y);
        _tileNumX = Mathf.RoundToInt(transform.position.x);
        _tileNumY = Mathf.RoundToInt(transform.position.y);
        _gridController.SetGrid(_spriteNumber, _tileNumX, _tileNumY);
        _audio.clip = _tp;
        _audio.volume = GeneralSettings.SFXVOLUME / 100;
        if (!GeneralSettings.MUTED)
        {
            _audio.Play();
        }
        Instantiate(_VFXTP, transform.position, Quaternion.identity);
        SetChangeTurn();
        yield return 0;
    }

    virtual protected IEnumerator EndCoroutine()
    {
        while (t < 3)
        {
            t += Time.deltaTime;
            yield return null;
        }
        Instantiate(_VFXDead, transform.position, Quaternion.identity);
        Instantiate(_spriteChulu, transform.position, Quaternion.identity);
        _audioChulu.PlayDelayed(46100);
        Destroy(this.gameObject);
        yield return 0;
    }
}
