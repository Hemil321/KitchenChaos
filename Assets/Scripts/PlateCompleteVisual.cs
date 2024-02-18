using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script is responsible for changing the visual of the plate according to the ingredients that it is carrying
public class PlateCompleteVisual : MonoBehaviour
{
    //This struct stores the link between a kitchen object and the actual game object
    //Use serializable to expose it to the editor for dropping objects
    [Serializable]
    public struct KitchenObjectSO_GameObject
    {
        public KitchenObjectSO kitchenObjectSO;
        public GameObject gameObject;
    }

    //The reference to the plateKitchenObject
    [SerializeField] private PlateKitchenObject plateKitchenObject;
    [SerializeField] private List<KitchenObjectSO_GameObject> kitchenObjectSOGameObjectList;

    private void Start()
    {
        //when an ingredient is added, the plate kitchen object fires an event for updating the visual
        plateKitchenObject.OnIngredientAdded += PlateKitchenObject_OnIngredientAdded;
    }

    private void PlateKitchenObject_OnIngredientAdded(object sender, PlateKitchenObject.OnObjectAddedEventArgs e)
    {
        //It receives the new kitchen ingredient that was added to the plate, then we will go through the list of kitchen objects and make the relevant game object to be active
        foreach(KitchenObjectSO_GameObject kitchenObjectSOGameObject in kitchenObjectSOGameObjectList)  
        {
            //If the new ingredient matches the one currently, then set it to be active
            if(e.kitchenObjectSO == kitchenObjectSOGameObject.kitchenObjectSO)
            {
                kitchenObjectSOGameObject.gameObject.SetActive(true);
            }
        }
    }
}
