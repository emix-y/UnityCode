using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;

public class SphereBehaviour : MonoBehaviour
{
    // Variables to store the sensor data
    float temperature;
    float humidity;

    // Serial port variables
    SerialPort serialPort;
    string portName = "COM3"; // Change this to the name of the serial port you are using
    int baudRate = 9600;

    // Variables to control sphere size and color
    float sphereSize = 1.0f;
    Color sphereColor = Color.white;

    // Start is called before the first frame update
    void Start()
    {
        // Open the serial port
        serialPort = new SerialPort(portName, baudRate);
        serialPort.Open();
    }

    // Update is called once per frame
    void Update()
    {
        // Read data from the serial port
        string data = serialPort.ReadLine();

        // Split the data into temperature and humidity values
        string[] values = data.Split(',');
        temperature = float.Parse(values[0]);
        humidity = float.Parse(values[1]);

        // Set sphere size based on humidity (0-100% humidity maps to 0.5-2.0 sphere size)
        sphereSize = Mathf.Lerp(0.5f, 2.0f, humidity / 100.0f);
        transform.localScale = new Vector3(sphereSize, sphereSize, sphereSize);

        // Set sphere color based on temperature (0-50°C maps to blue-green-yellow-red spectrum)
        float t = Mathf.Clamp((temperature - 0) / (50 - 0), 0, 1);
        sphereColor = Color.Lerp(Color.blue, Color.green, t);
        sphereColor = Color.Lerp(sphereColor, Color.yellow, t);
        sphereColor = Color.Lerp(sphereColor, Color.red, t);
        GetComponent<Renderer>().material.color = sphereColor;
    }

    // Close the serial port when the script is destroyed
    private void OnDestroy()
    {
        if (serialPort != null && serialPort.IsOpen)
        {
            serialPort.Close();
        }
    }
}
