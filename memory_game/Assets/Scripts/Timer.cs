using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class Timer : MonoBehaviour
    {
        public float interval;

        public GameScript gameScript;

        void Awake()
        {
            gameScript = GetComponent<GameScript>();
        }

        public void Update()
        {
            if (gameScript.arrayCardsFlipped.Count == 2)
            {
                if (interval > 0)
                {
                    interval -= Time.deltaTime;
                }
                if (interval <= 0)
                {
                    if (gameScript.arrayCardsFlipped[0].id.ToString().Equals(gameScript.arrayCardsFlipped[1].id.ToString()))
                    {
                        gameScript.arrayCardsFlipped.ForEach(gameScript.SetMatch);
                    }
                    else
                    {
                        gameScript.arrayCardsFlipped.ForEach(gameScript.SetDown);
                    }
                    gameScript.arrayCardsFlipped = new List<GameScript.Card>();
                    interval = 2;
                }
            }
        }
    }
}
