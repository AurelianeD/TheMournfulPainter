using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class dialogue : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    public string[] lines;
    public float textSpeed;
    private bool isClose;

    public GameObject dialoguePanel;

    private int index; 
    // Start is called before the first frame update
    void Start()
    {
        index = 0;
        textComponent.text = string.Empty;
        //StartDialogue();
    }

    // Update is called once per frame
    void Update()
    {

       
        if (Input.GetKeyDown(KeyCode.C) && isClose)
        {
            if (dialoguePanel.activeInHierarchy)
            {
                ZeroText();
            }
            else
            {
                dialoguePanel.SetActive(true);
                StartCoroutine(TypeLine());

                if(textComponent.text == lines[index])
                {
                    NextLine();
                }
                else
                {
                    ZeroText();
                }
            }
            //if (textComponent.text == lines[index])
            //{
            //    NextLine();
            //}
            //else
            //{
            //    StopAllCoroutines();
            //    textComponent.text = lines[index];
            //}
        }
    }

    private void ZeroText()
    {
        textComponent.text = string.Empty;
        index = 0;
        dialoguePanel.SetActive(false);
    }

    //void StartDialogue()
    //{
    //    index = 0;
    //    StartCoroutine(TypeLine());

    //}

    IEnumerator TypeLine()
    {
        //Type each caracter one by one
        foreach (char c in lines[index].ToCharArray())
        {
            textComponent.text += c;
            yield return new WaitForSeconds(Time.deltaTime * textSpeed);
        }
    }

    void NextLine()
    {
        if(index < lines.Length - 1)
        {
            index++;
            textComponent.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else
        {
            ZeroText();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            isClose = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isClose = false;
            ZeroText();
        }
    }
}
