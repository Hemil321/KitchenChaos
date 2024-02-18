using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : BaseCounter
{
    public override void Interact(Player player)
    {
        //Check if there is some object already on the counter
        if (!HasKitchenObject())
        {
            //The counter is empty, so we will check if the player is carrying any object
            if (player.HasKitchenObject())
            {
                //The player is carrying an object, so we will put it on the counter
                player.GetKitchenObject().SetKitchenObjectParent(this);
            }
            else
            {
                //Player is not carrying anything, so nothing happens
            }
        }
        //There is something on the counter, which is not a plate
        else
        {
            //Check if the player is carrying an object
            if (player.HasKitchenObject())
            {
                //check if the player is carrying a plate
                if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject))
                {
                    //Player is holding a plate, so check if that ingredient can be added on the plate
                    if (plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO()))
                    {
                        GetKitchenObject().DestroySelf();
                    }
                }
                else
                {
                    //Player is not carrying a plate, but something else, so we will check if the object on the counter is a plate
                    if (GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObjectOnCounter))
                    {
                        //It is a plate, so let's check if the player has something that can be put on it
                        if (plateKitchenObjectOnCounter.TryAddIngredient(player.GetKitchenObject().GetKitchenObjectSO()))
                        {
                            player.GetKitchenObject().DestroySelf();
                        }
                    }
                }
            }
            else
            {
                //The player is not carrying anything, so we will give the item on the counter to the player
                GetKitchenObject().SetKitchenObjectParent(player);
            }
        }
    }
}
