﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Carcassonne
{
    public class DeckManager
    {

        #region Declarations

        public List<Deck> decks;
        public List<Texture2D> soldiers;
        private int activeDeck;

        #endregion

        #region Constructor

        public DeckManager()
        {
            decks = new List<Deck>();
            soldiers = new List<Texture2D>();
            activeDeck=0;
        }

        #endregion

        #region Initialize

        public void Initialize(ContentManager Content)
        {
            foreach (Deck deck in decks)
            {
                deck.Initialize(Content);
            }

            for (int i = 1; i <= 5; i++) 
                soldiers.Add(Content.Load<Texture2D>(@"Textures\soldiers\Soldier" + i));
           
            decks[0].reserveTile();
        }

        #endregion

        #region Properties

        public int CountAll
        {
            get
            {
                int deckSum=0;
               foreach(Deck deck in decks)
                   deckSum+=deck.Count;

               if (decks[0].Reserve)
                   return deckSum + 1;
               else
                   return deckSum;
            }
        }
        public int NextDeck
        {
            get { return (int)MathHelper.Min(activeDeck+1, decks.Count); }
        }

        public int Count
        {
            get { return decks[activeDeck].Count; }
        }

        #endregion

        #region Public Soldier Methods

        public Texture2D Soldier(int x)
        {
            return soldiers[x-1];
        }

        #endregion

        #region Public Deck Methods

        public void AddNewDeck()
        {
            decks.Add(new Deck());
        }

        public void AddTextureName(string name, int deckID)
        {
            decks[deckID].textureName.Add(name);
        }

        public void AddQuantities(int quantity, int deckID)
        {
            decks[deckID].deck.Add(quantity);
        }

        public Texture2D GetTileTexture(int x,int deck)
        {
            return decks[deck].GetTileTexture(x);
        }

        public Texture2D GetTileTexture(int x)
        {

                if (activeDeck == 0 && decks[activeDeck].Reserve && Count==0)
                {
                    activeDeck = NextDeck;
                    return decks[0].getReservedTile();
                }
               // if (Count == decks[1].fullDeck && activeDeck == 1)
                 //   return decks[1].GetTileTexture(0);

                if (Count == 0 && activeDeck >= 1)
                    return decks[1].getEndingTexture(0);
                          
              
            return decks[activeDeck].GetTileTexture(x);
        }

        public int GetRandomTileNo()
        {
            int no = decks[activeDeck].GetRandomTile();

         //   if (decks[activeDeck].Count ==0)
            //    activeDeck = NextDeck;

            return no;
        }

        public Texture2D GetRandomTile(int pos)
        {
                        
            return decks[activeDeck].GetTileTexture(pos);
        }


        #endregion

    }
}