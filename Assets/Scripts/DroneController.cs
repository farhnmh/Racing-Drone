using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneController : MonoBehaviour
{
    [Header ("Another Script")]
    [SerializeField] ArduinoManager arduino;
    [SerializeField] GameObject drone;

    [Header ("Attribute Drone")]
    [SerializeField] float moveSpeed;
    [SerializeField] float upwardSpeed;
    [SerializeField] float rotateSpeed;
    [SerializeField] float indexRotation;
    [SerializeField] float angleRotation;

    [Header ("Attribute Propeller")]
    [SerializeField] List<GameObject> propeller;
    [SerializeField] List<Vector3> propellerRotation;
    [SerializeField] float propellerSpeed;

    [Header("LED Properties")]
    [SerializeField] GameObject led;
    [SerializeField] List<Material> ledMaterial;
    [SerializeField] Material green;
    [SerializeField] Material yellow;
    [SerializeField] int waitingSeconds;
    [SerializeField] int indexMaterial;

    [Header("Bool Condition")]
    public bool isGrounded;
    public bool isOn;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LEDBlinking());
    }

    // Update is called once per frame
    void Update()
    {
        //left button
        if (!isOn && arduino.pushButtonDetail[0] == 1)
        {
            ledMaterial[1] = green;
            isOn = true;
        }
        else if (isGrounded && isOn && arduino.pushButtonDetail[1] == 1)
        {
            ledMaterial[1] = yellow;
            isOn = false;
        }

        if (isOn)
        {
            //propeller setting
            if (arduino.leftJoystickDetail[0] != 0 ||
                arduino.leftJoystickDetail[1] != 0 ||
                arduino.rightJoystickDetail[0] != 0 ||
                arduino.rightJoystickDetail[1] != 0)
            {
                propellerSpeed = 600;
            }
            else
            {
                propellerSpeed = 150;
            }

            propeller[0].transform.Rotate(propellerRotation[0].x * Time.deltaTime * propellerSpeed,
                                          propellerRotation[0].y * Time.deltaTime * propellerSpeed,
                                          propellerRotation[0].z * Time.deltaTime * propellerSpeed);

            propeller[1].transform.Rotate(propellerRotation[1].x * Time.deltaTime * propellerSpeed,
                                          propellerRotation[1].y * Time.deltaTime * propellerSpeed,
                                          propellerRotation[1].z * Time.deltaTime * propellerSpeed);

            propeller[2].transform.Rotate(propellerRotation[2].x * Time.deltaTime * propellerSpeed,
                                          propellerRotation[2].y * Time.deltaTime * propellerSpeed,
                                          propellerRotation[2].z * Time.deltaTime * propellerSpeed);

            propeller[3].transform.Rotate(propellerRotation[3].x * Time.deltaTime * propellerSpeed,
                                          propellerRotation[3].y * Time.deltaTime * propellerSpeed,
                                          propellerRotation[3].z * Time.deltaTime * propellerSpeed);

            //left joystick
            this.transform.Translate(arduino.leftJoystickDetail[0] * Vector3.forward * moveSpeed * Time.deltaTime);
            this.transform.Translate(arduino.leftJoystickDetail[1] * Vector3.right * moveSpeed * Time.deltaTime);

            //right joystick
            indexRotation = indexRotation + arduino.rightJoystickDetail[1] * rotateSpeed * Time.deltaTime;
            this.transform.Translate(arduino.rightJoystickDetail[0] * Vector3.up * upwardSpeed * Time.deltaTime);
            this.transform.localRotation = Quaternion.Euler(this.transform.rotation.x, indexRotation, this.transform.rotation.z);

            //angle settings
            drone.transform.localRotation = Quaternion.Euler(arduino.leftJoystickDetail[0] * angleRotation, drone.transform.rotation.y, -arduino.leftJoystickDetail[1] * angleRotation);
        }
    }

    IEnumerator LEDBlinking()
    {
        if (indexMaterial == ledMaterial.Count)
            indexMaterial = 0;

        led.transform.GetComponent<MeshRenderer>().material = ledMaterial[indexMaterial];

        indexMaterial++;
        yield return new WaitForSeconds(waitingSeconds);
        StartCoroutine(LEDBlinking());
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
            isGrounded = true;
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
            isGrounded = false;
    }
}
