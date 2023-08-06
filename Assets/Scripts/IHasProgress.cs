using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace V10
{
    public interface IHasProgress
    {


        public event EventHandler<OnProgressChangedEventArgs> OnProgressChanged;
        public class OnProgressChangedEventArgs : EventArgs
        {
            public float progressNormalized;
        }


    }
}
