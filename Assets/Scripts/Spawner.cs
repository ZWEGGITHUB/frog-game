using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject prefab;
    public AudioSource makeSound;
    public float minTime = 2f;
    public float maxTime = 4f;

    private void OnEnable()
    {
        Invoke(nameof(Spawn), minTime);
    }

    private void OnDisable()
    {
        CancelInvoke();
    }

    private void Spawn()
    {
        Instantiate(prefab, transform.position, Quaternion.identity);
        makeSound.Play();
        Invoke(nameof(Spawn), Random.Range(minTime, maxTime));
    }

}
