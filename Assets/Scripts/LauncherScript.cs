using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LauncherScript : MonoBehaviour {
    [SerializeField] SimulatedScene _simulatedScene;    
    
    [SerializeField] AirmailPackage _projectile;
    [SerializeField] float _force;

    [SerializeField] Transform _originPoint;
    [SerializeField] private float _pitchSpeed = 5f;
    [SerializeField] private float _yawSpeed = 5f;
    [SerializeField] private float _forceSpeed = 5f;

    private void FixedUpdate()
    {
        _simulatedScene.SimulatedTrajectory(_projectile, _originPoint.position, _force * _originPoint.forward);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            AirmailPackage projectile = Instantiate(_projectile, _originPoint.position, Quaternion.identity);

            projectile.ApplyImpulse(_force * _originPoint.forward);
        }

        float pitchAmount = Input.GetAxis("Vertical") * Time.deltaTime * _pitchSpeed;
        float yawAmount = Input.GetAxis("Horizontal") * Time.deltaTime * _yawSpeed;

        transform.Rotate(0, yawAmount, pitchAmount);

        if (Input.GetKey(KeyCode.R))
        {
            _force += _forceSpeed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.F))
        {
            _force -= _forceSpeed * Time.deltaTime;
        }
    }
}
