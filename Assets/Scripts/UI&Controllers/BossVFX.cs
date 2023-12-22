using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossVFX : Sprites
{
    [SerializeField] GameObject _VFX1;
    [SerializeField] GameObject _VFX2;
    [SerializeField] GameObject _VFX3;
    [SerializeField] GameObject _VFX4;
    [SerializeField] GameObject _VFX5;
    [SerializeField] GameObject _VFX6;
    [SerializeField] GameObject _Boss;

    float t = 0;
    bool effects = false;

    // Update is called once per frame
    void Update()
    {
        if (_gridController.GetRitual() && !effects)
        {
            effects = true;
            Instantiate(_VFX1, new Vector3(6, 4, 0), Quaternion.identity);
            Instantiate(_VFX2, new Vector3(6, 4, 0), Quaternion.identity);
            Instantiate(_VFX3, new Vector3(6, 4, 0), Quaternion.identity);
            StartCoroutine(RitualAnimation());
        }
    }

    protected IEnumerator RitualAnimation()
    {
        while (t < 2)
        {
            t += Time.deltaTime;
            yield return null;
        }
        Instantiate(_VFX4, new Vector3(6, 4, 0), Quaternion.identity);
        Instantiate(_VFX5, new Vector3(6, 4, 0), Quaternion.identity);
        Instantiate(_VFX6, new Vector3(6, 4, 0), Quaternion.identity);
        Instantiate(_Boss, new Vector3(6, 4, 0), Quaternion.identity);
        yield return null;
    }

}
