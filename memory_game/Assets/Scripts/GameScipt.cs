using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class GameScipt : MonoBehaviour {

    public GUISkin customSkin;
    static int columns = 4;
    static int rows = 4;
    static int totalCards = columns * rows;
    int matchesNeededToWin = totalCards / 2;
    int machesMade = 0;
    int cardWidth = 100;
    int cardHeight = 100;
    List<Card[]> arrayCards;
    Card[,] gridOfCards;
    ArrayList arrayCardsFlipped;
    bool playerCanClick; //flag to prevent clicking
    bool playerHasWon = false;

    void Start()
    {
        playerCanClick = true;
        arrayCards = new List<Card[]>();
        gridOfCards = new Card[rows, columns];
        arrayCardsFlipped = new ArrayList();

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                gridOfCards[i, j] = new Card();
            }
        }
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
    }
}
