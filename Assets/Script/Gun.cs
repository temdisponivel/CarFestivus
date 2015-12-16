using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script
{
    /// <summary>
    /// Class that represents a gun in the game.
    /// </summary>
    [Serializable]
    public class Gun
    {
        public GameObject _projectile = null;
        public int _shootsPerSecond = 0;
        public float Delay { get { return 1f / _shootsPerSecond; } }
        private float _lastShoot = 0f;
        public Color _color;

        public Gun(GameObject projectile)
        {
            this._projectile = projectile;
        }

        public Gun() {}

        /// <summary>
        /// Return true if this gun can shoot. False otherwise.
        /// </summary>
        public bool CanShoot { get { return Time.time - this._lastShoot >= this.Delay && this._projectile; } }

        /// <summary>
        /// Instantiate a new projectile with the transform direction and rotarion.
        /// </summary>
        public void Shoot(Transform transform)
        {
            if (this._projectile == null) { return; }
            (GameObject.Instantiate(this._projectile, transform.position, transform.rotation) as GameObject).GetComponent<Renderer>().material.color = this._color;
            this._lastShoot = Time.time;
        }
    }
}
