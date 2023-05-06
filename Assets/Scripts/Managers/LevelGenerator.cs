using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Managers
{
    public class LevelGenerator : MonoBehaviour
    {
        private const float playerDistanceSpawnLevelPart = 300f;
        private const float levelPartDestroyDistance = 800f;
        private Vector3 _lastEndPosition;

        [SerializeField] private Transform levelPartStart;
        [SerializeField] private List<Transform> levelPartList;
        [SerializeField] private GameObject player;
        
        [ShowInInspector]private List<Transform> _spawnedLevelParts = new();

        private void Awake()
        {
            _lastEndPosition = levelPartStart.Find("EndPosition").position;
            const int startingSpawnLevelParts = 2;
            for (var i = 0; i < startingSpawnLevelParts; i++)
            {
                SpawnLevelPart();
            }
        }

        private void Update()
        {
            if (Vector3.Distance(player.transform.position, _lastEndPosition) < playerDistanceSpawnLevelPart)
            {
                SpawnLevelPart();
            }
            
            for (var i = 0; i < _spawnedLevelParts.Count; i++)
            {
                var levelPart = _spawnedLevelParts[i];
                if (levelPart != null && Vector3.Distance(player.transform.position, levelPart.position) > levelPartDestroyDistance)
                {
                    _spawnedLevelParts.RemoveAt(i);
                    Destroy(levelPart.gameObject);
                }
            }
        }

        private void SpawnLevelPart()
        {
            var chosenLevelPart = levelPartList[Random.Range(0, levelPartList.Count)];
            var lastLevelPartTransform = SpawnLevelPart(chosenLevelPart, _lastEndPosition);
            _lastEndPosition = lastLevelPartTransform.Find("EndPosition").position;
            _spawnedLevelParts.Add(lastLevelPartTransform);
        }

        private static Transform SpawnLevelPart(Transform levelPart,Vector3 spawnPosition)
        {
            var levelPartTransform = Instantiate(levelPart, spawnPosition, Quaternion.identity);
            return levelPartTransform;
        }
    }
}