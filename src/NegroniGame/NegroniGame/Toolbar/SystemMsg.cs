namespace NegroniGame.Toolbar
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using System;
    using System.Collections.Generic;
    using SystemFunctions;

    public sealed class SystemMsg
    {
        // Singleton !
        private static SystemMsg instance;

        private SystemMsg()
        {
            this.Font = GameScreen.Instance.FontMessages;
            this.LinesAsSprites = new List<Sprite>();
            this.ScreenHeight = GameScreen.ScreenHeight;
            this.ScreenWidth = GameScreen.ScreenWidth;
            this.AllMessages = new List<Dictionary<string, Color>>();
        }

        public static SystemMsg Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new SystemMsg();
                }
                return instance;
            }
        }

        public void GetLastMessages()
        {
            // defines the position of the first message
            this.MsgXPos = 130;
            this.MsgYPos = this.ScreenHeight - 20;

            LinesAsSprites = new List<Sprite>();

            if (AllMessages.Count > 0)
            {
                int endIndex;

                if (AllMessages.Count > 5)
                {
                    endIndex = AllMessages.Count - 6;
                }
                else
                {
                    endIndex = 0;
                }

                for (int i = AllMessages.Count - 1; i >= endIndex; i--)
                {
                    // gets the one record in the Dictionary
                    foreach (var record in AllMessages[i])
                    {
                        this.LinesAsSprites.Add(new Sprite(this.Font, record.Key, new Vector2(this.MsgXPos, this.MsgYPos), record.Value));
                        this.MsgYPos -= 15;
                    }
                }
            }
        }

        public void DrawText()
        {
            int msgForDisplay = (LinesAsSprites.Count <= 5) ? LinesAsSprites.Count : 5;

            for (int i = 0; i < msgForDisplay; i++)
            {
                this.LinesAsSprites[i].DrawText();
            }
        }

        //public void AddMessages(string item)
        //{
        //    // elixirs handler 
        //    this.AllMessages.Add(new Dictionary<string, Color>() { { String.Format(">> You restored {0} HP.", this.RestoredPoints), Color.Aquamarine } });
        //    this.AllMessages.Add(new Dictionary<string, Color>() { { String.Format(">> 1 HP Elixir destroyed."), Color.Red } });
        //    this.AllMessages.Add(new Dictionary<string, Color>() { { String.Format(">> {0} seconds to reuse Elixir.", REUSE_TIME - (int)this.elapsedTimeElixir), Color.Aquamarine } });

        //    // monster
        //    this.AllMessages.Add(new Dictionary<string, Color>() { { String.Format(">> {0} did you {1} dmg.", this.Name, this.Damage), Color.Red } });

        //    // monsters handler
        //    this.AllMessages.Add(new Dictionary<string, Color>() { { String.Format(">> {0} died.", this.SpawnedMobs[mobIndex].Name), Color.Cyan } });
        //    this.AllMessages.Add(new Dictionary<string, Color>() { { String.Format(">> {0} droped {1} {2}.", this.SpawnedMobs[mobIndex].Name, currentDrop.Amount, currentDrop.Name), Color.SpringGreen } });

        //    // player
        //    this.AllMessages.Add(new Dictionary<string, Color>() { { String.Format(">> {0} destroyed.", item), Color.Red } });

        //    // scenery
        //    this.AllMessages.Add(new Dictionary<string, Color>() { { String.Format(">> You picked up {0} {1}.", Scenery.Instance.DropList[index].Amount, Scenery.Instance.DropList[index].Name), Color.Beige } });

        //    // Shot
        //    this.AllMessages.Add(new Dictionary<string, Color>() { { ">> You missed the target.", Color.Pink } });
        //    this.AllMessages.Add(new Dictionary<string, Color>() { { String.Format(">> You did {0} dmg to {1}.", Player.Instance.WeaponDmg, Monsters.MonstersHandler.Instance.SpawnedMobs[index].Name), Color.Yellow } });

        //    // Well
        //    this.AllMessages.Add(new Dictionary<string, Color>() { { String.Format(">> Well restored you {0} HP.", restoredPoints), Color.Aquamarine } });
        //    this.AllMessages.Add(new Dictionary<string, Color>() { { String.Format(">> {0} seconds to reuse.", REUSE_TIME - (int)this.elapsedTimeToReuse), Color.Red } });
        //}

        public int MsgXPos { get; private set; }
        public int MsgYPos { get; private set; }
        public SpriteFont Font { get; private set; }
        public int ScreenWidth { get; private set; }
        public int ScreenHeight { get; private set; }
        public List<Dictionary<string, Color>> AllMessages { get; set; }
        public List<Sprite> LinesAsSprites { get; private set; }
    }
}
