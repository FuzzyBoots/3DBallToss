using System;
using UnityEngine;
using UnityEngine.Assertions;

class GameManager : MonoBehaviour
{
    enum gameState
    {
        AIMING,
        SHOOTING,
        EVALUATING
    }

    [SerializeField] gameState _gameState = gameState.AIMING;

    [SerializeField] SimulatedScene _simulatedScene;
    [SerializeField] LauncherScript _launcher;

    [SerializeField] private float _pitchSpeed = 5f;
    [SerializeField] private float _yawSpeed = 5f;
    [SerializeField] private float _forceSpeed = 5f;
    [SerializeField] private int _score;

    public static GameManager Instance
    {
        get;
        private set;
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        } else
        {
            Debug.LogError("Duplicate GameManager", this);
            Destroy(this);
        }

        Assert.IsNotNull(_simulatedScene);
    }

    private void Update()
    {
        switch (_gameState)
        {
            case gameState.AIMING:
                PerformAiming();
                break;
            case gameState.SHOOTING:
                PerformShooting();
                break;
            case gameState.EVALUATING:
                PerformEvaluating();
                break;
        }
    }

    private void PerformEvaluating()
    {
        // ??
    }

    private void PerformShooting()
    {
        // ??
    }

    private void PerformAiming()
    {
        float multiplier = Input.GetKey(KeyCode.LeftShift) ? 5f : 1.0f;

        float pitchAmount = Input.GetAxis("Vertical") * Time.deltaTime * _pitchSpeed * multiplier;
        float yawAmount = Input.GetAxis("Horizontal") * Time.deltaTime * _yawSpeed * multiplier;

        _launcher.RotateTurret(pitchAmount, yawAmount);

        if (Input.GetKey(KeyCode.R))
        {
            _launcher.AddForce(_forceSpeed * Time.deltaTime * multiplier);
        }

        if (Input.GetKey(KeyCode.F))
        {
            _launcher.AddForce(-_forceSpeed * Time.deltaTime * multiplier);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartShooting();
        } else {
            _launcher.SimulateTrajectory();
        }
    }

    private void StartShooting()
    {
        _launcher.Fire();
        _gameState = gameState.SHOOTING;
    }

    // Helper function for bullets going out of scope. Initially, we'll assume one.
    public void RegisterBulletDeath()
    {
        StartEvaluating();
    }

    private void StartEvaluating()
    {
        _gameState = gameState.EVALUATING;
        // If all of our targets are gone, total up the score

        // Otherwwise:
        StartAiming();
    }

    private void StartAiming()
    {
        _gameState = gameState.AIMING;
    }

    public void AddScore(int score)
    {
        _score += score;

        // Update UI?
    }
}