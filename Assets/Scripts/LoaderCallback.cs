using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace V10
{
    public class LoaderCallback : MonoBehaviour
    {


        private bool isFirstUpdate = true;

        private void Update()
        {
            if (isFirstUpdate)
            {
                isFirstUpdate = false;

                Loader.LoaderCallback();
            }
        }


    }
}
