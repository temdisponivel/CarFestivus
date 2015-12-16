using UnityEngine;
using System.Collections;
using UnityEngine.UI;


namespace Assets.Script
{
    public class GameOverManager : MonoBehaviour
    {
        public Text _points = null;
        public Renderer _renderer = null;

        void Start()
        {
            _points.text += " " + GameManager.Instance._pontuation + " \n";
            _points.text += " You " + (GameManager.Instance.Win ? "WIN" : "LOSE") + "\n";
            _renderer.material.color = GameManager.Instance.PlayerColor;
        }

        public void Restart()
        {
            GameManager.Instance.Restart();
            Application.LoadLevel("Title");
        }
    }
}