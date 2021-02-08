using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NPCController : MonoBehaviour
{
    [SerializeField]
    private Canvas textBubble;

    [SerializeField]
    private TextMeshProUGUI text;

    [SerializeField]
    private SpriteRenderer emote;

    [SerializeField]
    private Sprite sadFace;

    [SerializeField]
    private Sprite happyFace;

    [SerializeField]
    private GameObject feelingBubble;

    [SerializeField]
    private string[] lostPhrasePool;

    [SerializeField]
    private string[] foundPhrasePool;

    [SerializeField]
    private GameObject[] lostObjectPool;

    public GameObject myObject;
    private BoxCollider2D interactBox;
    private bool objectFound = false;
    private string phrase;
    public bool hasTalked = false;

    // Start is called before the first frame update
    void Start()
    {
        interactBox = gameObject.GetComponent<BoxCollider2D>();
        phrase = lostPhrasePool[Random.Range(0, lostPhrasePool.Length)];
        hideText();

        LostItem lost = myObject.GetComponent<LostItem>();
        lost.owner = gameObject;

        string objectName = myObject.GetComponent<LostItem>().myName;
        phrase = phrase.Replace("1", objectName);
        text.text = phrase;
    }

    // Update is called once per frame
    void Update()
    {
        updateEmote();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            hasTalked = true;
            GameObject playerCarry = collision.gameObject.GetComponent<PlayerController>().carrying;
            if (playerCarry)
            {
                if (playerCarry == myObject) { 
                    LostItem thisItem = myObject.GetComponent<LostItem>();
                    objectFound = true;
                    phrase = foundPhrasePool[Random.Range(0, foundPhrasePool.Length)];
                    phrase = phrase.Replace("1", thisItem.myName);
                    Destroy(myObject);
                }
            }
            else
            {
                text.text = "That's not mine...";
            }

            showText();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            hideText();
        }
    }

    private void showText()
    {
        text.text = phrase;
        textBubble.enabled = true;
        feelingBubble.SetActive(false);
    }

    private void hideText()
    {
        textBubble.enabled = false;
        feelingBubble.SetActive(true);
    }

    private void updateEmote()
    {
        if (objectFound)
        {
            emote.sprite = happyFace;
        }
        else
        {
            emote.sprite = sadFace;
        }
    }
}
