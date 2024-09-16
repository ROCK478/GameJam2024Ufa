using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{
    public GameObject windowDialog;
    public GameObject pressFforTalk; 
    public TextMeshProUGUI textDialog; 
    public Button button; 
    public bool GoTalk = false; 
    public string[] message; 
    private int NumberDialog = 0; 
    public GameObject[] SpeakPersons; 
    private GameObject SpeakPersonNow; 
    private Vector2 rememberFirstScale;
    public static bool Stop = false;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (!GoTalk)
            {
                pressFforTalk.SetActive(true);
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                pressFforTalk.SetActive(false);
                GoTalk = true;
                Time.timeScale = 0f;
                windowDialog.SetActive(true);
                textDialog.text = message[NumberDialog];
                SpeakPersonNow = SpeakPersons[NumberDialog];
                rememberFirstScale = SpeakPersonNow.transform.localScale;
                SpeakPersonNow.transform.localScale *= 1.5f;
                button.gameObject.SetActive(true);
            }
            if (GoTalk)
            {
                if (NumberDialog == message.Length - 1)
                {
                    button.gameObject.SetActive(false);
                    Time.timeScale = 1f;
                }
                else
                {
                    button.gameObject.SetActive(true);
                    button.onClick.AddListener(NextDialog);
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (pressFforTalk != null)
        {
            pressFforTalk.SetActive(false);
        }
        if (windowDialog != null)
        {
            windowDialog.SetActive(false);
        }
    }

    public void NextDialog()
    {
        NumberDialog++;
        textDialog.text = message[NumberDialog];
        SpeakPersonNow.transform.localScale = rememberFirstScale;
        SpeakPersonNow = SpeakPersons[NumberDialog];
        rememberFirstScale = SpeakPersonNow.transform.localScale;
        SpeakPersonNow.transform.localScale *= 1.5f;
        if (NumberDialog == message.Length - 1)
        {
            button.gameObject.SetActive(false);
            Time.timeScale = 1f;
        }
    }


}
