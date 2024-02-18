using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

//This scriptable object stores a recipe
[CreateAssetMenu()]
public class CuttingRecipeSO : ScriptableObject
{
    public KitchenObjectSO input;
    public KitchenObjectSO output;
    public int cuttingProgressMax;
}
