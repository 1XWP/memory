using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts;
using System;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Threading;

public class GameScript : MonoBehaviour
{
    public GUISkin customSkin;

    public  int columns = 4;
    public  int rows = 4;
    static int totalCards;

    int matchesNeededToWin;
    int matchesMade = 0;
    int cardWidth = 100;
    bool playerCanClick; //flag to prevent clicking
    bool playerHasWon = false;
    public Card[,] gridOfCards;
    public List<Card> arrayCardsFlipped;
    List<Card> deckOfCards = new List<Card>();
    List<string> images = new List<string>(new string[] { "elephant", "giraffe", "gorilla", "lion", "moose", "hippopotamus", "sloth", "zebra" });
    Card card;
    PlaytimeTimer playtimeTimer;
    ModalPanel modalPanel;
    public GameObject promptTimeGameObject;
    public GameObject textTimeGameObject;
    public GameObject messageText;
    private GUIStyle guiStyle = new GUIStyle();

    void Start()
    {
        totalCards = columns * rows;
        matchesNeededToWin = totalCards / 2;
        playerCanClick = true;
        gridOfCards = new Card[rows, columns];
        arrayCardsFlipped = new List<Card>();
        BuildDeck();
        System.Random rnd = new System.Random();

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                int someNum = rnd.Next(0, deckOfCards.Count);
                gridOfCards[i, j] = deckOfCards.ElementAt(someNum);
                deckOfCards.RemoveAt(someNum);
            }
        }
    }

    void Awake()
    {
        playtimeTimer = GetComponent<PlaytimeTimer>();
        modalPanel = GetComponent<ModalPanel>();
    }

    private void BuildDeck()
    {
        int id = 0;

        for (int i = 0; i < 2; i++)
        {
            for (int j = 0; j < totalCards / 2; j++)
            {
                card = new Card(images.ElementAt(j), id);
                id++;
                deckOfCards.Add(card);
            }
        }
        deckOfCards.Shuffle();
    }

    void OnGUI()
    {
        GUILayout.BeginArea(new Rect(0, 0, Screen.width, Screen.height));
        TimerAssign();
        BuildGrid();
        if (playerHasWon)
        {
            modalPanel.ActivatePanel();
        }

        GUILayout.EndArea();
    }
    
    void TimerAssign()
    {
        Text text = textTimeGameObject.GetComponent<Text>();
        text.text = playtimeTimer.timerString;

        Text promptTimeText = promptTimeGameObject.GetComponent<Text>();
       promptTimeText.text = playtimeTimer.timerString;

        Text message = messageText.GetComponent<Text>();
        message.text = "Your time";

    }

    private void BuildGrid()
    {
        guiStyle.fixedHeight = 100;
        guiStyle.fixedWidth = 100; 
        GUILayout.BeginVertical();
        GUILayout.FlexibleSpace();
        for (int i = 0; i < rows; i++)
        {
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            for (int j = 0; j < columns; j++)
            {
                Card card = gridOfCards[i, j];
                string img;
                if (card.isFaceUp)
                {
                    img = card.img;
                }
                else
                {
                    img = card.imgDown;
                }
                GUI.enabled = !card.isMatched; //disable button if card is matched
                if (GUILayout.Button(Resources.Load(img) as Texture2D, guiStyle, GUILayout.Width(cardWidth)))
                {
                    if (playerCanClick)
                    {
                        FlipCardFaceUp(card);
                        onClick();//start timer on click
                    }
                }
                GUI.enabled = true;
            }
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
        }
        GUILayout.FlexibleSpace();
        GUILayout.EndVertical();
    }

    public void SetMatch(Card obj)
    {
        obj.isMatched = true;
    }

    public void HideCard(Card obj)
    {
        if (obj.isMatched)
        {
            obj.img = null;
        }
    }

    public void SetDown(Card obj)
    {
        if (!obj.isMatched)
            obj.isFaceUp = false;
    }

    private void FlipCardFaceUp(Card card)
    {
        card.isFaceUp = true;
        if (arrayCardsFlipped.Contains(card) == false)
        {
            arrayCardsFlipped.Add(card);
            if (arrayCardsFlipped.Count == 2)
            {
                if (arrayCardsFlipped[0].img.ToString() == arrayCardsFlipped[1].img.ToString())
                {
                    playerCanClick = false;
                    arrayCardsFlipped.ForEach(SetMatch);
                    matchesMade++;
                    if(matchesMade >= matchesNeededToWin)
                    {
                        playerHasWon = true;
                        playtimeTimer.Finnish();
                    }
                    arrayCardsFlipped = new List<Card>();
                    playerCanClick = true;
                }
            }
            if (arrayCardsFlipped.Count > 2)
            {
                playerCanClick = false;
                arrayCardsFlipped.ForEach(SetDown);
                arrayCardsFlipped = new List<Card>();
                playerCanClick = true;
            }
        }
    }

    public void onClick()
    {
        if (!playtimeTimer.start)
        {
            playtimeTimer.StartTimer();
        }
    }
    
    public class Card : object
    {
        public bool isFaceUp = false;
        public bool isMatched = false;
        public string img;
        public string imgTransp;
        public string imgDown;
        public int id;

        public Card(string img, int id)
        {
            this.img = img;
            this.id = id;
            imgDown = "blank-01";
            imgTransp = "transp";
        }
    }

}