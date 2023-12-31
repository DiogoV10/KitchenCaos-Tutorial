using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

namespace V10
{
    public class ContainerCounter : BaseCounter
    {
        public event EventHandler OnPlayerGrabbedObject;

        [SerializeField] private KitchenObjectSO kitchenObjectSO;


        public override void Interact(Player player)
        {
            if (!player.HasKitchenObject())
            {
                // Player is not carrying anything
                KitchenObject.SpawnKitchenObject(kitchenObjectSO, player);

                InteractLogicServerRPC();
            }            
        }

        [ServerRpc(RequireOwnership = false)]
        private void InteractLogicServerRPC()
        {
            InteractLogicClientRPC();
        }

        [ClientRpc]
        private void InteractLogicClientRPC()
        {
            OnPlayerGrabbedObject?.Invoke(this, EventArgs.Empty);
        }


    }
}
