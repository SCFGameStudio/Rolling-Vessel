using System;
using Controllers.Bullet;
using Controllers.Player;
using Data.UnityObjects;
using Data.ValueObjects;
using Keys;
using Signals;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Managers
{
    public class PlayerManager : MonoBehaviour
    {
        [SerializeField] private PlayerMovementController movementController;
        [SerializeField] private PlayerPhysicsController physicsController;
        [SerializeField] private PlayerMeshController meshController;
        [SerializeField] private BulletController bulletController;


        [ShowInInspector] private PlayerData _data;

        private void Awake()
        {
            _data = GetPlayerData();
            SendDataToControllers();
        }
        
        private PlayerData GetPlayerData()
        {
            return Resources.Load<CD_Player>("Data/CD_Player").Data;
        }
        
        private void SendDataToControllers()
        {
            movementController.GetMovementData(_data.MovementData);
            movementController.GetInvulnerabilityData(_data.InvulnerabilityData);
            movementController.GetRelentlessData(_data.RelentlessData);
            movementController.GetCannonData(_data.CannonData);
            bulletController.GetCannonData(_data.CannonData);
            physicsController.GetInvulnerabilityData(_data.InvulnerabilityData);
        }
        


        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
        }
        
        private void UnSubscribeEvents()
        {
        }

        private void OnDisable()
        {
            UnSubscribeEvents();
        }

        private void OnPlay()
        {
        }

        private void OnInputTaken()
        {
        }

        private void OnReset()
        {
            movementController.OnReset();
            meshController.OnReset();
            physicsController.OnReset();
        }
    }
}