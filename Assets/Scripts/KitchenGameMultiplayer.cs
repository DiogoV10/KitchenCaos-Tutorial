using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

namespace V10
{
    public class KitchenGameMultiplayer : NetworkBehaviour
    {
        

        public static KitchenGameMultiplayer Instance { get; private set; }


        [SerializeField] private KitchenObjectListSO KitchenObjectListSO;


        private void Awake()
        {
            Instance = this;
        }


        public void SpawnKitchenObject(KitchenObjectSO kitchenObjectSO, IKitchenObjectParent kitchenObjectParent)
        {
            SpawnKitchenObjectServerRPC(GetKitchenObjectSOIndex(kitchenObjectSO), kitchenObjectParent.GetNetworkObject());
        }

        [ServerRpc(RequireOwnership =false)]
        private void SpawnKitchenObjectServerRPC(int kitchenObjectSOIndex, NetworkObjectReference kitchenObjectParentNetworkObjectReference)
        {
            KitchenObjectSO kitchenObjectSO = GetKitchenObjectSOFromIndex(kitchenObjectSOIndex);

            Transform kitchenObjectTransform = Instantiate(kitchenObjectSO.prefab);

            NetworkObject kitchenObjectNetworkObject = kitchenObjectTransform.GetComponent<NetworkObject>();
            kitchenObjectNetworkObject.Spawn(true);

            KitchenObject kitchenObject = kitchenObjectTransform.GetComponent<KitchenObject>();

            kitchenObjectParentNetworkObjectReference.TryGet(out NetworkObject kitchenObjectParentNetworkObject);
            IKitchenObjectParent kitchenObjectParent = kitchenObjectParentNetworkObject.GetComponent<IKitchenObjectParent>();

            kitchenObject.SetKitchenObjectParent(kitchenObjectParent);
        }

        private int GetKitchenObjectSOIndex(KitchenObjectSO kitchenObjectSO)
        {
            return KitchenObjectListSO.kitchenObjectSOList.IndexOf(kitchenObjectSO);
        }

        private KitchenObjectSO GetKitchenObjectSOFromIndex(int kitchenObjectSOIndex)
        {
            return KitchenObjectListSO.kitchenObjectSOList[kitchenObjectSOIndex];
        }

        public void DestroyKitchenObject(KitchenObject kitchenObject)
        {
            DestroyKitchenObjectServerRpc(kitchenObject.NetworkObject);
        }

        [ServerRpc(RequireOwnership = false)]
        private void DestroyKitchenObjectServerRpc(NetworkObjectReference kitchenObjectNetworkObjectReference)
        {
            kitchenObjectNetworkObjectReference.TryGet(out NetworkObject kitchenObjectNetworkObject);
            KitchenObject kitchenObject = kitchenObjectNetworkObject.GetComponent<KitchenObject>();

            ClearKitchenObjectOnParentClientRpc(kitchenObjectNetworkObjectReference);

            kitchenObject.DestroySelf();
        }

        [ClientRpc]
        private void ClearKitchenObjectOnParentClientRpc(NetworkObjectReference kitchenObjectNetworkObjectReference)
        {
            kitchenObjectNetworkObjectReference.TryGet(out NetworkObject kitchenObjectNetworkObject);
            KitchenObject kitchenObject = kitchenObjectNetworkObject.GetComponent<KitchenObject>();

            kitchenObject.ClearKitchenObjectOnParent();
        }


    }
}
