using UnityEngine;

public class PlatformsManager : MonoBehaviour
{
    [SerializeField]
    private Transform platformsPivot;
    [SerializeField]
    private InstantiatePoolObjects[] platformPrefabs;
    [SerializeField]
    private int initialPlataforms = 5;
    [SerializeField]
    private float speed = 5f;
    private bool isRunning = true;
    private GameObject lastPlataform;
    private void Start()
    {
        InstantiatePlatform(initialPlataforms);
        transform.position = platformsPivot.position;
    }
    public void InstantiatePlatform(int number)
    {
        for (int i=0; i < number; i++)
        {
            InstantiatePoolObjects instantiatePool = platformPrefabs[Random.Range(0, platformPrefabs.Length)];
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
