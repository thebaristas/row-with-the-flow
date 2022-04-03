using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SpawnCategory {
    public string name;
    [Tooltip("Number of items to spawn per second")]
    public float spawnRate;
    public Riverer[] prefabs;

    private float spawnTimer;

    public SpawnCategory() {
        ResetTimer();
    }

    public void Update(float delta) {
        spawnTimer += delta;
    }

    public bool ShouldSpawn() {
        return spawnTimer > 1f / spawnRate;
    }

    public void ResetTimer() {
        spawnTimer = 0;
    }

    public Riverer GetRandomItem() {
        return prefabs[UnityEngine.Random.Range(0, prefabs.Length)];
    }
}

class ItemToSpawn {
    Riverer item;
    Vector2 position;
    float rotation;

    public ItemToSpawn(Riverer item, Vector2 position, float rotation) {
        this.item = item;
        this.position = position;
        this.rotation = rotation;
    }

    public Riverer Instantiate() {
        return UnityEngine.Object.Instantiate(item, position, Quaternion.Euler(0, 0, rotation));
    }
}

public class ItemSpawner : MonoBehaviour
{
    public SpawnCategory[] spawnCategories;
    public Transform spawnTopLeft;
    public Transform spawnBottomRight;

    private static ItemSpawner _instance;

    private Queue<ItemToSpawn> spawnQueue;

    public static ItemSpawner Instance { get { return _instance; } }

    private Vector2 PickRandomSpawnPoint() {
        var p = new Vector2(
            UnityEngine.Random.Range(spawnTopLeft.position.x, spawnBottomRight.position.x),
            UnityEngine.Random.Range(spawnTopLeft.position.y, spawnBottomRight.position.y)
        );
        Debug.Log(p.x);
        return p;
    }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        } else {
            _instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        spawnQueue = new Queue<ItemToSpawn>();
    }

    // Update is called once per frame
    void Update()
    {
        foreach (SpawnCategory category in spawnCategories) {
            category.Update(Time.deltaTime);
            if (category.ShouldSpawn()) {
                var item = category.GetRandomItem();
                spawnQueue.Enqueue(new ItemToSpawn(item, PickRandomSpawnPoint(), 360 * UnityEngine.Random.value));
                category.ResetTimer();
            }
        }

        // Attempt to spawn top item from queue
        if (spawnQueue.Count > 0) {
            Riverer instance = spawnQueue.Peek().Instantiate();
            Collider2D[] colliders = new Collider2D[1];
            int overlapCount = instance.GetRiverCollider2D().OverlapCollider(new ContactFilter2D().NoFilter(), colliders);
            foreach (Collider2D collider in colliders) {
                if (collider.gameObject.GetComponent<Riverer>() != null) {
                    Debug.Log("Overlapping objects");
                    Destroy(instance.gameObject);
                    return;
                }
            }
            spawnQueue.Dequeue();
            instance.initialVelocity = River.Instance.riverSpeed;
        }
    }
}
