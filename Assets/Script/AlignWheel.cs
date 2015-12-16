using UnityEngine;
using System.Collections;

namespace Assets.Script
{
    public class AlignWheel : MonoBehaviour
    {
        public WheelCollider _wheelCollider;

        void Update()
        {
            WheelHit hit;
            if (_wheelCollider.GetGroundHit(out hit))
            {
                transform.position = _wheelCollider.transform.position -
                Vector3.up * ((hit.point - _wheelCollider.transform.position).magnitude - _wheelCollider.radius);
            }
            transform.Rotate(new Vector3(1, 0, 0), _wheelCollider.rpm * 2 * Mathf.PI / 60);
        }
    }
}