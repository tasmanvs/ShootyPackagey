using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // Add this namespace


public class Countdown : MonoBehaviour
{

    [SerializeField]
    GameObject EndCanvas;
    public TextMeshProUGUI TimeText; // Use TextMeshProUGUI instead of Text
    private int _countdown = 60;

    private float _startTime;
    // Start is called before the first frame update
    void Start()
    {
        _startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        int left = _countdown - (int)(Time.time - _startTime);


        TimeText.text =  left < 10? "00:0" + left.ToString() : "00:" + left.ToString();

        if(left == 0)
        {
            Time.timeScale = 0;
            EndCanvas.SetActive(true);
        }
    }
}
