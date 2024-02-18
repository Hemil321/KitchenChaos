using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

//Scriptable Object for the burning recipe
[CreateAssetMenu()]
public class BurningRecipeSO : ScriptableObject
{
    public KitchenObjectSO input;
    public KitchenObjectSO output;
    public float burningTimerMax;
}
