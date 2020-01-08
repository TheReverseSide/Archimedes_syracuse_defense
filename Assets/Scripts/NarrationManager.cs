using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NarrationManager : MonoBehaviour
{
    public Text sentenceText;
    public Button playButton;
    public Button contButton;
    //public Image playImage;

    public Queue<string> sentences;

    void Start()
    {
        sentences = new Queue<string>();
        //playImage = playButton.gameObject.GetComponent<Image>();
        //Color color = playImage.color;
        playButton.gameObject.SetActive(false);
        playButton.interactable = false;
    }

    public void StartNarration(Narration narration)
    {
        Debug.Log("Should start");
        sentences.Clear();

        foreach (string sentence in narration.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }


    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence(string sentence)
    {
        sentenceText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            sentenceText.text += letter;
            yield return null;
        }
    }

    void EndDialogue()
    {
        Debug.Log("End of conversation");

        contButton.gameObject.SetActive(false);
        contButton.interactable = false;
        playButton.gameObject.SetActive(true);
        playButton.interactable = true;
    }

}
