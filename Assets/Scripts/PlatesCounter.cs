using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//In our architecture, a counter can only have one kitchen object
//But the plates counter can have multiple plates
//To solve this, we will spawn the plate visual on the counter, instead of the actual object
//And when the player wants a plate, we will generate the plate kitchen object
public class PlatesCounter : BaseCounter
{
    [SerializeField] KitchenObjectSO plateKitchenObjectSO;

    public event EventHandler OnPlateSpawned;
    public event EventHandler OnPlateRemoved;

    private float spawnPlateTimer;
    private float spawnPlateTimerMax = 4f;

    private int platesSpawnAmount;
    private int platesSpawnAmountMax = 4;
    private void Update()
    {
        spawnPlateTimer += Time.deltaTime;
        if(spawnPlateTimer > spawnPlateTimerMax)
        {
            spawnPlateTimer = 0;
            if(KitchenGameManager.Instance.IsGamePlaying() && platesSpawnAmount < platesSpawnAmountMax)
            {
                platesSpawnAmount++;
                //To notify the plate counter visual to update itself
                OnPlateSpawned?.Invoke(this, EventArgs.Empty);
            }
        }
    }

    public override void Interact(Player player)
    {
        if(!player.HasKitchenObject())
        {
            if(platesSpawnAmount > 0)
            {
                //There is at least one plate, so give it to the player
                platesSpawnAmount--;
                KitchenObject.SpawnKitchenObject(plateKitchenObjectSO, player);
                //To notify the plate counter visual to update itself
                OnPlateRemoved?.Invoke(this, EventArgs.Empty); 
            }
        }
    }
}
