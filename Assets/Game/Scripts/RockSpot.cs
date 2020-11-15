using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;
using CodeMonkey;
public class RockSpot : MonoBehaviour
{
    private Vector3 randomPosition;
    [SerializeField]private int width;
    [SerializeField]private int height;
    static public bool spawn = false;
    [SerializeField] int rocksOn = 0;
    public GameObject[] rocks;
    const int limitRocks = 3;
    // Start is called before the first frame update
    void Start()
    {
        SpawnRock();  
    }

    // Update is called once per frame
    void Update()
    {
        if (spawn)
        {
            StartCoroutine("SpawnTime");
            spawn = false;
        }
    }

    public void SpawnRock()
    {
      
        for (int i = 0; i < limitRocks; i++)
        {
            rocksOn = 0;
            for (int x = 0; x < rocks.Length; x++)
            {
                if (rocks[x].activeSelf)
                {
                    rocksOn++;
                }

            }
            for (int z = 0; z < rocks.Length; z++)
            {
                if (rocksOn < limitRocks)
                {
                    rocks[z].SetActive(true);
                    randomPosition = new Vector3(Random.Range(0, width * 10), Random.Range(0, height * 10));
                    Testing.pathfinding.GetGrid().GetXY(randomPosition, out int x, out int y);
                    rocks[z].transform.position = new Vector3(x * 10, y * 10) + Vector3.one * 5f;
                    rocksOn++;
                }
            }
        }
       
    }

    private IEnumerator SpawnTime()
    {
        yield return new WaitForSeconds(2.0f);
        SpawnRock();
    }
}
