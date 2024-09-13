using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Xml.XPath;
using System.Threading;

public class Hacking : MonoBehaviour
{
    private int CountRightAnswers = 0;
    private int iterator; // Итератор
    private String[] Answer = new string[5]; // Правильный ответ
    private String[] UserAnswer = new string[5];// Ответ пользователя
    private bool HackingInProccess = false; //Делается ли взлом
    public GameObject pressEforHack;// надпись нажмите Е для взлома
    public GameObject windowHack;//Окно с заданием
    [Header("Первая строка")]
    public TextMeshProUGUI[] FirstStringValue; //Заполняем в инспекторе как матрицу;
    [Header("Вторая строка")]
    public TextMeshProUGUI[] SecondStringValue; //Заполняем в инспекторе
    [Header("Третья строка")]
    public TextMeshProUGUI[] ThreeStringValue; //Заполняем в инспекторе
    [Header("Операция")]
    public TextMeshProUGUI Operation;
    public String[] Operations = new String[3];

    private void Start()
    {
        Operations[0] = "or";
        Operations[1] = "and";
        Operations[2] = "xor";
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player" && pressEforHack != null)
        {
            pressEforHack.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && pressEforHack != null)
        {
            pressEforHack.SetActive(false);
        }
    }

    private void Update()
    {
        if (pressEforHack.activeSelf && Input.GetKeyDown(KeyCode.E))
        {
            HackingInProccess = true;
        }

        if (HackingInProccess == true)
        {
            HackingInProccess = false;
            DialogManager.Stop = true;
            windowHack.SetActive(true);
            pressEforHack.SetActive(false);
            Operation.text = Operations[UnityEngine.Random.Range(0, 3)];
            for (int i = 0; i < 5; i++)
            {
                FirstStringValue[i].text = UnityEngine.Random.Range(0, 2).ToString();
                SecondStringValue[i].text = UnityEngine.Random.Range(0, 2).ToString();
            }
            RightAnswer();
        }
        if (iterator < 5)
        {
            if ((Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Keypad1)) && iterator < 5)
            {
                UserAnswer[iterator] = "1";
                ThreeStringValue[iterator].text = UserAnswer[iterator];
                iterator++;
            }
            else if ((Input.GetKeyDown(KeyCode.Alpha0) || Input.GetKeyDown(KeyCode.Keypad1)) && iterator < 5)
            {
                UserAnswer[iterator] = "0";
                ThreeStringValue[iterator].text = UserAnswer[iterator];
                iterator++;
            }
        }
        if (iterator == 5)
        {
            CheckAnswer();
            iterator++;
        }
        if (iterator > 5)
        {
            StartCoroutine(NonActiveScreen());
        }

    }

    private void RightAnswer()
    {
        if (Operation.text == "or")
        {
            for (int i = 0;i < 5; i++)
            {
                Answer[i] = (FirstStringValue[i].text == "1" || SecondStringValue[i].text == "1") ? "1" : "0";
            }
        }
        else if (Operation.text == "and")
        {
            for (int i = 0; i < 5; i++)
            {
                Answer[i] = (FirstStringValue[i].text == "1" && SecondStringValue[i].text == "1") ? "1" : "0";
            }
        }
        else if (Operation.text == "xor")
        {
            for (int i = 0; i < 5; i++)
            {
                Answer[i] = (FirstStringValue[i].text != SecondStringValue[i].text) ? "1" : "0";
            }
        }
    }

    private void CheckAnswer()
    {
        for (int i = 0; i < 5; i++)
        {
            Debug.Log(CountRightAnswers);
            if (Answer[i] == UserAnswer[i])
            {
                ThreeStringValue[i].GetComponent<TextMeshProUGUI>().color = Color.green;
                CountRightAnswers++;
            }
            else
            {
                ThreeStringValue[i].GetComponent<TextMeshProUGUI>().color = Color.red;
            }
        }
    }

    private IEnumerator NonActiveScreen()
    {
        yield return new WaitForSeconds(1f);
        windowHack.SetActive(false);
        DialogManager.Stop = false;

        if (CountRightAnswers == 5)
        {
            Destroy(transform.parent.gameObject, 1f);
        }
        else
        {
            transform.parent.gameObject.GetComponent<Enemy>().IsDetected = true;
            Destroy(this.gameObject);
        }
    }
}
