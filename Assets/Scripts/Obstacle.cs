using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    void Update()
    {
        // Rotar el objeto
        transform.Rotate(new Vector3(45, 0, 0) * Time.deltaTime);
    }
}
