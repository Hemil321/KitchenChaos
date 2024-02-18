
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This is an interface used by any object that can be the parent of a kitchen object
//An interface was needed, because multiple type of objects can be the parent of a kitchen object
public interface IKitchenObjectParent
{
    public Transform GetKitchenObjectFollowTransform();
    public void SetKitchenObject(KitchenObject kitchenObject);
    public KitchenObject GetKitchenObject();
    public void ClearKitchenObject();
    public bool HasKitchenObject();
}
