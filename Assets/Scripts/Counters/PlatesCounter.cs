using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

namespace V10
{
    public class PlatesCounter : BaseCounter
    {


        public event EventHandler OnPlateSpawned;
        public event EventHandler OnPlateRemoved;


        [SerializeField] private KitchenObjectSO plateKitchenObjectSO;


        private float spawnPlateTimer;
        private float spawnPlateTimerMax = 4f;
        private int platesSpawnedAmount;
        private int platesSpawnedAmountMax = 4;


        private void Update()
        {
            if (!IsServer)
            {
                return;
            }

            spawnPlateTimer += Time.deltaTime;
            if (spawnPlateTimer > spawnPlateTimerMax)
            {
                spawnPlateTimer = 0f;

                if (GameManager.Instance.IsGamePlaying() && platesSpawnedAmount < platesSpawnedAmountMax)
                {
                    SpawnPlateServerRPC();
                }
            }
        }

        [ServerRpc]
        private void SpawnPlateServerRPC()
        {
            SpawnPlateClientRPC();
        }

        [ClientRpc]
        private void SpawnPlateClientRPC()
        {
            platesSpawnedAmount++;

            OnPlateSpawned?.Invoke(this, EventArgs.Empty);
        }

        public override void Interact(Player player)
        {
            if (!player.HasKitchenObject())
            {
                // Player is empty handed
                if (platesSpawnedAmount > 0)
                {
                    // There's at least one plate here
                    KitchenObject.SpawnKitchenObject(plateKitchenObjectSO, player);

                    InteractLogicServerRPC();
                }
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
            platesSpawnedAmount--;

            OnPlateRemoved?.Invoke(this, EventArgs.Empty);
        }


    }
}
