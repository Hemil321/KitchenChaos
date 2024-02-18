using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatesCounterVisual : MonoBehaviour
{
    //A reference to the plates counter to catch the events
    [SerializeField] PlatesCounter platesCounter;

    [SerializeField] Transform counterTopPoint;
    [SerializeField] Transform plateVisualPrefab;

    //To store all the plate visual objects
    private List<GameObject> plateVisualGameObjectList;

    private void Awake()
    {
        plateVisualGameObjectList = new List<GameObject>();
    }

    private void Start()
    {
        platesCounter.OnPlateSpawned += PlatesCounter_OnPlateSpawned;
        platesCounter.OnPlateRemoved += PlatesCounter_OnPlateRemoved;
    }

    private void PlatesCounter_OnPlateRemoved(object sender, System.EventArgs e)
    {
        //When the player takes the plate, we also need to remove one from the visual
        GameObject plateOnTop = plateVisualGameObjectList[plateVisualGameObjectList.Count - 1];
        plateVisualGameObjectList.Remove(plateOnTop);
        Destroy(plateOnTop);
    }

    private void PlatesCounter_OnPlateSpawned(object sender, System.EventArgs e)
    {
        //spawn a new plate visual
        Transform plateVisualTransform = Instantiate(plateVisualPrefab, counterTopPoint);

        //Offset the spawning position according to the plates on the counter
        float plateOffsetY = .1f;
        plateVisualTransform.localPosition = new Vector3(0, plateOffsetY * plateVisualGameObjectList.Count, 0);
        plateVisualGameObjectList.Add(plateVisualTransform.gameObject);
    }
}
