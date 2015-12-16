using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script
{
    abstract public class Car : MonoBehaviour
    {
        #region properties
        readonly static public List<Car> Cars = new List<Car>();
        public int _helth = 100;
        public WheelCollider[] _wheelCollidersLeft = null;
        public WheelCollider[] _wheelCollidersRight = null;
        public Color _color;
        public Renderer _renderer;
        public GameObject _shootPoint = null;
        public int _velocity = 10;
        public int _brakeForce = 10;
        protected Rigidbody _rigidBody = null;
        public Gun _gun = null;
        public AudioSource _audio = null;
        public bool Dead { get; set; }

        #endregion

        public void Awake()
        {
            this._rigidBody = this.GetComponent<Rigidbody>();
            this._audio = this.GetComponent<AudioSource>();
            Car.Cars.Add(this);
        }
        
        virtual public void Start()
        {
            this._renderer.material.color = this._color;
            this._gun._color = this._color;

            if (GameManager.Instance.Sound)
            {
                this._audio.Play();
            }
        }

        virtual public void Update()
        {
            if (this.Dead)
            {
                return;
            }
            this.AdjustGun();
            this.HandleGun();
            if (this._helth <= 0)
            {
                this.Die();
            }
        }

        public void FixedUpdate()
        {
            if (this.Dead)
            {
                return;
            }
            float vertical, horizontal;
            bool brake;
            this.GetDirections(out horizontal, out vertical, out brake);

            foreach (WheelCollider wheel in this._wheelCollidersLeft)
            {
                wheel.motorTorque = (this._velocity * vertical + horizontal * this._velocity);
                if (brake)
                {
                    wheel.brakeTorque = _brakeForce;
                }
                else
                {
                    wheel.brakeTorque = 0;
                }
            }

            foreach (WheelCollider wheel in this._wheelCollidersRight)
            {
                wheel.motorTorque = (this._velocity * vertical - horizontal * this._velocity);
                if (brake)
                {
                    wheel.brakeTorque = _brakeForce;
                }
                else
                {
                    wheel.brakeTorque = 0;
                }
            }

            if (GameManager.Instance.Sound)
            {
                this._audio.volume = Mathf.Clamp(GameManager.Instance.Volume * (Math.Abs(vertical) == 0 ? Math.Abs(horizontal) : Mathf.Abs(vertical)), 0, 1);
            }
            else
            {
                this._audio.Pause();
            }
        }

        virtual public void OnTriggerEnter(Collider collider)
        {
            Projectile projectile = collider.gameObject.GetComponent<Projectile>();
            if (projectile != null)
            {
                this._helth -= projectile._power;
                if (projectile._pontuation)
                {
                    GameManager.Instance._pontuation += projectile._points;
                }
            }
        }

        /// <summary>
        /// Set the direction to whitch this car should go.
        /// </summary>
        /// <param name="horizontal">Horizontal multiplier.</param>
        /// <param name="vertical">Vertical multiplier.</param>
        /// <param name="brake">Brake or not</param>
        abstract public void GetDirections(out float horizontal, out float vertical, out bool brake);

        /// <summary>
        /// Adjust the gun position to point to some place to shoot.
        /// </summary>
        abstract public void AdjustGun();

        /// <summary>
        /// If this car should shoot.
        /// </summary>
        /// <returns>True if it is to shoot.</returns>
        abstract public bool ShouldShoot();

        /// <summary>
        /// Called when this car dies.
        /// </summary>
        virtual public void Die()
        {
            if (this.Dead)
            {
                return;
            }
            foreach (WheelCollider wheel in this._wheelCollidersLeft)
            {
                wheel.enabled = false;
                wheel.gameObject.transform.parent = null;
            }
            foreach (WheelCollider wheel in this._wheelCollidersRight)
            {
                wheel.enabled = false;
                wheel.gameObject.transform.parent = null;
            }
            this._rigidBody.AddForce(Vector3.up * 1000, ForceMode.Acceleration);
            Car.Cars.Remove(this);
            GameObject.Destroy(this.gameObject, 2);
            this.Dead = true;
        }
        
        /// <summary>
        /// Method that handle the gun input and shoot.
        /// </summary>
        private void HandleGun()
        {
            if (!this._gun.CanShoot) { return; }

            if (this.ShouldShoot())
            {
                this._gun.Shoot(this._shootPoint.transform);
            }
        }        
    }
}
