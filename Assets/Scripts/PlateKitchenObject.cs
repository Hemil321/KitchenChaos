using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateKitchenObject : KitchenObject
{
    //First, define all the kitchen objects that can be added to a plate
    [SerializeField] private List<KitchenObjectSO> validKitchenObjectSOList;

    //This will storen the objects that are currently on the plate
    private List<KitchenObjectSO> kitchenObjectSOList;
    
    //This event will notify the plate visual and plate UI elements
    public event EventHandler<OnObjectAddedEventArgs> OnIngredientAdded;
    public class OnObjectAddedEventArgs : EventArgs
    {
        public KitchenObjectSO kitchenObjectSO;
    }
    private void Awake()
    {
        kitchenObjectSOList = new List<KitchenObjectSO>();
    }

    //We will try to add the ingredient that the player wants to add
    public bool TryAddIngredient(KitchenObjectSO kitchenObjectSO)
    {
        if(!validKitchenObjectSOList.Contains(kitchenObjectSO))
        {
            //We cannot add any object that is not considered valid
            return false;
        }
        if (kitchenObjectSOList.Contains(kitchenObjectSO))
        {
            //We can only add one object of a certain type, so we cannot add the ingredient if it exists in the list
            return false;
        }
        else
        {
            kitchenObjectSOList.Add(kitchenObjectSO);

            OnIngredientAdded?.Invoke(this, new OnObjectAddedEventArgs
            {
                kitchenObjectSO = kitchenObjectSO
            });
            return true;
        }
    }
    public List<KitchenObjectSO> GetKitchenObjectSOList()
    {
        return kitchenObjectSOList;
    }
}
