using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace Assets.Script
{
    public class PlayerCar : Car
    {
        public Camera _cameraFirstPerson = null;
        public Camera _cameraGod = null;
        public Image _image = null;
        protected const string _shootInput = "Shoot";
        protected const string _cameraMovimentInput = "CameraMoviment";
        protected const string _horizontalInput = "Horizontal";
        protected const string _verticalInput = "Vertical";
        protected const string _brakeInput = "Brake";
        protected const string _boostInput = "Boost";
        protected Vector3 _initialScaleimage = default(Vector3);

        public override void Start()
        {
            this._color = GameManager.Instance.PlayerColor; 
            base.Start();
            _initialScaleimage = _image.transform.localScale;
        }
        
        public override void Update()
        {
            this.HandleCamera();
            base.Update();
            float percentLife = this._helth / 100f;
            _image.transform.localScale = new Vector3(_initialScaleimage.x * percentLife, _initialScaleimage.x, _initialScaleimage.x);
        }

        public override void GetDirections(out float horizontal, out float vertical, out bool brake)
        {
            horizontal = Input.GetAxis(_horizontalInput);
            vertical = Input.GetAxis(_verticalInput);

            brake = Input.GetButton(_brakeInput);

            if (Input.GetButton(_boostInput))
            {
                vertical *= 2;
            }
        }

        public override bool ShouldShoot()
        {
            return Input.GetButton(_shootInput);
        }

        override public void AdjustGun()
        {
            Ray rayMouse = this._cameraFirstPerson.ScreenPointToRay(Input.mousePosition);
            Vector3 look = this.transform.position - rayMouse.direction * 1000;
            RaycastHit hit;
            float angleBase;
            if (Physics.Raycast(rayMouse, out hit, 1000))
            {
                look = this._shootPoint.transform.position - hit.point;
            }
            angleBase = Vector3.Angle(look, this._shootPoint.transform.right);
            this._shootPoint.transform.Rotate(new Vector3(0, angleBase - 90, 0) * 10 * Time.deltaTime);
        }

        public void HandleCamera()
        {
            if (Input.GetButton(_cameraMovimentInput))
            {
                this._cameraGod.gameObject.SetActive(true);
                this._cameraFirstPerson.gameObject.SetActive(false);
            }
            else
            {
                this._cameraGod.gameObject.SetActive(false);
                this._cameraFirstPerson.gameObject.SetActive(true);
            }
        }

        public override void Die()
        {
            base.Die();
            GameManager.Instance.Win = false;
            this.StartCoroutine(GameManager.Instance.EndGame());
        }
    }
}