using System;
using System.Collections.Generic;

namespace ConsoleApplication
{
    public class Card{
        public string value;
        public string suit;
        public int num_val;
        public Card(int suit_num, int card_num){
            num_val = card_num;
            switch (card_num){
                case 1:
                    value = "A";
                    break;
                case 11:
                    value = "J";
                    break;
                case 12:
                    value = "Q";
                    break;
                case 13:
                    value = "K";
                    break;
                default:
                    value = $"{card_num}";
                    break;
            }
            switch (suit_num){
                case 1:
                    suit = "Clubs";
                    break;
                case 2:
                    suit = "Spades";
                    break;
                case 3:
                    suit = "Hearts";
                    break;
                case 4:
                    suit = "Diamonds";
                    break;
            }
        }
    }
    public class Deck{
        List<Card> cards = new List<Card>();
        Random rnd = new Random();
        public Deck(){
            for(int i = 1; i<=4;i++){
                for(int c = 1; c<=13;c++){
                    cards.Add(new Card(i,c));
                }
            }
        }
        public void Shuffle(){
            int n = cards.Count;
            while ( n > 1){
                n--;
                int k = rnd.Next(n + 1);
                Card temp = cards[k];
                cards[k] = cards[n];
                cards[n] = temp;
            }
        }
        public void Reset(){
            cards.Clear();
            for(int i = 1; i<=4;i++){
                for(int c = 1; c<=13;c++){
                    cards.Add(new Card(i,c));
                }
            }
        }
        public Card Deal(){
            int top = cards.Count-1;
            Card dealt = cards[top];
            cards.RemoveAt(top); 
            return dealt;
        
        }
    }
    public class Player{
        public string name;
        List<Card> hand = new List<Card>();

        public Player(string pname){
            name = pname;
        }
        public Card Draw(Deck activeDeck){
            Card held = activeDeck.Deal();
            hand.Add(held);
            return held;
        }
        public bool Discard(Card pcard){
            foreach (Card car in hand){
                if (car == pcard){
                    hand.Remove(pcard);
                return true;
                }
            }
            return false;
        }
    }
    public class Program
    {
        public static void Main(string[] args)
        {
            
        }
    }
}
