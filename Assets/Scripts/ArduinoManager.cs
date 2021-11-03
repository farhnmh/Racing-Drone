using System.Collections;
using System.Collections.Generic;
using System.IO.Ports;
using UnityEngine;

public class ArduinoManager : MonoBehaviour
{
    [Header("Arduino Attribute")]
    [SerializeField] bool isConnected;
    [SerializeField] string portName;
    [SerializeField] int baudRate;
    [SerializeField] SerialPort data_stream;

    [Header("Joystick Attribute")]
    public Vector2 leftJoystickDetail;
    public Vector2 rightJoystickDetail;
    public List<float> pushButtonDetail;

    void Start()
    {
        //deklarasi sumber port dan baud rate yang digunakan oleh Arduino
        data_stream = new SerialPort(portName, baudRate);

        //membuka port yang terhubung
        data_stream.Open();

        //menentukan batas jumlah waktu yang digunakan saat proses tidak selesai
        //data_stream.ReadTimeout = 101;

        isConnected = true;
    }

    void Update()
    {
        if (data_stream.IsOpen)
        {
            try
            {
                //menerima data yang dikirimkan oleh Arduino
                string[] datas = data_stream.ReadLine().Split(',');

                //menyiapkan seluruh data yang akan digunakan
                leftJoystickDetail.x = float.Parse(datas[0]);
                leftJoystickDetail.y = float.Parse(datas[1]);
                rightJoystickDetail.x = float.Parse(datas[2]);
                rightJoystickDetail.y = float.Parse(datas[3]);
                pushButtonDetail[0] = float.Parse(datas[4]);
                pushButtonDetail[1] = float.Parse(datas[5]);
                pushButtonDetail[2] = float.Parse(datas[6]);
                pushButtonDetail[3] = float.Parse(datas[7]);
            }
            catch (System.Exception)
            {

            }
        }
        else
        {
            //menutup port
            data_stream.Close();
            isConnected = false;
        }
    }
}
