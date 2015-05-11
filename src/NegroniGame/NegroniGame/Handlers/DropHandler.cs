namespace NegroniGame.Handlers
{
    using System.Collections.Generic;
    using Microsoft.Xna.Framework;
    using System;

    public sealed class DropHandler
    {
        // Singleton !
        private static DropHandler instance;

        private DropHandler()
        {
            this.DropList = new List<Drop>();
            this.IndexForDeletion = new List<int>();
        }

        public static DropHandler Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new DropHandler();
                }
                return instance;
            }
        }

        public List<Drop> DropList { get; private set; }
        public List<int> IndexForDeletion { get; private set; }

        public void AddDrop(Drop newDrop)
        {
            this.DropList.Add(newDrop);
        }

        public void Update(GameTime gameTime)
        {
            for (int index = 0; index < this.DropList.Count; index++)
            {
                bool isTimeUp = false;

                if (this.DropList[index] != null)
                {
                    isTimeUp = this.DropList[index].Update(gameTime);
                }

                // deletes the drop if time is up
                if (isTimeUp)
                {
                    this.IndexForDeletion.Add(index);
                }
            }

            // picks up drop
            for (int index = 0; index < this.DropList.Count; index++)
            {
                if (this.DropList[index] != null && Player.Instance.DestinationPosition.Intersects(this.DropList[index].DropPosition))
                {
                    if (this.DropList[index].Name == "Coins")
                    {
                        Player.Instance.Coins = new Items.Coins(Player.Instance.Coins.Amount + this.DropList[index].Amount);

                        Toolbar.SystemMsg.Instance.AllMessages.Add(new Dictionary<string, Color>() { { String.Format(">> You picked up {0} {1}.", this.DropList[index].Amount, this.DropList[index].Name), Color.Beige } });

                        GameScreen.Instance.PickUpSound.Play();

                        this.IndexForDeletion.Add(index);
                    }

                    if (this.DropList[index].Name == "HP Potion")
                    {
                        Player.Instance.Elixirs = new Items.ElixirsHP(Player.Instance.Elixirs.Count + 1);

                        Toolbar.SystemMsg.Instance.AllMessages.Add(new Dictionary<string, Color>() { { String.Format(">> You picked up 1 {0}.", this.DropList[index].Name), Color.Beige } });

                        GameScreen.Instance.PickUpSound.Play();

                        this.IndexForDeletion.Add(index);
                    }
                }
            }

            foreach (int index in IndexForDeletion)
            {
                this.DropList[index] = null;
            }

            this.IndexForDeletion = new List<int>();
        }


        public void Draw()
        {
            // drop
            foreach (Drop drop in DropList)
            {
                if (drop != null)
                {
                    drop.Draw();
                }
            }
        }
    }
}
