using System.Collections;
using System.Collections.Generic;
using System.IO.Ports;
using UnityEngine;
using TMPro;

public class ArduinoManager : MonoBehaviour
{
    [Header("Arduino Attribute")]
    [SerializeField] bool isConnected;
    [SerializeField] string portName;
    [SerializeField] int baudRate;
    [SerializeField] SerialPort data_stream;
    public TextMeshProUGUI sourcePortText;
    public TextMeshProUGUI detailDataText;
    public string datasReceived;

    [Header("Joystick Attribute")]
    public Vector2 leftJoystickDetail;
    public Vector2 rightJoystickDetail;
    public List<float> pushButtonDetail;

    void Start()
    {
        SetupSourcePort();
    }

    void Update()
    {
        if (isConnected)
        {
            datasReceived = data_stream.ReadLine();

            //menerima data yang dikirimkan oleh Arduino
            string[] datas = data_stream.ReadLine().Split(',');

            //menyiapkan seluruh data yang akan digunakan
            leftJoystickDetail.x = float.Parse(datas[0]);
            leftJoystickDetail.y = float.Parse(datas[1]);
            rightJoystickDetail.x = float.Parse(datas[2]);
            rightJoystickDetail.y = float.Parse(datas[3]);
            pushButtonDetail[0] = float.Parse(datas[4]);
            pushButtonDetail[1] = float.Parse(datas[5]);

            detailDataText.text = $"Joystick: {leftJoystickDetail.x}, {leftJoystickDetail.y}, {rightJoystickDetail.x}, {rightJoystickDetail.y} || " +
                                  $"Push Button: {pushButtonDetail[0]}, {pushButtonDetail[1]}";
        }
    }

    public void SetupSourcePort()
    {
        if (isConnected)
        {
            data_stream.Close();
            isConnected = false;
        }

        //portName = sourcePortText.text;

        //deklarasi sumber port dan baud rate yang digunakan oleh Arduino
        data_stream = new SerialPort(portName, baudRate);

        //membuka port yang terhubung
        data_stream.Open();

        isConnected = true;
    }
}
