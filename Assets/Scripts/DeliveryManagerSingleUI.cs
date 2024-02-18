using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

//This script is responsible for updating all the UI template elements according to the object
public class DeliveryManagerSingleUI : MonoBehaviour
{
    //All the UI fields that are going to be changed are recieved as a reference
    [SerializeField] private TextMeshProUGUI recipeNameText;
    [SerializeField] private Transform iconTemplate;
    [SerializeField] private Transform iconContainer;

    private void Awake()
    {
        //We will have to set the template object inactive
        iconTemplate.gameObject.SetActive(false);
    }
    public void SetRecipeSO(RecipeSO recipeSO)
    {
        //This function sets the text to the recipeSO text and all the icons of a particular recipe
        recipeNameText.text = recipeSO.recipeName;

        foreach(Transform child in iconContainer)
        {
            if (child == iconTemplate) continue;
            Destroy(child.gameObject);
        }

        foreach(KitchenObjectSO kitchenObjectSO in recipeSO.kitchenObjectSOList) 
        {
            //Instantiate function spawns the object iconTemplate and sets its parent as iconContainer
            //We will set it to active and set the icons
            Transform iconTransform = Instantiate(iconTemplate, iconContainer);
            iconTransform.gameObject.SetActive(true);
            iconTransform.GetComponent<Image>().sprite = kitchenObjectSO.sprite;
        }
    }
}
