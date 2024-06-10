using System;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SimulatedScene : MonoBehaviour
{
    private Scene _simulatedScene;
    private PhysicsScene _physicsScene;

    // A reference to a collection that holds the items we plan to simulate
    [SerializeField] private Transform _targetParent;


    private void Awake()
    {
        if (_targetParent == null)
        {
            _targetParent = transform.root;
        }
    }

    void Start()
    {
        Debug.Log("FixedDeltaTime: " + Time.fixedDeltaTime);
        CreateSimulatedPhysicsScene();

        Physics.simulationMode = SimulationMode.FixedUpdate;
    }

    private void CreateSimulatedPhysicsScene()
    {
        _simulatedScene = SceneManager.CreateScene("Physics Simulation Scene", new CreateSceneParameters(LocalPhysicsMode.Physics3D));
        _physicsScene = _simulatedScene.GetPhysicsScene();

        UpdateObstacles();
    }

    public void UpdateObstacles()
    {
        GameObject[] objectsInPhysicsScene = _simulatedScene.GetRootGameObjects();
        foreach (GameObject myObj in objectsInPhysicsScene)
        {
            // Debug.Log($"Destroying {myObj.name}");
            Destroy(myObj);
        }
        foreach (Transform obstacle in _targetParent)
        {
            // Debug.Log($"Considering {obstacle.name} with tag {obstacle.tag}");
            if (obstacle.CompareTag("Obstacle"))
            {
                GameObject simulatedObject = Instantiate(obstacle.gameObject, obstacle.position, obstacle.rotation);
                if (simulatedObject.TryGetComponent<Renderer>(out Renderer renderer))
                { 
                    renderer.enabled = false;
                }

                if (simulatedObject.TryGetComponent<Rigidbody>(out Rigidbody rigidbody))
                {
                    rigidbody.isKinematic = true;
                }

                SceneManager.MoveGameObjectToScene(simulatedObject, _simulatedScene);
            }
        }
    }

    [SerializeField] LineRenderer _lineRenderer;
    [SerializeField] private int _maxPhysicsInteractions = 100;

    public void SimulatedTrajectory(AirmailPackage package, Vector3 pos, Vector3 velocity)
    {
        // UpdateObstacles();

        AirmailPackage instance = Instantiate(package, pos, Quaternion.identity);
        
        if (instance.TryGetComponent<Renderer>(out Renderer renderer)) 
        {
            renderer.enabled = false;
        }

        SceneManager.MoveGameObjectToScene(instance.gameObject, _simulatedScene);

        instance.ApplyImpulse(velocity);

        // Set the points for the line renderer for the simulated projectiles
        _lineRenderer.positionCount = _maxPhysicsInteractions;

        for(int i=0; i < _maxPhysicsInteractions; i++)
        {
            _lineRenderer.SetPosition(i, instance.transform.position);
            _physicsScene.Simulate(Time.fixedDeltaTime * 3);
        }

        Destroy(instance.gameObject);
    }
}
