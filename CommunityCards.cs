using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using static PokerGame.Deck;
using static PokerGame.Card;

namespace PokerGame
{
    public class CommunityCards
    {
        public List<Card> CommCards = new List<Card>(); //Declares Community Cards

        //CommunityCards Constructor
        public CommunityCards(Deck d) 
        {
            for (int i = 0; i < 3; i++)
            {
                CommCards.Add(d.DrawCard());
            }
            PrintCommCards();
        }

        //Draws first three cards of Community Cards
        public void DrawTurn(Deck d)
        {
            if (CommCards.Count == 3) 
            {
                CommCards.Add(d.DrawCard());
                PrintCommCards();
            } 
            else
            {
                Console.WriteLine("Can't do this");
            } 
        }

        //Draws fourth community card
        public void DrawRiver(Deck d)
        {
            if(CommCards.Count == 4)
            {
                CommCards.Add(d.DrawCard());
                PrintCommCards();
            }
            else
            {
                Console.WriteLine("Can't do this");
            }
        }

        //Prints community cards
        public void PrintCommCards()
        {
            foreach (Card e in CommCards)
                Console.Write("  {0}  ", e.ToString());
            Console.WriteLine();
        }
    }
}
