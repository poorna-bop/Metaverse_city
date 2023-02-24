using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowScript : MonoBehaviour
{
    public float speed=1;
    Vector3 _startPosition;
    void Start () 
    {
        speed = Random.Range(speed-2, speed+2);
        _startPosition = transform.position;
    }
 
    void Update()
    {
        transform.position = _startPosition + new Vector3(0.0f, 0.0f, Mathf.Sin(speed*Time.time));
    }
}
