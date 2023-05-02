using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIScript : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text1;
    [SerializeField] TextMeshProUGUI text2;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump") && text1.enabled == true && text2.enabled == true)
        {
            text1.enabled = false;
            text2.enabled = false;
        } else if (Input.GetButtonDown("Jump") && text1.enabled == false && text2.enabled == false)
        {
            text1.enabled = true;
            text2.enabled = true;
        }
    }
}
