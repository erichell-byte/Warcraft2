using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyProject
{
    public class SpawnManager : MonoBehaviour
    {
        [SerializeField] private GameObject charPrefab;

        private float _charX = -6.3f;
        private float _charY = 2f;

        private float _spawnHumanTime = 10f;

        public GameObject[] _childrenObj;

        private bool[] _isHave;

        // Start is called before the first frame update
        void Start()
        {
            StartCoroutine(SpawnEnemyHuman());
            _isHave = new bool[4];
            _isHave[0] = false;
            _isHave[1] = false;
            _isHave[2] = false;
            _isHave[3] = false;
        }

        IEnumerator SpawnEnemyHuman()
        {
            while (true)
            {
                Instantiate(charPrefab, new Vector3(_charX, _charY, 0), Quaternion.identity);
                _charX += 0.5f;
                if (_charX >= -3.6f)
                {
                    _charY -= 0.5f;
                    _charX = -6.3f;
                }

                if (_charY <= 0)
                {
                    _charX = -6.3f;
                    _charY = 2f;
                }

                yield return new WaitForSeconds(_spawnHumanTime);
            }
        }

        private void Update()
        {
            AddTimeSpawn();
        }

        void AddTimeSpawn()
        {
            for (int i = 0; i < 4; i++)
            {
                if (!_isHave[i])
                {
                    if (_childrenObj[i] == null)
                    {
                        _spawnHumanTime += 2.5f;
                        _isHave[i] = true;
                        Debug.Log("Spawn time is " +_spawnHumanTime);
                    }
                }
            }
        }
    }
}
