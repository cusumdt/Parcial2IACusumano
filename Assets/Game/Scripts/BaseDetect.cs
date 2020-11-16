using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaseDetect : MonoBehaviour
{
    public Text coins;
    [SerializeField] private int goldBase;
    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.tag == "Minero")
        {
            var comp = other.transform.parent.gameObject.GetComponent<Animator>();
            if (comp.GetBehaviour<ReturningMineroBehaviour>().returnBase)
            {
                if (comp.GetBehaviour<ReturningMineroBehaviour>().rock.activeSelf)
                {
                    goldBase += comp.GetBehaviour<ReturningMineroBehaviour>().gold;
                    comp.GetBehaviour<ReturningMineroBehaviour>().gold = 0;
                    comp.SetTrigger("ToMinningReturning");
                    coins.text = goldBase.ToString();

                }
                else 
                {
                    comp.SetTrigger("ToIdleReturning");
                }
            }
        }
    }
}
