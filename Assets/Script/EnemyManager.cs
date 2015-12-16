using UnityEngine;
using System.Collections;

namespace Assets.Script
{
    public class EnemyManager : MonoBehaviour
    {
        public GameObject _enemyCar;

        void Start()
        {
            if (GameManager.Instance._enemyQuantity % 2 != 0)
            {
                GameManager.Instance._enemyQuantity++;
            }

            for (int i = 0; i < GameManager.Instance._enemyQuantity; i++)
            {
                GameObject.Instantiate(_enemyCar, GameManager.Instance.GetRandomPosition(), Quaternion.identity);
            }
        }

        void Update()
        {
            if (Car.Cars.Count == 1)
            {
                GameManager.Instance.Win = true;
                this.StartCoroutine(GameManager.Instance.EndGame());
            }
        }
    }
}