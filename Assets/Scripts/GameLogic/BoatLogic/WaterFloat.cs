using GameLogic.Extensions;
using GameLogic.WavesLogic;
using UnityEngine;

namespace GameLogic.BoatLogic
{
    [RequireComponent(typeof(Rigidbody))]
    public class WaterFloat : MonoBehaviour
    {
        [Header("Buoyancy Settings")]
        [SerializeField] private float _airDrag = 1; 
        [SerializeField] private float _waterDrag = 20; 
        [SerializeField] private bool _affectDirection = true; 
        [SerializeField] private bool _attachToSurface = false; 
        [SerializeField] private Transform[] _floatPoints; 
        [SerializeField] private float _power = 0.3f;

        private Rigidbody _rigidbody;
        private Waves _waves;

        private float _waterLine; 
        private Vector3[] _waterLinePoints; 

        private Vector3 _smoothVectorRotation; 
        private Vector3 _targetUp; 
        private Vector3 _centerOffset; 
    
        private Vector3 Center => transform.position + _centerOffset;

        void Awake()
        {
            InitializeComponents();
            InitializeWaterLinePoints();
        }
    
        private void InitializeComponents()
        {
            _waves = FindObjectOfType<Waves>();
            _rigidbody = GetComponent<Rigidbody>();
            _rigidbody.useGravity = false;
        }
    
        private void InitializeWaterLinePoints()
        {
            _waterLinePoints = new Vector3[_floatPoints.Length];
            for (int i = 0; i < _floatPoints.Length; i++)
                _waterLinePoints[i] = _floatPoints[i].position;
            _centerOffset = _waterLinePoints.GetCenter() - transform.position;
        }

        void FixedUpdate()
        {
            UpdateWaterLine();
            ApplyBuoyancy();
        }
    
        private void UpdateWaterLine()
        {
            var newWaterLine = 0f;
            var pointUnderWater = false;
        
            for (int i = 0; i < _floatPoints.Length; i++)
            {
                _waterLinePoints[i] = _floatPoints[i].position;
                _waterLinePoints[i].y = _waves.GetHeight(_floatPoints[i].position);
                newWaterLine += _waterLinePoints[i].y / _floatPoints.Length;
                if (_waterLinePoints[i].y > _floatPoints[i].position.y)
                    pointUnderWater = true;
            }

            _waterLine = newWaterLine;
            _targetUp = _waterLinePoints.GetNormal();

        
            AdjustRotation(pointUnderWater);
        }
    
        private void ApplyBuoyancy()
        {
            var gravity = Physics.gravity;
            _rigidbody.drag = _waterLine > Center.y ? _waterDrag : _airDrag;

            if (_attachToSurface && _waterLine > Center.y)
            {
                // Smoothly attaching to the water surface
                Vector3 desiredPosition = new Vector3(_rigidbody.position.x, _waterLine - _centerOffset.y, _rigidbody.position.z);
                Vector3 positionDelta = desiredPosition - _rigidbody.position;
                _rigidbody.AddForce(positionDelta * _power, ForceMode.VelocityChange); // Using VelocityChange for immediate velocity change
            }
            else
            {
                // Calculating the necessary additional speed
                var waterLineDelta = _waterLine - Center.y;
                if (_affectDirection)
                    gravity = _targetUp * -Physics.gravity.y;
        
                // Smoothly adding force to return to the water surface
                Vector3 velocityChange = (Vector3.up * waterLineDelta * 10f) - _rigidbody.velocity;// 0.9f
                _rigidbody.AddForce(velocityChange * _power, ForceMode.VelocityChange);
            }
        }
    
        private void AttachObjectToSurface()
        {
            _rigidbody.position = new Vector3(_rigidbody.position.x, _waterLine - _centerOffset.y, _rigidbody.position.z);
        }
    
        private void AdjustRotation(bool pointUnderWater)
        {
            if (pointUnderWater)
            {
                // Reduce the smoothing time for a quicker response to changes in water direction
                _targetUp = Vector3.SmoothDamp(transform.up, _targetUp, ref _smoothVectorRotation, 0.2f); // 0.2f
                _rigidbody.rotation = Quaternion.FromToRotation(transform.up, _targetUp) * _rigidbody.rotation;
            }
        }
    }
}