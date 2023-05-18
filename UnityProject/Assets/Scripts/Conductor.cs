using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Conductor : MonoBehaviour
{
    [Header("Audio")]
    [SerializeField]
    private AudioSource _audioSource;

    [SerializeField]
    private float _songBPM;

    [Range(0f, 0.5f)]
    [SerializeField]
    private float _leniency;

    [Header("Sphere")]
    [SerializeField]
    private Material _yellowMat;

    [SerializeField]
    private Material _greyMat;

    [SerializeField]
    private MeshRenderer _sphere;

    private float _startDspTime;
    private float _secondsPerBeat => 60f / _songBPM;
    private float _songPosInSeconds => (float)AudioSettings.dspTime - _startDspTime;
    private float _songPosInBeats => _songPosInSeconds / _secondsPerBeat;

    // public bool OnBeat => _songPosInSeconds % _secondsPerBeat < _secondsPerBeat * _leniency ||
    //      _songPosInSeconds % _secondsPerBeat > _secondsPerBeat * (1 - _leniency);

    public bool OnBeat => _songPosInBeats % 1 < _leniency || _songPosInBeats % 1 > 1 - _leniency;


    // Start is called before the first frame update
    void Start()
    {
        _startDspTime = (float)AudioSettings.dspTime;
        _audioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        //_songPosInSeconds = (float)AudioSettings.dspTime - _startDspTime;
        // if (_songPosInSeconds % _secondsPerBeat < 3f * Time.fixedDeltaTime ||
        //  _songPosInSeconds % _secondsPerBeat > _secondsPerBeat - 3f * Time.fixedDeltaTime)
        // {
        //     _sphere.material = _yellowMat;
        // }
        // else
        // {
        //     _sphere.material = _greyMat;
        // }
        // if (OnBeat)
        // {
        //     _sphere.material = _yellowMat;
        // }
        // else
        // {
        //     _sphere.material = _greyMat;
        // }
    }

    public void OnSpacebarPressed(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            if (OnBeat)
            {
                Debug.Log("On beat");
                StartCoroutine(Flash());
            }
            else
            {
                Debug.Log("Missed beat");
            }
        }

    }

    private IEnumerator Flash()
    {
        _sphere.material = _yellowMat;
        yield return new WaitForSeconds(_secondsPerBeat * 0.2f);
        _sphere.material = _greyMat;
        yield return null;
    }
}
