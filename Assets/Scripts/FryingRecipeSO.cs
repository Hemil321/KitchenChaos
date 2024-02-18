using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

//Scriptable objects for all the frying recipes
[CreateAssetMenu()]
public class FryingRecipeSO : ScriptableObject
{
    public KitchenObjectSO input;
    public KitchenObjectSO output;
    public float fryingTimerMax;
}
