using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject prefab;
    public AudioSource makeSound;
    private Animator anim;
    public Transform spawnPoint;
    public float minTime = 2f;
    public float maxTime = 4f;

    private void Start() 
    {
        anim = GetComponent<Animator>();
    }

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
        makeSound.Play();
        anim.SetTrigger("Attack");

        Invoke(nameof(SpawnPrefab), 0.5f);
        Invoke(nameof(Spawn), Random.Range(minTime, maxTime));
    }
    private void SpawnPrefab()
    {
        // Instantiate the prefab at the spawn point after the delay
        Instantiate(prefab, spawnPoint.position, Quaternion.identity);
    }
}
