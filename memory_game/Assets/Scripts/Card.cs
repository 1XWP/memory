using UnityEngine;
using System.Collections;

public class Card : object
{
        public bool isFaceUp = false;
        public bool isMatched = false;
        public string img;
        public string imgDown;

        public Card(string img)
        {
            this.img = img;
            imgDown = "blank-01";
        }   
}
