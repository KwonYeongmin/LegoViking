using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawnerSelector : MonoBehaviour
{
    public GameObject itemSpawner;
    private ItemSpawner[] ItemSpawners = new ItemSpawner[10];
    private int ItemSpawnersCount = 10;
    public float Interval = 5.0f;
    private int CreatibleItemIndex = 2;
    public void SetItemIndex(int index) { CreatibleItemIndex = index; }
    private void Awake()
    {
        for (int i = 0; i < ItemSpawnersCount; i++)
        {
            ItemSpawners[i] = itemSpawner.transform.GetChild(i).gameObject.GetComponent<ItemSpawner>();
        }
    }

    public void Reset()
    {
        for (int i = 0; i < ItemSpawnersCount; i++)
        {
            itemSpawner.transform.GetChild(i).gameObject.GetComponent<ItemSpawner>().Reset();
        }
    }

    IEnumerator Start()
    {
        while (true)
        {
            yield return new WaitForSeconds(Interval);

            int index = Random.Range(0, ItemSpawnersCount);
            ItemSpawners[index].CreatibleItemIndex = CreatibleItemIndex;
            ItemSpawners[index].bIsChoosen = true;
            
        }
    }

}
