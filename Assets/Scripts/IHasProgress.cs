using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//An inteface used by any counter that has progress attached to it
//An interface was needed because, multiple counters can have a progress element
public interface IHasProgress
{
    //This just has an event that is fired off for the UI elements
    public event EventHandler<OnProgressChangedEventArgs> OnProgressChanged;
    public class OnProgressChangedEventArgs : EventArgs
    {
        public float progressNormalized;
    }

}
