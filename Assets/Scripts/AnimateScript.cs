using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateScript : MonoBehaviour
{
    public float ScaleSpeed;
    public float MaxScaleShift;

    private Vector3 _startScale;
    private bool _upscale;

    void Start()
    {
        _startScale = transform.localScale;
        _upscale = true;
    }

    void Update()
    {
        var scaleShift = Time.deltaTime * ScaleSpeed;
        var nextScale = transform.localScale.x + (_upscale ? scaleShift : -scaleShift);
        if (nextScale >= _startScale.x + MaxScaleShift)
        {
            transform.localScale = new Vector3(_startScale.x + MaxScaleShift, _startScale.y + MaxScaleShift * (_startScale.y / _startScale.x), _startScale.z + MaxScaleShift);
            _upscale = false;
        }
        else if (nextScale <= _startScale.x)
        {
            transform.localScale = new Vector3(_startScale.x, _startScale.y, _startScale.z);
            _upscale = true;
        }
        else
        {
            transform.localScale = new Vector3(nextScale, nextScale * (_startScale.y / _startScale.x), nextScale);
        }
    }
}
