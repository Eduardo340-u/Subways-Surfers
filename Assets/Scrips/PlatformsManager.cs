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
    private float speed = 5f;
    [SerializeField]
    private UnityEvent<Platform> onPlatformPassed;
    private bool isRunning = true;
    private GameObject lastPlataform;
    private int platformIstantiated = 0;
    public void StartGame()
    {
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
                spawnPosition = lastPlataform.transform.localPosition + lastPlataform.GetComponent<Collider>().bounds.size.z * Vector3.forward * 0.5f;
            }
            instantiatePool.InstantiateObject(spawnPosition);
            GameObject newPlatform = instantiatePool.GetCurrentObject();
            newPlatform.transform.SetParent(transform);
            newPlatform.transform.localPosition = spawnPosition + newPlatform.GetComponent<Collider>().bounds.size.z * Vector3.forward * 0.5f;
            lastPlataform = newPlatform;
            onPlatformPassed?.Invoke(newPlatform.GetComponent<Platform>());
        }
    }
    private void Update() 
    {
        if (isRunning)
        {
            transform.Translate(Vector3.back * speed * Time.deltaTime);
        }
    }
    public void StopPlatforms()
    {
        isRunning = false;
    }
}
