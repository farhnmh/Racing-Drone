using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    [Header ("Drone Attribute")]
    [SerializeField] GameObject drone;
    [SerializeField] List<GameObject> propeller;
    [SerializeField] List<Vector3> propellerRotation;
    [SerializeField] float propellerSpeed;

    [Header("LED Properties")]
    [SerializeField] GameObject led;
    [SerializeField] List<Material> ledMaterial;
    [SerializeField] int waitingSeconds;
    [SerializeField] int indexMaterial;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LEDBlinking());
    }

    // Update is called once per frame
    void Update()
    {
        RotatePropeller();
    }

    public void MoveToScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void QuitApplication()
    {
        Application.Quit();
    }

    public void RotatePropeller()
    {
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
}
