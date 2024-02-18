using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//a kitchen object is any object that can be spawned in the game and can have a parent associated with it
public class KitchenObject : MonoBehaviour
{
    //We need to give a kitchen object scriptable object to every kitchen object
    [SerializeField] private KitchenObjectSO kitchenObjectSO;

    //To store the parent of a kitchen object
    private IKitchenObjectParent kitchenObjectParent;

    public KitchenObjectSO GetKitchenObjectSO()
    {
        return kitchenObjectSO;
    }
    //A kitchen object is responsible for setting its parent
    public void SetKitchenObjectParent(IKitchenObjectParent kitchenObjectParent)
    {
        //When changing the parent , we need to first notify the old parent that the object no longer belongs to it
        if(this.kitchenObjectParent != null)
        {
            this.kitchenObjectParent.ClearKitchenObject();
        }
        //change the parent to a new one
        this.kitchenObjectParent = kitchenObjectParent;
        //Tell the new parent to set its kitchen object to this kitchen object
        this.kitchenObjectParent.SetKitchenObject(this);

        //Update the visual of the object to be on top of the new clear counter
        transform.parent = kitchenObjectParent.GetKitchenObjectFollowTransform();
        transform.localPosition = Vector3.zero;
    }
    public IKitchenObjectParent GetKitchenObjectParent()
    {
        return this.kitchenObjectParent;
    }
    //A kitchen object is responsible for destroying itself
    public void DestroySelf()
    {
        GetKitchenObjectParent().ClearKitchenObject();
        Destroy(gameObject);
    }
    //A plate kitchen object works differently from a normal kitchen object, so we will implement the try get plate function here
    public bool TryGetPlate(out PlateKitchenObject plateKitchenObject)
    {
        if (this is PlateKitchenObject)
        {
            plateKitchenObject = this as PlateKitchenObject;
            return true;
        }
        else
        {
            plateKitchenObject = null;
            return false;
        }
    }
    //A kitchen object is also responsible for spawning itself to a parent
    public static void SpawnKitchenObject(KitchenObjectSO kitchenObjectSO, IKitchenObjectParent iKitchenObjectParent)
    {
        Transform kitchenObject = Instantiate(kitchenObjectSO.prefab);
        kitchenObject.GetComponent<KitchenObject>().SetKitchenObjectParent(iKitchenObjectParent);
    }
}
