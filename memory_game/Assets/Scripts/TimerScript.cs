using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{   
    /// <summary>
    /// Class providing method for timer assigned to GameScreen obejct
    /// <remarks>
    /// Timer used for delay GUI actions
    /// </remarks>
    /// </summary>
    public class TimerScript : MonoBehaviour
    {   
        /// <summary>
        /// Holds delay value for flipping back card 
        /// </summary>
        public float flipBackInterval;

        /// <summary>
        /// Holds delay value for hiding matched cards
        /// </summary>
        public float hideInterval;

        public GameScript gameScript;

        void Awake()
        {
            gameScript = GetComponent<GameScript>();
        }

        public void Update()
        {
            DelayFlippingBackCard();
            DelayHidingMatched();
        }

        /// <summary>
        /// Function delay hiding matched cards
        /// </summary>
        private void DelayHidingMatched()
        {
            if (hideInterval > 0)
            {
                hideInterval -= Time.deltaTime;
            }
            if (hideInterval <= 0)
            {
                for (int i = 0; i < gameScript.rows; i++)
                {
                    for (int j = 0; j < gameScript.columns; j++)
                    {
                        gameScript.HideCard(gameScript.gridOfCards[i, j]);
                    }
                }
                hideInterval = 2;
            }
        }

        /// <summary>
        /// Function delay flipping back unmatched card
        /// </summary>
        private void DelayFlippingBackCard()
        {
            if (gameScript.cardsFlipped.Count == 2)
            {
                if (flipBackInterval > 0)
                {
                    flipBackInterval -= Time.deltaTime;
                }
                if (flipBackInterval <= 0)
                {
                    if (!gameScript.cardsFlipped[0].id.ToString().Equals(gameScript.cardsFlipped[1].id.ToString()))
                    {
                        gameScript.cardsFlipped.ForEach(gameScript.SetDown);                   
                    }                
                    gameScript.cardsFlipped = new List<GameScript.Card>();
                    flipBackInterval = 1.5f;
                }
            }
        }
    }
}