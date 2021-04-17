/// <summary>
/// Kenneth Shortrede
/// GUI --- NPC Floating Names
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Simple script to make the floating names of each NPC always face the player on the Scene
public class NamesFaceCamera : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        this.transform.LookAt(Camera.main.transform.position);
        this.transform.Rotate(0, 180, 0);
    }
}
