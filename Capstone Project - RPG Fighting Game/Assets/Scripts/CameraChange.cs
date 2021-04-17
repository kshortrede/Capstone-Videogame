using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Credit to Jimmy Vegas for the code premise
public class CameraChange : MonoBehaviour
{
    
    public GameObject ThirdPerson;
    public GameObject FirstPerson;
    public int CameraMode;

    // Update is called once per frame
    void Update()
    {
        //"c" is the designated camera button
        if (Input.GetButtonDown ("Camera")) 
        {
            if (CameraMode == 1) 
            {
                CameraMode = 0;
            }
            else 
            {
                CameraMode += 1;
            }
            StartCoroutine(CamChange());
        }

        IEnumerator CamChange () 
        {
            yield return new WaitForSeconds(0.01f);
            if (CameraMode == 0) 
            {
                FirstPerson.SetActive(false);
                ThirdPerson.SetActive(true);                                                                       
            }
            if (CameraMode == 1)
            {
                FirstPerson.SetActive(true);
                ThirdPerson.SetActive(false);                
            }
        }
    }
}
