using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] DroneController drone;
    [SerializeField] GameObject gameOverPanel;
    [SerializeField] GameObject[] checkpoint;
    public int indexCheckpoint;
    public bool isOver;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < checkpoint.Length; i++)
        {
            if (indexCheckpoint == int.Parse(checkpoint[i].name))
                checkpoint[i].SetActive(true);
            else
                checkpoint[i].SetActive(false);
        }

        if (indexCheckpoint == checkpoint.Length)
            isOver = true;

        if (!drone.isOn && isOver)
            gameOverPanel.SetActive(true);
    }
}
