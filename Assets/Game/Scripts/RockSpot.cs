using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;
using CodeMonkey;
public class RockSpot : MonoBehaviour
{
    private Vector3 randomPosition;
    [SerializeField] private int width;
    [SerializeField] private int height;
    static public bool spawn = false;
    static public int rocksOn = 0;
    public GameObject[] rocks;
    const int limitRocks = 3;
    public int rockscant = 0;
    private bool spawnSpot = true;
    // Start is called before the first frame update
    void Start()
    {
        SpawnRock();
    }

    // Update is called once per frame
    void Update()
    {
        if(spawnSpot)
            StartCoroutine("SpawnTime");
    }

    public void SpawnRock()
    {

        if (rocksOn < limitRocks)
        {
            rockscant = 0;
            for (int x = 0; x < rocks.Length; x++)
            {
                if (rocks[x].activeSelf)
                {
                    if(!rocks[x].GetComponent<Rock>().detected)
                        rockscant++;
                }

            }
            for (int z = 0; z < rocks.Length; z++)
            {
                if (rockscant < limitRocks)
                {
                    if (!rocks[z].activeSelf)
                    { 
                        StartCoroutine("SpawnRockTime",z);
                        rockscant++;
                        rocksOn++;
                    }
                }
            }
        }

    }

    private IEnumerator SpawnTime()
    {
        spawnSpot = false;
        yield return new WaitForSeconds(2.0f);
        SpawnRock();
        spawnSpot = true;
    }

    private IEnumerator SpawnRockTime(int i)
    {
        yield return new WaitForSeconds(4.0f);

        rocks[i].SetActive(true);
        Spawn(i);
    }

    private void Spawn(int i)
    {
        randomPosition = new Vector3(Random.Range(0, width * 10), Random.Range(0, height * 10));
        Testing.pathfinding.GetGrid().GetXY(randomPosition, out int x, out int y);
        List<PathNode> path = Testing.pathfinding.FindPath(0, 0, x, y);
        if (path != null)
            rocks[i].transform.position = new Vector3(x * 10, y * 10) + Vector3.one * 5f;
        else
        {
            rocks[i].SetActive(false);
        }
    }


}
