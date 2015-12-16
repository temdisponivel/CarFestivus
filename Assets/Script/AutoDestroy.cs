using UnityEngine;
using System.Collections;


namespace Assets.Script
{
    public class AutoDestroy : MonoBehaviour
    {
        public float _seconds = 0f;
        private float _startTime;

        void Start()
        {
            this._startTime = Time.time;
        }

        void Update()
        {
            if (Time.time - this._startTime >= this._seconds)
            {
                GameObject.DestroyImmediate(this.gameObject);
            }
        }
    }
}