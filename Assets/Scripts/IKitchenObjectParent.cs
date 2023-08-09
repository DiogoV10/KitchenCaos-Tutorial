using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

namespace V10
{
    public interface IKitchenObjectParent
    {
        public Transform GetKitchenObjectFollowTransform();

        public void SetKitchenObject(KitchenObject kitchenObject);

        public KitchenObject GetKitchenObject();

        public void ClearKitchenObject();

        public bool HasKitchenObject();

        public NetworkObject GetNetworkObject();
    }
}
