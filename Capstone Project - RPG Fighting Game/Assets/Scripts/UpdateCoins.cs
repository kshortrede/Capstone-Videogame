/// <summary>
/// Kenneth Shortrede
/// GUI --- Update coins player has earned on Screen
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UpdateCoins : MonoBehaviour
{
    private Text text;
    private void Awake()
    {
        text = this.gameObject.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("" + Character.exp);
        text.text =  " " + Character.gold;
    }
}
