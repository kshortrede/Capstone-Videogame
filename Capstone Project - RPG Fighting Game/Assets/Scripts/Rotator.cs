/// <summary>
/// Kenneth Shortrede
/// Rotate
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Simple class used to rotate objects around in the game
public class Rotator : MonoBehaviour
{
    public float speed;
    public float x, y, z;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(x * Time.deltaTime * speed, y * Time.deltaTime * speed, z * Time.deltaTime * speed, Space.World);
    }
}
