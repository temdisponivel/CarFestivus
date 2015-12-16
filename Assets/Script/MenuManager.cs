using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace Assets.Script
{
    public class MenuManager : MonoBehaviour
    {
        public Toggle _toggleSound = null;
        public Slider _sliderVolume = null;
        public Renderer _rederCar = null;
        public float _red = 0, _blue = 0, _green = 0;

        public void Play()
        {
            GameManager.Instance.PlayerColor = this._rederCar.material.color;
            GameManager.Instance.Sound = _toggleSound.isOn;
            GameManager.Instance.Volume = _sliderVolume.value;
            Application.LoadLevel("Game");
        }

        public void RedChange(Slider slider)
        {
            this._red = slider.value;
            this.SetColorCar();
        }

        public void GreenChange(Slider slider)
        {
            this._green = slider.value;
            this.SetColorCar();
        }

        public void BlueChange(Slider slider)
        {
            this._blue = slider.value;
            this.SetColorCar();
        }

        public void EnemyChange(Slider slider)
        {
            GameManager.Instance._enemyQuantity = (int) slider.value;
        }

        private void SetColorCar()
        {
            this._rederCar.material.color = new Color(this._red, this._green, this._blue);
        }
    }
}