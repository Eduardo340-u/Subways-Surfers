using UnityEngine;
using UnityEngine.Events;

public class PlatformsManager : MonoBehaviour
{
    [SerializeField]
    private Transform platformsPivot;
    [SerializeField]
    private InstantiatePoolObjects[] platformPrefabs;
    [SerializeField]
    private InstantiatePoolObjects[] securePlatformPrefabs;
    [SerializeField]
    private int initialPlataforms = 5;
    [SerializeField]
    private float minSpeed = 5f;
    [SerializeField]
    private float maxSpeed = 12f;
    [SerializeField]
    private float acceleration = 0.1f;
    [SerializeField]
    private UnityEvent<Platform> onPlatformPassed;
    private bool isRunning = true;
    private GameObject lastPlataform;
    private int platformIstantiated = 0;
    private float speed;
    public void StartGame()
    {
        speed = minSpeed;
        lastPlataform = null;
        platformIstantiated = 0;
        InitializePlatforms();
        InstantiatePlatform(initialPlataforms);
        transform.position = platformsPivot.position;
        isRunning = true;
    }
    private void InitializePlatforms()
    {
        foreach (var platform in platformPrefabs)
        {
            platform.DeactivateAllObjects();
        }
        foreach (var securePlatform in securePlatformPrefabs)
        {
            securePlatform.DeactivateAllObjects();
        }
    }
    public void InstantiatePlatform(int number)
    {
        for (int i=0; i < number; i++)
        {
            InstantiatePoolObjects instantiatePool;
            if (platformIstantiated < 2)
            {
                instantiatePool = securePlatformPrefabs[Random.Range(0, securePlatformPrefabs.Length)];
            } else
            {
                instantiatePool = platformPrefabs[Random.Range(0, platformPrefabs.Length)];
            }
            platformIstantiated++;
            Vector3 spawnPosition = Vector3.zero;
            if (lastPlataform !=null)
            {
                spawnPosition = lastPlataform.transform.localPosition + lastPlataform.GetComponent<Platform>().ColliderSize * Vector3.forward;
            }
            instantiatePool.InstantiateObject(spawnPosition);
            GameObject createPlatform = instantiatePool.GetCurrentObject();
            Platform newPlatform = instantiatePool.GetCurrentObject().GetComponent<Platform>();
            newPlatform.transform.SetParent(transform);
            newPlatform.transform.localPosition = spawnPosition + newPlatform.ColliderSize * Vector3.forward;
            lastPlataform = newPlatform.gameObject;
            onPlatformPassed?.Invoke(newPlatform);
        }
    }
    private void Update() 
    {
        if (isRunning)
        {
            transform.Translate(Vector3.back * speed * Time.deltaTime);
            speed = Mathf.Min(speed + acceleration * Time.deltaTime, maxSpeed);
        }
    }
    public void StopPlatforms()
    {
        isRunning = false;
    }
}
