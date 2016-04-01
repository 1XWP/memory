using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts;
using System;
using UnityEngine.SceneManagement;

public class GameScript : MonoBehaviour
{
    public GUISkin customSkin;

    static int columns = 4;
    static int rows = 4;
    static int totalCards = columns * rows;
    int matchesNeededToWin = totalCards / 2;
    int matchesMade = 0;
    int cardWidth = 100;
    int cardHeight = 100;
    bool playerCanClick; //flag to prevent clicking
    bool playerHasWon = false;
    Card[,] gridOfCards;
    public List<Card> arrayCardsFlipped;
    List<Card> deckOfCards = new List<Card>();
    List<string> images = new List<string>(new string[] { "elephant", "giraffe", "gorilla", "lion", "moose", "hippopotamus", "sloth", "zebra" });
    Card card;
    Timer timer;
    PlaytimeTimer playtimeTimer;
    

    public void SetMatch(Card obj)
    {
        obj.isMatched = true;
    }

    public void SetDown(Card obj)
    {
        if(obj.isMatched == false)
        obj.isFaceUp = false;
    }

    void Start()
    {
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
        timer = GetComponent<Timer>();
        playtimeTimer = GetComponent<PlaytimeTimer>();
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
        BuildGrid();
        if (playerHasWon) BuildWinPrompt();
        GUILayout.EndArea();
    }

    private void BuildWinPrompt()
    {
        int winPromptW = 100;
        int winPromptH = 90;
        float halfScreenW = Screen.width * 0.5f;
        float halfScreenH = Screen.height * 0.5f;
        float halfPromptW = winPromptW * 0.5f;
        float halfPromptH = winPromptH * 0.5f;

        GUI.BeginGroup(new Rect(halfScreenW - halfPromptW,
            halfScreenH - halfPromptH, winPromptW, winPromptH), "YOU WIN!");
        if(GUI.Button(new Rect(10,40,80,20),"Play again"))
        {
            SceneManager.LoadScene("titleScene");
        }
        GUI.EndGroup();

    }

    private void BuildGrid()
    {
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
                if (GUILayout.Button(Resources.Load(img) as Texture2D, GUILayout.Width(cardWidth)))
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
        public string imgDown;
        public int id;

        public Card(string img, int id)
        {
            this.img = img;
            this.id = id;
            imgDown = "blank-01";
        }
    }

}