namespace NegroniGame.Handlers
{
    using Microsoft.Xna.Framework;
    using NegroniGame.SystemFunctions;
    using System;

    public sealed class SceneryHandler
    {
        // Singleton !
        private static SceneryHandler instance;

        private SceneryHandler()
        {
            this.ScreenRectangle = new Rectangle(0, 0, GameScreen.ScreenWidth, GameScreen.ScreenHeight);
            this.ToolbarRectangle = new Rectangle(0, GameScreen.ScreenHeight - 134, 700, 134);
            this.PlayerPicRectangle = new Rectangle(13, GameScreen.ScreenHeight - 72, 96, 72);
            this.MarketPosition = new Rectangle(GameScreen.ScreenWidth - 115, 5, 102, 54);

            //this.DropList = new List<Drop>();
            //this.IndexForDeletion = new List<int>();
        }

        public static SceneryHandler Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new SceneryHandler();
                }
                return instance;
            }
        }

        public Rectangle ScreenRectangle { get; private set; }
        public Rectangle ToolbarRectangle { get; private set; }
        public Rectangle PlayerPicRectangle { get; private set; }
        public Rectangle MarketPosition { get; private set; }

        public Well Well { get; private set; }
        //public List<Drop> DropList { get; private set; }
        //public List<int> IndexForDeletion { get; private set; }


        //public void AddDrop(Drop newDrop)
        //{
        //    this.DropList.Add(newDrop);
        //}

        //public void Update(GameTime gameTime)
        //{
        //    UpdateDrop(gameTime);
        //}

        //public void UpdateDrop(GameTime gameTime)
        //{
        //    for (int index = 0; index < this.DropList.Count; index++)
        //    {
        //        bool isTimeUp = false;

        //        if (this.DropList[index] != null)
        //        {
        //            isTimeUp = this.DropList[index].Update(gameTime);
        //        }

        //        // deletes the drop if time is up
        //        if (isTimeUp)
        //        {
        //            this.IndexForDeletion.Add(index);
        //        }
        //    }

        //    // picks up drop
        //    for (int index = 0; index < Scenery.Instance.DropList.Count; index++)
        //    {
        //        if (this.DropList[index] != null && Player.Instance.DestinationPosition.Intersects(Scenery.Instance.DropList[index].DropPosition))
        //        {
        //            if (Scenery.Instance.DropList[index].Name == "Coins")
        //            {
        //                Player.Instance.Coins = new Items.Coins(Player.Instance.Coins.Amount + Scenery.Instance.DropList[index].Amount);

        //                Toolbar.SystemMsg.Instance.AllMessages.Add(new Dictionary<string, Color>() { { String.Format(">> You picked up {0} {1}.", Scenery.Instance.DropList[index].Amount, Scenery.Instance.DropList[index].Name), Color.Beige } });

        //                this.IndexForDeletion.Add(index);
        //            }
        //        }
        //    }

        //    foreach (int index in IndexForDeletion)
        //    {
        //        this.DropList[index] = null;
        //    }

        //    this.IndexForDeletion = new List<int>();
        //}


        public void Draw()
        {
            // backgroundTexture, toolbar, wellGraphic, playerPic, equipmentShopGraphic
            new Sprite(GameScreen.Instance.AllSceneryTextures[0], this.ScreenRectangle).DrawBox();
            new Sprite(GameScreen.Instance.AllSceneryTextures[1], this.ToolbarRectangle).DrawBox();
            Well.Instance.Draw();
            new Sprite(GameScreen.Instance.AllSceneryTextures[3], this.PlayerPicRectangle).DrawBox();
            new Sprite(GameScreen.Instance.AllSceneryTextures[4], this.MarketPosition).DrawBox();

            //// drop
            //foreach (Drop drop in DropList)
            //{
            //    if (drop != null)
            //    {
            //        drop.Draw();
            //    }
            //}

        }
    }
}
