using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace V10
{
    [CreateAssetMenu()]
    public class RecipeSO : ScriptableObject
    {


        public List<KitchenObjectSO> kitchenObjectSOList;
        public string recipeName;


    }
}
