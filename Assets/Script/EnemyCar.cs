using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Assets.Script
{
    public class EnemyCar : Car
    {
        public GameObject _target = null;
        public bool InWallFront { get; set; }
        public bool InWallBack { get; set; }

        public override void Start()
        {
            this._color = GameManager.Instance.GetRandomColor(); ;
            base.Start();
            this._target = GameManager.Instance.GetRandomTarget(this.gameObject);
        }

        public override void GetDirections(out float horizontal, out float vertical, out bool brake)
        {
            horizontal = 0;
            vertical = 0;
            brake = false;

            if (_target == null)
            {
                _target = GameManager.Instance.GetNearTarget(this.gameObject);
            }

            Vector3 nextPoint = Vector3.Lerp(this.transform.position, this._target.transform.position, this._velocity * Time.deltaTime);
            float distance = 0, angle = 0;
            distance = Vector3.Distance(this.transform.position, nextPoint);
            angle = Vector3.Angle(this._target.transform.position, this.transform.right) - 90;
            if (distance < 0 || this.InWallFront)
            {
                vertical = -1;
            }
            else if (distance > 0 || this.InWallBack)
            {
                vertical = 1;
            }
            
            if (angle < 0)
            {
                horizontal = -1;
            }
            else
            {
                horizontal = 1;
            }
        }

        public override bool ShouldShoot()
        {
            if (this._target == null)
            {
                return false;
            }
            Ray ray = new Ray(this._shootPoint.transform.position, this._shootPoint.transform.position - this._target.transform.position);
            RaycastHit rayHit;
            return Physics.Raycast(ray, out rayHit, 1000) && rayHit.collider.gameObject != this.gameObject && rayHit.collider.gameObject.tag == "Car"; ;
        }

        override public void AdjustGun()
        {
            if (this._target != null)
            {
                this._shootPoint.transform.LookAt(this._target.transform);
            }
        }
        
        override public void OnTriggerEnter(Collider collider)
        {
            if (collider.gameObject.tag != "Car")
            {
                base.OnTriggerEnter(collider);
                return;
            }
            
            if (collider.gameObject.tag == "Car")
            {
                _target = collider.gameObject;
            }

            this.AdjustGun();
        }

        public void OnCollisionEnter(Collision collision)
        {
            Debug.Log("ALO");
            if (collision.gameObject.tag == "Wall")
            {
                Vector3 localCollision = this.transform.InverseTransformPoint(collision.contacts[0].point);
                if (Vector3.Distance(localCollision, this._shootPoint.transform.position) < Vector3.Distance(localCollision, this.transform.position))
                {
                    this.InWallFront = true;
                    this.InWallBack = false;
                }
                else
                {
                    this.InWallFront = false;
                    this.InWallBack = true;
                }
            }
            else
            {
                this.InWallBack = false;
                this.InWallFront = false;
            }
        }
    }
}