using System.Collections;
using System.Collections.Generic;
using System.IO.Ports;
using UnityEngine;

public class ArduinoManager : MonoBehaviour
{
    [Header ("Arduino Attribute")]
    [SerializeField] bool isConnected;
    [SerializeField] string portName;
    [SerializeField] int baudRate;
    [SerializeField] SerialPort data_stream;
    public List<string> joystickData;
    
    void Start()
    {
        //deklarasi sumber port dan baud rate yang digunakan oleh Arduino
        data_stream = new SerialPort(portName, baudRate);

        //membuka port yang terhubung
        data_stream.Open();

        //menentukan batas jumlah waktu yang digunakan saat proses tidak selesai
        data_stream.ReadTimeout = 101;
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
                joystickData[0] = datas[0];
                joystickData[1] = datas[1];
                joystickData[2] = datas[2];
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
