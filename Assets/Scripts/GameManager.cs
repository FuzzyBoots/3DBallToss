using System;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.SceneManagement;

class GameManager : MonoBehaviour
{
    enum gameState
    {
        AIMING,
        SHOOTING,
        GAME_OVER
    }

    [SerializeField] gameState _gameState = gameState.AIMING;

    [SerializeField] SimulatedScene _simulatedScene;
    [SerializeField] LauncherScript _launcher;

    [SerializeField] private float _pitchSpeed = 5f;
    [SerializeField] private float _yawSpeed = 5f;
    [SerializeField] private float _forceSpeed = 5f;
    [SerializeField] private int _score;
    
    [SerializeField] private GameObject _enemyContainer;

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

        Assert.IsNotNull(_simulatedScene, "Simulated Scene not set");
        Assert.IsNotNull(_launcher, "Launcher not set");
        Assert.IsNotNull(_enemyContainer, "Enemy Container not set");

        CheckWinCondition();
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
        }

        if (Input.GetKey(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        CheckWinCondition();
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

        if (Input.GetKey(KeyCode.F))
        {
            _launcher.AddForce(_forceSpeed * Time.deltaTime * multiplier);
        }

        if (Input.GetKey(KeyCode.G))
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
        AddScore(-1);
        _gameState = gameState.SHOOTING;
    }

    // Helper function for bullets going out of scope. Initially, we'll assume one.
    public void RegisterBulletDeath()
    {
        if (_gameState != gameState.GAME_OVER)
        {
            StartAiming();
        }
    }

    public void CheckWinCondition()
    {
        int activeChildCount = _enemyContainer.transform.childCount;
        UIManager.Instance.SetLeft(activeChildCount);
        if (activeChildCount == 0)
        {
            UIManager.Instance.DisplayWinText();
            _gameState = gameState.GAME_OVER;
        }
    }

    private void StartAiming()
    {
        _gameState = gameState.AIMING;
    }

    public void AddScore(int score)
    {
        _score += score;

        UIManager.Instance.SetScore(_score);
    }
}