using System;
using UnityEngine;

public class Line : MonoBehaviour
{
    public GameObject[] _objs;
    
    private LineRenderer _lineRenderer;

    void Start()
    {
        _lineRenderer = this.gameObject.GetComponent<LineRenderer>();
        _lineRenderer.material = new Material(Shader.Find("Legacy Shaders/Particles/Alpha Blended Premultiply"));
    }

    void Update()
    {
        for (int i = 0; i < _objs.Length; i++)
        {
            _lineRenderer.SetPosition(i, _objs[i].transform.position);
        }
    }
}
