using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBarUI : MonoBehaviour
{
    //Progress bar needs to work with any object that has IProgress inherited, so as a reference to the object, we need to have a game object as the reference
    [SerializeField] private GameObject hasProgressGameObject;
    [SerializeField] private Image barImage;

    //To get the events associated with the hasProgress interface
    private IHasProgress hasProgress;

    private void Start()
    {
        //We have to assign the hasProgress as the gameObject's hasProgress component
        hasProgress = hasProgressGameObject.GetComponent<IHasProgress>();

        if(hasProgress == null)
        {
            Debug.LogError("Object doesn't have IHasProgress Component!");
        }
        hasProgress.OnProgressChanged += HasProgress_OnProgressChanged;

        barImage.fillAmount = 0f;

        Hide();
    }

    private void HasProgress_OnProgressChanged(object Sender, IHasProgress.OnProgressChangedEventArgs e)
    {
        barImage.fillAmount = e.progressNormalized;

        if(e.progressNormalized == 0f || e.progressNormalized == 1f) 
        {
            Hide();
        }
        else
        {
            Show();
        }
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
