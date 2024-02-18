using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//A delivery counter can only accept plates, so we will only destroy the object, if it is a plate
public class DeliveryCounter : BaseCounter
{
    [SerializeField] private DeliveryManager deliveryManager;

    public static DeliveryCounter Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }
    public override void Interact(Player player)
    {
        if(player.HasKitchenObject())
        {
            if(player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject))
            {
                DeliveryManager.Instance.DeliverRecipe(plateKitchenObject);
                player.GetKitchenObject().DestroySelf();
            }
        }
    }
}
