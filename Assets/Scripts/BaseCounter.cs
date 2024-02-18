using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//all the counters are inherited from the base counter to make the code better and this also inherits the kitchen object parent, because all the counters can be the parent of objects
public class BaseCounter : MonoBehaviour, IKitchenObjectParent
{
    //this is the point where the object is to be spawned on the counter
    [SerializeField] private Transform counterTopPoint;

    private KitchenObject kitchenObject;

    public static event EventHandler OnDroppedSomething;

    public static void ResetStaticData()
    {
        OnDroppedSomething = null;
    }
    public virtual void Interact(Player player)
    {

    }
    public virtual void InteractAlternate(Player player) 
    {
        
    }

    public Transform GetKitchenObjectFollowTransform()
    {
        return counterTopPoint;
    }
    public void SetKitchenObject(KitchenObject kitchenObject)
    {
        this.kitchenObject = kitchenObject;

        if(kitchenObject != null)
        {
            OnDroppedSomething?.Invoke(this, EventArgs.Empty);
        }
    }
    public KitchenObject GetKitchenObject()
    {
        return kitchenObject;
    }
    public void ClearKitchenObject()
    {
        kitchenObject = null;
    }
    public bool HasKitchenObject()
    {
        return kitchenObject != null;
    }
}
