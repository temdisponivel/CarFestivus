using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

namespace Assets.Script
{
    [Serializable]
    public class GameManager
    {
        static private GameManager _instance;
        static public GameManager Instance { get { return GameManager._instance ?? (GameManager._instance = new GameManager()); } }

        public bool Sound { get; set; }
        public float Volume { get; set; }
        public Color PlayerColor { get; set; }
        public int _enemyQuantity = 5;
        public int _pontuation = 0;
        public bool Win { get; set; }

        public LinkedList<int> _unavableTarget = new LinkedList<int>();

        public Color GetRandomColor()
        {
            float red, blue, green;
            red = UnityEngine.Random.value;
            blue = UnityEngine.Random.value;
            green = UnityEngine.Random.value;
            return new Color(red, green, blue);
        }

        public GameObject GetNearTarget(GameObject gameObject)
        {
            float distance = float.PositiveInfinity;
            GameObject nearest = null;
            for (int i = 0; i < EnemyCar.Cars.Count; i++)
            {
                Car car = EnemyCar.Cars[i];
                float aux;
                if (car.gameObject == gameObject)
                {
                    continue;
                }
                if ((aux = Vector3.Distance(gameObject.transform.position, car.transform.position)) < distance)
                {
                    nearest = car.gameObject;
                    distance = aux;
                }
            }
            return nearest;
        }

        public GameObject GetRandomTarget(GameObject gameObject)
        {
            int index = UnityEngine.Random.Range(0, Car.Cars.Count);
            if (this._unavableTarget.Contains(index) || Car.Cars[index].gameObject == gameObject)
            {
                return this.GetNearTarget(gameObject);
            }
            else
            {
                this._unavableTarget.AddLast(index);
                return Car.Cars[index].gameObject;
            }
        }

        public Vector3 GetRandomPosition()
        {
            return new Vector3() { x = UnityEngine.Random.Range(10, 90), y = 1, z = UnityEngine.Random.Range(10, 90) };
        }

        public IEnumerator EndGame()
        {
            yield return new WaitForSeconds(1);
            Application.LoadLevel("EndGame");
        }

        public void Restart()
        {
            this.Win = false;
            this._pontuation = 0;
            this._enemyQuantity = 5;
            this.Sound = true;
            this.Volume = 0.5f;
        }
    }
}