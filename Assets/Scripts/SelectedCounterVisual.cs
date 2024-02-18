using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script handles the activation of selected counter visual when the on selected counter changed event is fired
public class SelectedCounterVisual : MonoBehaviour
{
    //The reference to the counter
    [SerializeField] private BaseCounter counter;
    //The reference to all the objects that have to be set as selected
    [SerializeField] private GameObject[] visualGameObjectArray;
    private void Start()
    {
        Player.Instance.OnSelectedCounterChanged += Player_OnSelectedCounterChanged;
    }

    private void Player_OnSelectedCounterChanged(object Sender, Player.OnSelectedCounterChangedEventArgs e)
    {
        //This event will be listened by every counter which has this script, so we will check if this counter is the one that is currently selected
        if(e.selectedCounter == counter) 
        {
            Show();
        }
        else
        {
            Hide();
        }
    }
    private void Show()
    {
        //We will set all the individual objects as selected
        foreach(GameObject visualGameObject in visualGameObjectArray)
        {
            visualGameObject.SetActive(true);
        }
    }
    private void Hide()
    {
        foreach (GameObject visualGameObject in visualGameObjectArray)
        {
            visualGameObject.SetActive(false);
        }
    }
}
