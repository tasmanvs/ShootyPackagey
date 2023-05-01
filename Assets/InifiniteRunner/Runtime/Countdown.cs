using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // Add this namespace
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Countdown : MonoBehaviour
{

    [SerializeField]
    GameObject EndCanvas;
    [SerializeField]
    Button RestartButton;
    public TextMeshProUGUI TimeText; // Use TextMeshProUGUI instead of Text
    private int _countdown = 5;

    private float _startTime;
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        _startTime = Time.time;
        RestartButton.onClick.AddListener(Restart);
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

    void Restart()
    {
        EndCanvas.SetActive(false);
        Time.timeScale = 1;
        _startTime = Time.time;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}
