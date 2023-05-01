using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // Add this namespace
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;// Required when using Event data.

public class Countdown : MonoBehaviour
{

    [SerializeField]
    GameObject EndCanvas;
    [SerializeField]
    Button RestartButton;

    [SerializeField]
    Button PlusButton;
    
    [SerializeField]
    Button Mass;

    [SerializeField]
    Button Speed;

    [SerializeField]
    GameObject Package;

    [SerializeField]
    GameObject Cannon;

    public TextMeshProUGUI TimeText; // Use TextMeshProUGUI instead of Text
    private int _countdown = 1;

    private float _packageMass = 1;
    private float _cannonSpeed = 20;

    private float _startTime;
    // Start is called before the first frame update
    void Start()
    {
        RemoveListeners(); 

        Time.timeScale = 1;
        _startTime = Time.time;
        RestartButton.onClick.AddListener(Restart);
        Mass.onClick.AddListener(AddMass);
        Mass.gameObject.GetComponentInChildren<TextMeshProUGUI>().text = "package mass: " + _packageMass.ToString();

        Speed.onClick.AddListener(AddSpeed);
        Speed.gameObject.GetComponentInChildren<TextMeshProUGUI>().text = "Cannon speed: " + _cannonSpeed.ToString();

        Package.GetComponent<Rigidbody>().mass = _packageMass;

        Debug.Log($"canno is null {Cannon.GetComponent<CannonController>() == null}");

        Cannon.transform.Find("cannon_transform/rail_cannon").gameObject.GetComponent<CannonController>().shootForce = _cannonSpeed;

        Speed.interactable = true;
        Mass.interactable = true;

        PlusButton.gameObject.SetActive(false);
    }

    void RemoveListeners()
    {
        RestartButton.onClick.RemoveAllListeners();
        Mass.onClick.RemoveAllListeners();
        Speed.onClick.RemoveAllListeners();
    }

    void AddMass()
    {
        PlusButton.gameObject.SetActive(true);
        PlusButton.transform.position = Mass.transform.position + new Vector3(150, 0, 0);
        _packageMass += 3;

        Speed.interactable = false;

        Mass.gameObject.GetComponentInChildren<TextMeshProUGUI>().text = "package mass: " + _packageMass.ToString();
    }

    void AddSpeed()
    {
        PlusButton.gameObject.SetActive(true);
        PlusButton.transform.position = Speed.transform.position + new Vector3(150, 0, 0);
        _cannonSpeed += 10;

        Mass.interactable = false;

        Speed.gameObject.GetComponentInChildren<TextMeshProUGUI>().text = "Cannon speed: " + _cannonSpeed.ToString();
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
        Start();
        Time.timeScale = 1;
        _startTime = Time.time;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}
