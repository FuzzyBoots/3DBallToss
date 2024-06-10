using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class LauncherScript : MonoBehaviour {
    [SerializeField] SimulatedScene _simulatedScene;    
    
    [SerializeField] AirmailPackage _projectile;

    [SerializeField] Transform _originPoint;

    [SerializeField] Transform _barrel;

    [SerializeField] float _force;

    private void Start()
    {
        if (!_barrel)
        {
            _barrel = transform.Find("Barrel");

            Assert.IsNotNull( _barrel);
        }
    }

    private void FixedUpdate()
    {
        _simulatedScene.SimulatedTrajectory(_projectile, _originPoint.position, _force * _originPoint.forward);
    }

    public void RotateTurret(float pitch, float yaw)
    {
        Debug.Log($"Rotate by {pitch}, {yaw}");
        transform.Rotate(Vector3.up * yaw);
        _barrel.Rotate(Vector3.right * pitch);
    }

    internal void Fire()
    {
        AirmailPackage projectile = Instantiate(_projectile, _originPoint.position, Quaternion.identity);

        projectile.ApplyImpulse(_force * _originPoint.forward);
    }

    internal void AddForce(float force)
    {
        _force += force;
    }

    internal void SimulateTrajectory()
    {
        _simulatedScene.SimulatedTrajectory(_projectile, _originPoint.position, _force * _originPoint.forward);
    }
}
