using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Managers
{
    public class LevelGenerator : MonoBehaviour
    {
        private const float PlayerDistanceSpawnLevelPart = 100f;
        private const float LevelPartDestroyDistance = 800f;

        [SerializeField] private Transform levelPartStart;
        [SerializeField] private List<Transform> levelPartList;
        [SerializeField] private GameObject player;

        private Vector3 _lastEndPosition;
        [ShowInInspector]private List<Transform> _spawnedLevelParts = new List<Transform>();

        private void Awake()
        {
            _lastEndPosition = levelPartStart.Find("EndPosition").position;
            int startingSpawnLevelParts = 2;
            for (int i = 0; i < startingSpawnLevelParts; i++)
            {
                SpawnLevelPart();
            }
        }

        private void Update()
        {
            if (Vector3.Distance(player.transform.position, _lastEndPosition) < PlayerDistanceSpawnLevelPart)
            {
                SpawnLevelPart();
            }
            
            for (int i = 0; i < _spawnedLevelParts.Count; i++)
            {
                Transform levelPart = _spawnedLevelParts[i];
                if (levelPart != null && Vector3.Distance(player.transform.position, levelPart.position) > LevelPartDestroyDistance)
                {
                    _spawnedLevelParts.RemoveAt(i);
                    Destroy(levelPart.gameObject);
                }
            }
        }

        private void SpawnLevelPart()
        {
            Transform chosenLevelPart = levelPartList[Random.Range(0, levelPartList.Count)];
            Transform lastLevelPartTransform = SpawnLevelPart(chosenLevelPart, _lastEndPosition);
            _lastEndPosition = lastLevelPartTransform.Find("EndPosition").position;
            
            _spawnedLevelParts.Add(lastLevelPartTransform);
        }

        private Transform SpawnLevelPart(Transform levelPart,Vector3 spawnPosition)
        {
            Transform levelPartTransform = Instantiate(levelPart, spawnPosition, Quaternion.identity);
            return levelPartTransform;
        }
    }
}