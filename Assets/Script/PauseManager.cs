using UnityEngine;
using System.Collections;
using UnityEngine.UI;


namespace Assets.Script
{
    public class PauseManager : MonoBehaviour
    {
        public GameObject _pauseMenu = null;
        public AudioSource _background = null;
        public Text _points = null;
        public Toggle _toggleSound = null;
        public Slider _sliderVolume = null;
        private const string _pauseInput = "Pause";

        void Start()
        {
            _background.volume = GameManager.Instance.Volume;
            if (GameManager.Instance.Sound)
            {
                _background.Play();
            }
            _points.color = new Color(GameManager.Instance.PlayerColor.r, GameManager.Instance.PlayerColor.g, GameManager.Instance.PlayerColor.b);
        }

        void Update()
        {
            if (Input.GetButtonDown(_pauseInput))
            {
                if (Time.timeScale == 0f)
                {
                    this.Continue();
                    return;
                }

                Time.timeScale = 0f;
                _pauseMenu.SetActive(true);
                _sliderVolume.value = GameManager.Instance.Volume;
                _background.Pause();
            }

            _points.text = GameManager.Instance._pontuation.ToString();
        }

        public void Continue()
        {
            Time.timeScale = 1;
            _pauseMenu.SetActive(false);
            GameManager.Instance.Sound = _toggleSound.isOn;
            GameManager.Instance.Volume = _sliderVolume.value;
            _background.UnPause();
            _background.volume = GameManager.Instance.Volume;
            if (!GameManager.Instance.Sound)
            {
                _background.Pause();
            }
            else
            {
                _background.UnPause();
            }
        }

        public void MainMenu()
        {
            Time.timeScale = 1;
            Application.LoadLevel("Title");
        }
    }
}