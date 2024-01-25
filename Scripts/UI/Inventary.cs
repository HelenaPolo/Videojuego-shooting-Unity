using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventary : MonoBehaviour
{
    public GameObject inventaryPanel;
    public GameObject collectionablePanel;
    public GameObject weaponPanel;

    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (inventaryPanel.gameObject.GetComponent<Animator>().GetInteger("State") == 0)
            {
                inventaryPanel.gameObject.GetComponent<Animator>().SetInteger("State",1);
            }
            else
            {
                inventaryPanel.gameObject.GetComponent<Animator>().SetInteger("State", 0);
            }

            if (collectionablePanel.gameObject.GetComponent<Animator>().GetInteger("State") == 0)
            {
                collectionablePanel.gameObject.GetComponent<Animator>().SetInteger("State", 1);
            }
            else
            {
                collectionablePanel.gameObject.GetComponent<Animator>().SetInteger("State", 0);
            }

            if (weaponPanel.gameObject.GetComponent<Animator>().GetInteger("State") == 0)
            {
                weaponPanel.gameObject.GetComponent<Animator>().SetInteger("State", 1);
            }
            else
            {
                weaponPanel.gameObject.GetComponent<Animator>().SetInteger("State", 0);
            }




            /*if (inventaryPanel.gameObject.GetComponent<Animator>().GetInteger("State") == 0 && collectionablePanel.gameObject.GetComponent<Animator>().GetInteger("State") == 0 && weaponPanel.gameObject.GetComponent<Animator>().GetInteger("State") == 0)
            {
                inventaryPanel.gameObject.GetComponent<Animator>().SetInteger("State", 1);
                collectionablePanel.gameObject.GetComponent<Animator>().SetInteger("State", 1);
                weaponPanel.gameObject.GetComponent<Animator>().SetInteger("State", 1);

            }
            else
            {
                inventaryPanel.gameObject.GetComponent<Animator>().SetInteger("State", 0);
                collectionablePanel.gameObject.GetComponent<Animator>().SetInteger("State", 0);
                weaponPanel.gameObject.GetComponent<Animator>().SetInteger("State", 0);

            }*/


        }
    }
}
