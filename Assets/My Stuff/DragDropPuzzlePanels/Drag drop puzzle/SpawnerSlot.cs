using UnityEngine;

// Creates a slot that can spawn in new items so that items can be dragged out.

// NOTE: to use, remember to add a prefab image item  in itemPrefab
public class SpawnerSlot : MonoBehaviour
{
    [SerializeField] private GameObject itemPrefab;
    [SerializeField] private float spawnedScale = 0.7f;
    
    private void Start()
    {
        SpawnItem();
    }

    public void SpawnItem()
    {
        GameObject newItem = Instantiate(itemPrefab, transform);
        newItem.transform.SetParent(transform);
        newItem.transform.localScale = Vector3.one * spawnedScale;
    }
}
