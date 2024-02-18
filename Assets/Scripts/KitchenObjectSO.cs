using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Sciptable object for all the kitchen objects
[CreateAssetMenu()]
public class KitchenObjectSO : ScriptableObject
{
    public Transform prefab;
    public Sprite sprite;
    public string objectName;
}
