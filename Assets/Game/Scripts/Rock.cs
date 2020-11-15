using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour
{
    [SerializeField] private bool detected = false;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Minero")
        {
            if (detected) 
            { 
            RockSpot.spawn = true;
                detected = false;
            this.gameObject.SetActive(false);
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
}
