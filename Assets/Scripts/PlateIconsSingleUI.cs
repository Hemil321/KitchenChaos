using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//For changing the sprite of the UI to be relevant to the kitchenObjectSO
public class PlateIconsSingleUI : MonoBehaviour
{
    [SerializeField] private Image image;
    public void SetKitchenObjectSO(KitchenObjectSO kitchenObjectSO)
    {
        image.sprite = kitchenObjectSO.sprite;
    }
}
