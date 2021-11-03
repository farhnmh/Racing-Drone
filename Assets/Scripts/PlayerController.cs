using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public ArduinoManager arduino;

    [Header("Drone Properties")]
    [SerializeField] GameObject drone;
    [SerializeField] List<GameObject> propeller;
    [SerializeField] List<Vector3> propellerRotation;
    [SerializeField] float indexSpeed;
    [SerializeField] float normalSpeed;
    [SerializeField] float steadySpeed;
    [SerializeField] float runSpeed;

    [Header("LED Properties")]
    [SerializeField] GameObject led;
    [SerializeField] List<Material> ledMaterial;
    [SerializeField] Material green;
    [SerializeField] Material yellow;
    [SerializeField] int waitingSeconds;
    [SerializeField] int indexMaterial;

    [Header("Condition Properties")]
    [SerializeField] bool isRun;
    [SerializeField] bool isPaused;
    [SerializeField] bool isGrounded;

    [Header("Controller Properties")]
    [SerializeField] List<KeyCode> leftJoystick;
    [SerializeField] List<KeyCode> rightJoystick;
    [SerializeField] List<KeyCode> pushButton;
    [SerializeField] Vector2 leftJoystickDetail;
    [SerializeField] Vector2 rightJoystickDetail;
    [SerializeField] List<float> pushButtonDetail;
    [SerializeField] float rotateSpeed;
    [SerializeField] float moveSpeed;

    Vector3 newRotation;
    Vector3 newPosition;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LEDBlinking());
    }

    // Update is called once per frame
    void Update()
    {
        StartPauseEngine();

        if (isRun)
        {
            if (!isPaused)
            {
                MovementController();
                DroneMovement();
                ledMaterial[1] = green;
            }

            else if (isPaused)
            {
                ledMaterial[1] = yellow;
            }

            PropellerRotator();
        }
    }

    public void PropellerRotator()
    {
        propeller[0].transform.Rotate(propellerRotation[0].x * Time.deltaTime * indexSpeed,
                                      propellerRotation[0].y * Time.deltaTime * indexSpeed,
                                      propellerRotation[0].z * Time.deltaTime * indexSpeed);

        propeller[1].transform.Rotate(propellerRotation[1].x * Time.deltaTime * indexSpeed,
                                      propellerRotation[1].y * Time.deltaTime * indexSpeed,
                                      propellerRotation[1].z * Time.deltaTime * indexSpeed);

        propeller[2].transform.Rotate(propellerRotation[2].x * Time.deltaTime * indexSpeed,
                                      propellerRotation[2].y * Time.deltaTime * indexSpeed,
                                      propellerRotation[2].z * Time.deltaTime * indexSpeed);

        propeller[3].transform.Rotate(propellerRotation[3].x * Time.deltaTime * indexSpeed,
                                      propellerRotation[3].y * Time.deltaTime * indexSpeed,
                                      propellerRotation[3].z * Time.deltaTime * indexSpeed);
    }

    public void StartPauseEngine()
    {
        //start engine
        if (Input.GetKeyDown(pushButton[0]))
            isRun = true;

        //pause engine
        if (Input.GetKeyDown(pushButton[1]))
        {
            if (!isPaused && isRun)
            {
                isPaused = true;
            }
            else if (isPaused && isRun)
                isPaused = false;
        }

        //landing
        if (Input.GetKeyDown(pushButton[1]))
        {

        }

        //stop engine
        if (Input.GetKeyDown(pushButton[3]))
        {
            led.transform.GetComponent<MeshRenderer>().material = ledMaterial[0];
            isRun = false;
        }
    }

    public void MovementController()
    {
        if (Input.GetKey(leftJoystick[0]))
        {
            leftJoystickDetail.x = 1;
            indexSpeed = runSpeed;

            this.transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
        }

        if (Input.GetKey(leftJoystick[1]))
        {
            leftJoystickDetail.y = -1;
            indexSpeed = runSpeed;

            this.transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
        }

        if (Input.GetKey(leftJoystick[2]))
        {
            leftJoystickDetail.x = -1;
            indexSpeed = runSpeed;

            this.transform.Translate(-Vector3.forward * moveSpeed * Time.deltaTime);
        }

        if (Input.GetKey(leftJoystick[3]))
        {
            leftJoystickDetail.y = 1;
            indexSpeed = runSpeed;

            this.transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
        }

        if (Input.GetKey(rightJoystick[0]))
        {
            rightJoystickDetail.x = 1;
            indexSpeed = runSpeed;

            this.transform.Translate(Vector3.up * moveSpeed * Time.deltaTime);
        }

        if (Input.GetKey(rightJoystick[1]))
        {
            rightJoystickDetail.y = -1;
            indexSpeed = runSpeed;
        }

        if (Input.GetKey(rightJoystick[2]))
        {
            rightJoystickDetail.x = -1;
            indexSpeed = runSpeed;

            this.transform.Translate(-Vector3.up * moveSpeed * Time.deltaTime);
        }

        if (Input.GetKey(rightJoystick[3]))
        {
            rightJoystickDetail.y = 1;
            indexSpeed = runSpeed;
        }

        if (Input.GetKeyUp(leftJoystick[0]) ||
            Input.GetKeyUp(leftJoystick[1]) ||
            Input.GetKeyUp(leftJoystick[2]) ||
            Input.GetKeyUp(leftJoystick[3]) ||
            Input.GetKeyUp(rightJoystick[0]) ||
            Input.GetKeyUp(rightJoystick[1]) ||
            Input.GetKeyUp(rightJoystick[2]) ||
            Input.GetKeyUp(rightJoystick[3]))
        {
            if (!isGrounded)
                indexSpeed = steadySpeed;
            else
                indexSpeed = normalSpeed;

            leftJoystickDetail = new Vector2(0, 0);
            rightJoystickDetail = new Vector2(0, 0);
            this.transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
    }

    public void DroneMovement()
    {
        if (leftJoystickDetail.x != 0)
        {
            if (leftJoystickDetail.x > 0 && newRotation.x < 30)
                newRotation.x += rotateSpeed * Time.deltaTime;
            if (leftJoystickDetail.x < 0 && newRotation.x > -30)
                newRotation.x -= rotateSpeed * Time.deltaTime;
        }
        if (leftJoystickDetail.x == 0)
        {
            if (newRotation.x > 0)
                newRotation.x -= rotateSpeed * Time.deltaTime;
            if (newRotation.x < 0)
                newRotation.x += rotateSpeed * Time.deltaTime;
        }
        if (leftJoystickDetail.y != 0)
        {
            if (leftJoystickDetail.y > 0 && newRotation.z > -30)
                newRotation.z -= rotateSpeed * Time.deltaTime;
            if (leftJoystickDetail.y < 0 && newRotation.z < 30)
                newRotation.z += rotateSpeed * Time.deltaTime;
        }
        if (leftJoystickDetail.y == 0)
        {
            if (newRotation.z > 0)
                newRotation.z -= rotateSpeed * Time.deltaTime;
            if (newRotation.z < 0)
                newRotation.z += rotateSpeed * Time.deltaTime;
        }

        if (rightJoystickDetail.y != 0)
        {
            if (rightJoystickDetail.y > 0)
                newRotation.y += rotateSpeed * Time.deltaTime;
            if (rightJoystickDetail.y < 0)
                newRotation.y -= rotateSpeed * Time.deltaTime;
        }

        //newPosition = new Vector3(leftJoystickDetail.y, rightJoystickDetail.x, leftJoystickDetail.x);
        //this.transform.position += newPosition * moveSpeed * Time.deltaTime;

        drone.transform.localRotation = Quaternion.Euler(newRotation.x, drone.transform.rotation.y, newRotation.z);
        this.transform.localRotation = Quaternion.Euler(this.transform.rotation.x, newRotation.y, this.transform.rotation.z);
    }

    IEnumerator LEDBlinking()
    {
        if (indexMaterial == ledMaterial.Count)
            indexMaterial = 0;

        if (isRun)
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
