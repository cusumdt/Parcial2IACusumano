using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Spawner : MonoBehaviour
{
    public GameObject[] Explorers;
    public GameObject[] Mineros;
    public int ActualMinero = 1;
    public int ActualExplorer = 1;
    public Text cantMineros;
    public Text cantExplorers;
    // Start is called before the first frame update


    public void SpawnMinero()
    {
        if (ActualMinero < Mineros.Length)
        {
            Mineros[ActualMinero].SetActive(true);
            ActualMinero++;
            cantMineros.text = ActualMinero.ToString() + " / " + Mineros.Length.ToString(); ;
        }
    }

    public void SpawnExplorer() 
    {
        if (ActualExplorer<Explorers.Length)
        { 
            Explorers[ActualExplorer].SetActive(true);
            ActualExplorer++;
            cantExplorers.text = ActualExplorer.ToString() + " / " + Explorers.Length.ToString();
        }
    }
}
