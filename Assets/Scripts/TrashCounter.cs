using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//A trash counter is just a counter that will delete the object that the player has
public class TrashCounter : BaseCounter
{
    public static event EventHandler OnAnyObjectTrashed;

    new public static void ResetStaticData()
    {
        OnAnyObjectTrashed = null;
    }
    public override void Interact(Player player)
    {
        if(player.HasKitchenObject()) 
        {
            player.GetKitchenObject().DestroySelf();

            OnAnyObjectTrashed?.Invoke(this, EventArgs.Empty);
        }
    }
}
