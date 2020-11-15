using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour
{
    [SerializeField] private bool detected = false;
    public int gold = 20;
    public float time = 0f;
    private void Update()
    {
        if (gold == 0)
        {
            gold = 20;
            detected = false;
            this.gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
     
        if (other.gameObject.tag == "ColMinero")
        {
            if (detected)
            {
                /* RockSpot.spawn = true;
                 detected = false;
                 this.gameObject.SetActive(false);*/
                Debug.Log("A minar kpo");
                var comp = other.transform.parent.gameObject.GetComponent<Animator>();
                comp.SetTrigger("ToMinning");
                comp.GetBehaviour<MinningMineroBehaviour>().ObjectPos = transform.position;
            }
        }
        if (other.gameObject.tag == "Minero")
        {
            if (detected)
            {

                if (time >= 1.0f)
                {
                    Debug.Log("Holaaa");
                    var comp = other.transform.parent.gameObject.GetComponent<Animator>();
                    gold--;
                    comp.GetBehaviour<MinningMineroBehaviour>().gold++;
                    time = 0f;
                }
                else
                    time += Time.deltaTime;

            }
        }
        if (other.gameObject.tag == "ColExplorer")
        {
            if (!detected)
            { 
            var comp = other.transform.parent.gameObject.GetComponent<Animator>();
            comp.SetTrigger("ToMarking");
            comp.GetBehaviour<MarkingExplorerBehaviour>().ObjectPos = transform.position;
            }

        }
        if (other.gameObject.tag == "Explorer")
        {
            var comp = other.transform.parent.gameObject.GetComponent<Animator>();
            detected = true;
        }
      
        if (other.gameObject.tag == "Rock")
        {
            this.gameObject.SetActive(false);
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Minero")
        {
            if (detected)
            {
      
                if (time >= 1.0f)
                {
                    Debug.Log("Holaaa");
                    var comp = other.transform.parent.gameObject.GetComponent<Animator>();
                    gold--;
                    comp.GetBehaviour<MinningMineroBehaviour>().gold++;
                    time = 0f;
                }
                else
                    time += Time.deltaTime;

            }
        }
    }

}
