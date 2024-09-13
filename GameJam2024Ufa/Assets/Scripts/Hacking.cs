using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Hacking : MonoBehaviour
{
    [NonSerialized] public bool IsHack = false;
    public GameObject pressEforHack;// надпись нажмите Е для взлома
    public GameObject windowHack;
    public TextMeshProUGUI[] textDialog;


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            pressEforHack.SetActive(true);
        }
    }


    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                DialogManager.Stop = true;
                windowHack.SetActive(true);
                
            }
        }
    }
}
