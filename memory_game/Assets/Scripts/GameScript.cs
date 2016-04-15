using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts;
using UnityEngine.UI;

/// <summary>
/// Main class of game assigned to GameScreen game object.
/// </summary>
public class GameScript : MonoBehaviour
{
    public GameObject promptTimeText;
    public GameObject timeText;
    public GameObject messageText;

    /// <summary>
    /// Store dimensions of cards grid.
    /// </summary>
    public int columns = 4;
    public int rows = 4;

    /// <summary>
    /// Array of cards displayed on screen.
    /// </summary>
    public Card[,] gridOfCards;

    /// <summary>
    /// List for storing two curently flipped cards.
    /// </summary>
    public List<Card> cardsFlipped;

    /// <summary>
    /// Store amount of cards used for build grid.
    /// </summary>
    static int totalCards;

    /// <summary>
    /// Store amount of matches needed to win game.
    /// </summary>
    int matchesNeededToWin;

    /// <summary>
    /// Store amount of matches made.
    /// </summary>
    int matchesMade = 0;

    /// <summary>
    /// Width of card in pixels.
    /// </summary>
    int cardWidth = Screen.height/5;

    /// <summary>
    /// Flag to prevent clicking.
    /// </summary>
    bool playerCanClick; 

    /// <summary>
    /// Flag indicating if player has won.
    /// </summary>
    bool playerHasWon = false;

    /// <summary>
    /// List for building deck of cards.
    /// </summary>
    List<Card> deckOfCards = new List<Card>();

    /// <summary>
    /// List of images names. 
    /// </summary>
    List<string> images = new List<string>(new string[] { "1", "2", "3", "4", "5", "6", "7", "8" });

    Card card;
    PlaytimeTimer playtimeTimer;
    ModalPanel modalPanel;
    public GUIStyle guiStyle = new GUIStyle();

    /// <summary>
    /// Function start timer if it is not started
    /// </summary>
    public void StartTimer()
    {
        if (!playtimeTimer.start)
        {
            playtimeTimer.StartTimer();
        }
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

    /// <summary>
    /// Function called on start.
    /// <remarks>
    /// Function initialize fields. 
    /// </remarks>
    /// </summary>
    void Start()
    {
        totalCards = columns * rows;
        matchesNeededToWin = totalCards / 2;
        playerCanClick = true;
        gridOfCards = new Card[rows, columns];
        cardsFlipped = new List<Card>();
        playtimeTimer = GetComponent<PlaytimeTimer>();
        modalPanel = GetComponent<ModalPanel>();
        BuildDeck();
        InitializeGridOfCards();
    }

    /// <summary>
    /// Function called on every frame.
    /// <remarks>
    /// Building UI elements is placed here.
    /// </remarks>
    /// </summary>
    void OnGUI()
    {
        GUILayout.BeginArea(new Rect(0, 0, Screen.width, Screen.height));
        TextAssign();
        DisplayGrid();
        if (playerHasWon)
        {
            modalPanel.ActivatePanel();
        }
        GUILayout.EndArea();
    }

    /// <summary>
    /// Function building deck of cards from images.
    /// <remarks>
    /// Two different cards of every image are created. Builded deck are shuffled. </remarks>
    /// </summary>
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

    /// <summary>
    /// Function initialize elemets of GridOfCards with random picked element from deckOfCards
    /// </summary>
    private void InitializeGridOfCards()
    {
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

    /// <summary>
    /// Function assign text of PromptTime, Time and Message components
    /// </summary>
    void TextAssign()
    {
        Text time = timeText.GetComponent<Text>();
        time.text = playtimeTimer.timerString;

        Text prompt = promptTimeText.GetComponent<Text>();
        prompt.text = playtimeTimer.timerString;

        Text message = messageText.GetComponent<Text>();
        message.text = "Your time";
    }

    /// <summary>
    /// Function for displaying GridOfCards.
    /// <remarks>
    /// Every card is dispalyed with proper image depending on card state.
    /// </remarks>
    /// </summary>
    private void DisplayGrid()
    {
        guiStyle.fixedHeight = (Screen.height / 5);
        guiStyle.fixedWidth = (Screen.width / 5);
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
                DisplayCard(card, img);
            }
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
        }
        GUILayout.FlexibleSpace();
        GUILayout.EndVertical();
    }

    /// <summary>
    /// Function display single card which can be flipped.
    /// <remarks>
    /// Buttons are disabled if cards are matched.
    /// Timer is started on first click.
    /// </remarks>
    /// </summary>
    /// <param name="card">Card to display</param>
    /// <param name="img">Image name</param>
    private void DisplayCard(Card card, string img)
    {
        GUI.enabled = !card.isMatched;
        if (GUILayout.Button(Resources.Load(img) as Texture2D, guiStyle, GUILayout.Width(cardWidth)))
        {
            if (playerCanClick)
            {
                FlipCardFaceUp(card);
                StartTimer();
            }
        }
        GUI.enabled = true;
    }

    /// <summary>
    /// Function change card state to faceUp.
    /// <remarks>
    /// Only two cards can be fliped at a time.
    /// </remarks>
    /// </summary>
    /// <param name="card">Card to flip</param>
    private void FlipCardFaceUp(Card card)
    {
        card.isFaceUp = true;
        if (cardsFlipped.Contains(card) == false)
        {
            cardsFlipped.Add(card);
            if (cardsFlipped.Count == 2)
            {
                CheckIfMatched();
            }
            if (cardsFlipped.Count > 2)
            {
                playerCanClick = false;
                cardsFlipped.ForEach(SetDown);
                cardsFlipped = new List<Card>();
                playerCanClick = true;
            }
        }
    }

    /// <summary>
    /// Function check if two flipped cards are matched.
    /// <remarks>
    /// Img string of cards is checked.
    /// </remarks>
    /// </summary>
    private void CheckIfMatched()
    {
        if (cardsFlipped[0].img.ToString() == cardsFlipped[1].img.ToString())
        {
            playerCanClick = false;
            cardsFlipped.ForEach(SetMatch);
            matchesMade++;
            CheckIfWin();
            cardsFlipped = new List<Card>();
            playerCanClick = true;
        }
    }

    /// <summary>
    /// Function check if player has won.
    /// <remarks>
    /// Timer is stopped on win.
    /// </remarks>
    /// </summary>
    private void CheckIfWin()
    {
        if (matchesMade >= matchesNeededToWin)
        {
            playerHasWon = true;
            playtimeTimer.Finnish();
        }
    }

    /// <summary>
    /// Class representing Card object
    /// </summary>
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
            imgDown = "blank";
        }
    }
}