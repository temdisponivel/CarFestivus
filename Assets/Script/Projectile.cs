using UnityEngine;
using System.Collections;

namespace Assets.Script
{
    public class Projectile : MonoBehaviour
    {
        public int _velocity = 0;
        public int _power = 1;
        public Renderer _renderer = null;
        public ParticleSystem _particleSystem = null;
        public int _points;
        public bool _pontuation;

        public void Start()
        {
            this._renderer = this.GetComponent<Renderer>();
            this._particleSystem = this.GetComponent<ParticleSystem>();
        }

        public void Update()
        {
            this.transform.position += this.transform.forward * _velocity * Time.deltaTime;
        }

        public void OnTriggerEnter(Collider collider)
        {
            _particleSystem.Play();
            Color color = this._renderer.material.color;
            this._particleSystem.startColor = new Color(color.r, color.g, color.b);
            this._velocity = 0;
            this.GetComponent<AutoDestroy>().enabled = true;
            if (GameManager.Instance.Sound)
            {
                AudioSource audio = this.GetComponent<AudioSource>();
                audio.volume = GameManager.Instance.Volume;
                audio.Play();
            }
        }
    }
}