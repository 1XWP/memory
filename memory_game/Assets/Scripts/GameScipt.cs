using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts;
using System;

public class GameScipt : MonoBehaviour
{
    public GUISkin customSkin;

    static int columns = 4;
    static int rows = 4;
    static int totalCards = columns * rows;
    int matchesNeededToWin = totalCards / 2;
    int machesMade = 0;
    int cardWidth = 100;
    int cardHeight = 100;
    bool playerCanClick; //flag to prevent clicking
    bool playerHasWon = false;
    Card[,] gridOfCards;
    public List<Card> arrayCardsFlipped;
    List<Card> deckOfCards = new List<Card>();
    List<string> images = new List<string>(new string[] { "elephant", "giraffe", "gorilla", "lion", "moose", "hippopotamus", "sloth", "zebra" });
    Card card;

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

    private void BuildDeck()
    {
        for (int i = 0; i < 2; i++)
        {
            for (int j = 0; j < totalCards / 2; j++)
            {
                card = new Card(images.ElementAt(j));
                deckOfCards.Add(card);
            }
        }
        deckOfCards.Shuffle();
    }

    void OnGUI()
    {
        GUILayout.BeginArea(new Rect(0, 0, Screen.width, Screen.height));
        BuildGrid();
        GUILayout.EndArea();
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
                if (GUILayout.Button(Resources.Load(img) as Texture2D, GUILayout.Width(cardWidth)))
                {
                    FlipCardFaceUp(card);
                }
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
        arrayCardsFlipped.Add(card);

        if (arrayCardsFlipped.Count > 2)//player can flip only two cards at a time
        {
            playerCanClick = false;
            arrayCardsFlipped.ForEach(SetDown);
            arrayCardsFlipped = new List<Card>();
            playerCanClick = true;
        }  
    }

    private void SetDown(Card obj)
    {
        obj.isFaceUp = false;
    }
}