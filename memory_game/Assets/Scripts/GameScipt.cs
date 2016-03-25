using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Threading;

public class GameScipt : MonoBehaviour {

    public GUISkin customSkin;
    static int columns = 4;
    static int rows = 4;
    static int totalCards = columns * rows;
    int matchesNeededToWin = totalCards / 2;
    int machesMade = 0;
    int cardWidth = 100;
    int cardHeight = 100;
    Card[,] gridOfCards;
    ArrayList arrayCardsFlipped;
    List<Card> deckOfCards = new List<Card>();
    bool playerCanClick; //flag to prevent clicking
    bool playerHasWon = false;
    List<string> images = new List<string>(new string[] { "elephant", "giraffe", "gorilla", "lion", "moose", "hippopotamus", "sloth", "zebra" });
    Card card;

    void Start()
    {
        playerCanClick = true;
        gridOfCards = new Card[rows, columns];
        arrayCardsFlipped = new ArrayList();
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
        for(int i =0; i<2; i++)
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
                if (GUILayout.Button(Resources.Load(card.img) as Texture2D, GUILayout.Width(cardWidth)))
                {
                    Debug.Log(card.img);
                }
            }
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
        }
        GUILayout.FlexibleSpace();

        GUILayout.EndVertical();
    }

    
   
    public class Card : System.Object
    {
        public bool isFaceUp = false;
        public bool isMatched = false;
        public string img;

        public Card()
        {
            img = "lion";
        }

        public Card(string img)
        {
            this.img = img;
        }
    }
}

public static class ThreadSafeRandom
{
    [ThreadStatic]
    private static System.Random Local;

    public static System.Random ThisThreadsRandom
    {
        get { return Local ?? (Local = new System.Random(unchecked(Environment.TickCount * 31 + Thread.CurrentThread.ManagedThreadId))); }
    }
}

static class MyExtensions
{
        public static void Shuffle<T>(this IList<T> list)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = ThreadSafeRandom.ThisThreadsRandom.Next(n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }
}