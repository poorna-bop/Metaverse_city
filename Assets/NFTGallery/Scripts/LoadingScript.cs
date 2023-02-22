using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingScript : MonoBehaviour
{
   public GameObject rotateObj;
   public int speed=50;
    void Update()
    {
        rotateObj.transform.Rotate(0.0f, 0.0f, Time.deltaTime*speed, Space.Self);
    }
}
