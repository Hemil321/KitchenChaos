using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

//A delivery manager script to handle the logic of spawning and removing recipe orders
public class DeliveryManager : MonoBehaviour
{
    //We will fire these events to let the UI update itself
    public event EventHandler OnRecipeSpawned;
    public event EventHandler OnRecipeCompleted;

    //We will fire these events for the sound
    public event EventHandler OnRecipeSuccess;
    public event EventHandler OnRecipeFailed;

    //We can only have one instance of the delivery manager
    public static DeliveryManager Instance { get; private set; }

    //The list of all the recipes that can be possible
    [SerializeField] private RecipeListSO recipeListSO;

    //The list of recipes that are waiting to be finished
    private List<RecipeSO> waitingRecipeSOList;

    private float spawnRecipeTimer;
    private float spawnRecipeTimerMax = 4f;
    private int waitingRecipeMax = 4;
    private int deliveredRecipesCount = 0;
    private void Awake()
    {
        Instance = this;
        waitingRecipeSOList = new List<RecipeSO>();
    }

    //In here, we will spawn a new recipe after the spawnRecipeTimerMax has passed
    private void Update()
    {
        spawnRecipeTimer -= Time.deltaTime;
        if(spawnRecipeTimer <= 0f)
        {
            spawnRecipeTimer = spawnRecipeTimerMax;
            if(KitchenGameManager.Instance.IsGamePlaying() && waitingRecipeSOList.Count < waitingRecipeMax)
            {
                //If a recipe can be spawned, we will spawn a random one from the global list of recipes
                RecipeSO waitingRecipeSO = recipeListSO.recipeSOList[UnityEngine.Random.Range(0, recipeListSO.recipeSOList.Count)];
                waitingRecipeSOList.Add(waitingRecipeSO);

                //We will invoke the event for the UI
                OnRecipeSpawned?.Invoke(this, EventArgs.Empty);
            }
        }
    }

    //This function handles the delivering of a recipe, when the player has delivered something on the counter
    public void DeliverRecipe(PlateKitchenObject plateKitchenObject)
    {
        for(int i = 0; i < waitingRecipeSOList.Count; i++)
        {
            RecipeSO waitingRecipeSO = waitingRecipeSOList[i];
            //If the number of ingredients on this recipe and on the plate are the same
            if(waitingRecipeSO.kitchenObjectSOList.Count == plateKitchenObject.GetKitchenObjectSOList().Count)
            {
                bool plateContentsMatchesRecipe = true;
                foreach(KitchenObjectSO recipeKitchenObjectSO in waitingRecipeSO.kitchenObjectSOList)
                {
                    //cycle through each object in the current recipe
                    bool ingredientFound = false;
                    foreach(KitchenObjectSO plateKitchenObjectSO in plateKitchenObject.GetKitchenObjectSOList())
                    {
                        //Cycle through each object on the plate
                        if(recipeKitchenObjectSO == plateKitchenObjectSO)
                        {
                            //ingredient found
                            ingredientFound = true;
                            break;
                        }
                    }
                    if(!ingredientFound)
                    {
                        plateContentsMatchesRecipe = false;
                        break;
                    }
                }
                if(plateContentsMatchesRecipe)
                {
                    //Player delivered the correct recipe
                    waitingRecipeSOList.RemoveAt(i);

                    //Invoke this event for the UI
                    OnRecipeCompleted?.Invoke(this, EventArgs.Empty);

                    //Invoke this event to play the sound
                    OnRecipeSuccess?.Invoke(this, EventArgs.Empty);

                    //Store the count of total delivered recipes to show at the game over screen
                    deliveredRecipesCount++;
                    return;
                }
            }
        }
        //Player did not deliver the correct recipe
        OnRecipeFailed?.Invoke(this, EventArgs.Empty);
    }

    public List<RecipeSO> GetWaitingRecipeSOList()
    {
        return waitingRecipeSOList;
    }

    public int GetDeliveredRecipesCount()
    {
        return deliveredRecipesCount;
    }
}
