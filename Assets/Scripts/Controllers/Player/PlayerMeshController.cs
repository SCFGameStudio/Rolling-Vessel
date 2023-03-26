using System;
using UnityEngine;

namespace Controllers.Player
{
    public class PlayerMeshController : MonoBehaviour
    {
        #region Instance

        

        
        private static PlayerMeshController _instance;

        public static PlayerMeshController Instance
        {
            get
            {
                if (_instance == null)
                {
                    Debug.LogError("PlayerMeshController is null");
                }

                return _instance;
            }
        }
        #endregion

        //[SerializeField] private PlayerManager manager;
        [SerializeField] private ParticleSystem waterParticle;
        [SerializeField] private ParticleSystem fireParticle;
        [SerializeField] private ParticleSystem destroyParticle;
        [SerializeField] private new Renderer renderer;
        
        
        private void Awake()
        {
            _instance = this;
        }

        public void PlayWaterParticle()
        {
            waterParticle.Play();
        }
        
        public void PlayFireParticle()
        {
            fireParticle.Play();
        }
        
        public void PlayDestroyParticle()
        {
            destroyParticle.Play();
        }
    }
}