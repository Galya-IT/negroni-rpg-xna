namespace NegroniGame
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using Microsoft.Xna.Framework.Input;
    using NegroniGame.Interfaces;
    using NegroniGame.SystemFunctions;
    using System;
    using System.Collections.Generic;

    public sealed class Player : SpriteObjectAnime, IReact, IPlayer //no class can inherit from class Player
    {
        // Singleton !
        private static Player instance;

        #region Fields Declaration

        private const float PLAYER_SPEED = 2f;
        private const float PLAYER_ANIM_SPEED = 200f;

        private Vector2 playerPosition = new Vector2((float)GameScreen.ScreenWidth / 2, (float)GameScreen.ScreenHeight / 2 - 50);

        private readonly List<Texture2D> playerTextures = GameScreen.Instance.PlayerTextures;

        private Texture2D playerAnim = GameScreen.Instance.PlayerTextures[3];

        private Rectangle animSourcePosition;

        private float elapsedTimePlayerAnim;

        private int frames = 0;

        private int hpPointsCurrent;

        private int weaponDmg;

        private int armorDef;

        //private string name;

        private Random randomGenerator;

        private Rectangle reactRect;

        #endregion

        private Player() { }

        public static Player Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Player();
                }
                return instance;
            }
        }

        #region Properties Declarations

        public Rectangle DestinationPosition { get; private set; }
        public Vector2 CenterOfPlayer { get; private set; }
        public DirectionsEnum Direction { get; private set; }

        public int HpPointsCurrent
        {
            get
            {
                return this.hpPointsCurrent;
            }

            private set
            {
                if (value < 0)
                {
                    throw new SystemFunctions.Exceptions.InvalidAmountException("The amount must be positive or zero!");
                }
                this.hpPointsCurrent = value;
            }
        }

        public int WeaponDmg
        {
            get
            {
                return this.weaponDmg;
            }

            private set
            {
                if (value < 0)
                {
                    throw new SystemFunctions.Exceptions.InvalidAmountException("The amount must be positive or zero!");
                }
                this.weaponDmg = value;
            }
        }

        public int ArmorDef
        {
            get
            {
                return this.armorDef;
            }

            private set
            {
                if (value < 0)
                {
                    throw new SystemFunctions.Exceptions.InvalidAmountException("The amount must be positive or zero!");
                }
                this.armorDef = value;
            }
        }

        public Items.Coins Coins { get; set; } //  TO DO PRIVATE
        public Items.ElixirsHP Elixirs { get; set; }
        public IWeapon Weapon { get; private set; }
        public IShield Shield { get; private set; }
        public IHelmet Helmet { get; private set; }
        public IRobe Robe { get; private set; }
        public IGloves Gloves { get; private set; }
        public IBoots Boots { get; private set; }

        #endregion

        public void Initialize()
        {
            base.Name = "Elvina";
            this.HpPointsCurrent = GameSettings.HP_POINTS_INITIAL;
            this.Direction = SystemFunctions.DirectionsEnum.South;

            // 16 = half of texture width
            this.CenterOfPlayer = new Vector2(this.playerPosition.X + 16, this.playerPosition.Y + 16);

            this.Coins = new Items.Coins(1000);
            this.Elixirs = new Items.ElixirsHP(2);
            this.Weapon = new Items.Weapon.NewbieStaff();

            this.WeaponDmg = this.Weapon.Attack;
            this.ArmorDef = 0;

            this.randomGenerator = new Random();

            this.IsActive = true;
        }

        public override void Update(GameTime gameTime)
        {
            KeyboardState ks = Keyboard.GetState();

            Move(gameTime, ks);

            this.CenterOfPlayer = new Vector2(this.playerPosition.X, this.playerPosition.Y);

            if (this.Weapon == null)
            {
                this.WeaponDmg = 5;
            }
            else
            {
                this.WeaponDmg = this.Weapon.Attack;
            }

            this.ArmorDef = 0;

            if (this.Shield != null)
            {
                this.ArmorDef += this.Shield.Defence;
            }
            if (this.Helmet != null)
            {
                this.ArmorDef += this.Helmet.Defence;
            }
            if (this.Robe != null)
            {
                this.ArmorDef += this.Robe.Defence;
            }
            if (this.Gloves != null)
            {
                this.ArmorDef += this.Gloves.Defence;
            }
            if (this.Boots != null)
            {
                this.ArmorDef += this.Boots.Defence;
            }
        }

        public int DrinkFromWell()
        {
            int restoredPoints = GameSettings.HP_POINTS_INITIAL - this.HpPointsCurrent;

            this.HpPointsCurrent = GameSettings.HP_POINTS_INITIAL;

            GameScreen.Instance.DrinkWell.Play();

            return restoredPoints;
        }

        public void DrinkElixir()
        {
            this.HpPointsCurrent += Handlers.ElixirsHandler.Instance.RestoredPoints;
            this.Elixirs = new Items.ElixirsHP(this.Elixirs.Count - 1);

            GameScreen.Instance.DrinkElixir.Play();
        }

        public void TakeDamage(int damage)
        {
            if (this.HpPointsCurrent > 0)
            {
                this.HpPointsCurrent -= damage;
            }
            if (this.HpPointsCurrent < 0)
            {
                this.HpPointsCurrent = 0;
            }

            int index = this.randomGenerator.Next(0, 3);
            GameScreen.Instance.HitSounds[index].Play();
        }

        public void DestroyItem(string itemForDestruction)
        {
            switch (itemForDestruction)
            {
                case "weapon":
                    Toolbar.SystemMsg.Instance.AllMessages.Add(new Dictionary<string, Color>() { { String.Format(">> {0} destroyed.", this.Weapon.Name), Color.Red } });
                    this.Weapon = null;
                    break;
                case "shield":
                    Toolbar.SystemMsg.Instance.AllMessages.Add(new Dictionary<string, Color>() { { String.Format(">> You destroyed {0}.", this.Shield.Name), Color.Red } });
                    this.Shield = null;
                    break;
                case "gloves":
                    Toolbar.SystemMsg.Instance.AllMessages.Add(new Dictionary<string, Color>() { { String.Format(">> You destroyed {0}.", this.Gloves.Name), Color.Red } });
                    this.Gloves = null;
                    break;
                case "robe":
                    Toolbar.SystemMsg.Instance.AllMessages.Add(new Dictionary<string, Color>() { { String.Format(">> You destroyed {0}.", this.Robe.Name), Color.Red } });
                    this.Robe = null;
                    break;
                case "boots":
                    Toolbar.SystemMsg.Instance.AllMessages.Add(new Dictionary<string, Color>() { { String.Format(">> You destroyed {0}.", this.Boots.Name), Color.Red } });
                    this.Boots = null;
                    break;
                case "helmet":
                    Toolbar.SystemMsg.Instance.AllMessages.Add(new Dictionary<string, Color>() { { String.Format(">> You destroyed {0}.", this.Helmet.Name), Color.Red } });
                    this.Helmet = null;
                    break;
            }
        }

        public void BuyItem(Interfaces.IItem newItem, int coinsSpent)
        {
            if (coinsSpent <= this.Coins.Amount)
            {
                if (newItem.ToString().Contains("Elixir"))
                {
                    CompleteTransaction(newItem, coinsSpent);
                    this.Elixirs = (Items.ElixirsHP)newItem;
                }
                else if (newItem.ToString().Contains("Boots"))
                {
                    if (this.Boots != null)
                    {
                        Toolbar.SystemMsg.Instance.AllMessages.Add(new Dictionary<string, Color>() { { String.Format(">> First destroy your Boots!"), Color.Red } });
                    }
                    else
                    {
                        CompleteTransaction(newItem, coinsSpent);
                        this.Boots = (Interfaces.IBoots)newItem;
                    }
                }
                else if (newItem.ToString().Contains("Gloves"))
                {
                    if (this.Gloves != null)
                    {
                        Toolbar.SystemMsg.Instance.AllMessages.Add(new Dictionary<string, Color>() { { String.Format(">> First destroy your Gloves!"), Color.Red } });
                    }
                    else
                    {
                        CompleteTransaction(newItem, coinsSpent);
                        this.Gloves = (Interfaces.IGloves)newItem;
                    }
                }
                else if (newItem.ToString().Contains("Helmet"))
                {
                    if (this.Helmet != null)
                    {
                        Toolbar.SystemMsg.Instance.AllMessages.Add(new Dictionary<string, Color>() { { String.Format(">> First destroy your Helmet!"), Color.Red } });
                    }
                    else
                    {
                        CompleteTransaction(newItem, coinsSpent);
                        this.Helmet = (Interfaces.IHelmet)newItem;
                    }
                }
                else if (newItem.ToString().Contains("Robe"))
                {
                    if (this.Robe != null)
                    {
                        Toolbar.SystemMsg.Instance.AllMessages.Add(new Dictionary<string, Color>() { { String.Format(">> First destroy your Robe!"), Color.Red } });
                    }
                    else
                    {
                        CompleteTransaction(newItem, coinsSpent);
                        this.Robe = (Interfaces.IRobe)newItem;
                    }
                }
                else if (newItem.ToString().Contains("Shield"))
                {
                    if (this.Shield != null)
                    {
                        Toolbar.SystemMsg.Instance.AllMessages.Add(new Dictionary<string, Color>() { { String.Format(">> First destroy your Shield!"), Color.Red } });
                    }
                    else
                    {
                        CompleteTransaction(newItem, coinsSpent);
                        this.Shield = (Interfaces.IShield)newItem;
                    }
                }
                else if (newItem.ToString().Contains("Weapon"))
                {
                    if (this.Weapon != null)
                    {
                        Toolbar.SystemMsg.Instance.AllMessages.Add(new Dictionary<string, Color>() { { String.Format(">> First destroy your Weapon!"), Color.Red } });
                    }
                    else
                    {
                        CompleteTransaction(newItem, coinsSpent);
                        this.Weapon = (Interfaces.IWeapon)newItem;
                    }
                }
            }
            else
            {
                Toolbar.SystemMsg.Instance.AllMessages.Add(new Dictionary<string, Color>() { { ">> Not enough coins.", Color.Red } });
            }
        }

        public void RentHelper()
        {
            int newCoinsAmount = this.Coins.Amount - GameSettings.HELPER_RENTING_PRICE;
            this.Coins = new Items.Coins(newCoinsAmount);
            Toolbar.SystemMsg.Instance.AllMessages.Add(new Dictionary<string, Color>() { { ">> You rented a helper.", Color.DarkGoldenrod } });
        }

        public override void Draw()
        {
            if (GameScreen.Instance.GameState != 3)
            {
                new SystemFunctions.Sprite(this.playerAnim, this.DestinationPosition, this.animSourcePosition).DrawBoxAnim();
            }
            else
            {
                new SystemFunctions.Sprite(this.playerTextures[4], new Vector2(this.DestinationPosition.X, this.DestinationPosition.Y)).Draw();
            }
        }

        private void CompleteTransaction(Interfaces.IItem newItem, int coinsSpent)
        {
            int newCoinsAmount = this.Coins.Amount - coinsSpent;
            this.Coins = new Items.Coins(newCoinsAmount);
            Toolbar.SystemMsg.Instance.AllMessages.Add(new Dictionary<string, Color>() { { String.Format(">> You bought {0}.", newItem.Name), Color.DarkGoldenrod } });

            if (newItem.ToString().Contains("Weapon"))
            {
                GameScreen.Instance.WeaponBought.Play();
            }
            else if (newItem.ToString().Contains("Armor"))
            {
                GameScreen.Instance.ArmorBought.Play();
            }
            else if (newItem.ToString().Contains("ElixirsHP"))
            {
                GameScreen.Instance.ElixirBought.Play();
            }
        }

        // playerTextures - right, left, up, down
        private void Move(GameTime gameTime, KeyboardState ks)
        {
            if (ks.IsKeyDown(Keys.Right) || ks.IsKeyDown(Keys.D))
            {
                if (playerPosition.X < GameScreen.ScreenWidth - 30)
                {
                    Rectangle newPosition = new Rectangle((int)(this.playerPosition.X + PLAYER_SPEED), (int)(this.playerPosition.Y), 32, 32);

                    if (!IntersectsWithObjects(newPosition))
                    {
                        this.playerPosition.X += PLAYER_SPEED;
                    }
                    this.playerAnim = this.playerTextures[0];
                    this.animSourcePosition = AnimatePlayer(gameTime);
                    this.Direction = SystemFunctions.DirectionsEnum.East;
                }
            }
            else if (ks.IsKeyDown(Keys.Left) || ks.IsKeyDown(Keys.A))
            {
                if (playerPosition.X > 0)
                {
                    Rectangle newPosition = new Rectangle((int)(this.playerPosition.X - PLAYER_SPEED), (int)(this.playerPosition.Y), 32, 32);

                    if (!IntersectsWithObjects(newPosition))
                    {
                        this.playerPosition.X -= PLAYER_SPEED;
                    }
                    this.playerAnim = this.playerTextures[1];
                    this.animSourcePosition = AnimatePlayer(gameTime);
                    this.Direction = SystemFunctions.DirectionsEnum.West;
                }
            }
            else if (ks.IsKeyDown(Keys.Up) || ks.IsKeyDown(Keys.W))
            {
                if (playerPosition.Y > 0)
                {
                    Rectangle newPosition = new Rectangle((int)(this.playerPosition.X), (int)(this.playerPosition.Y - PLAYER_SPEED), 32, 32);

                    if (!IntersectsWithObjects(newPosition))
                    {
                        this.playerPosition.Y -= PLAYER_SPEED;
                    }
                    this.playerAnim = this.playerTextures[2];
                    this.animSourcePosition = AnimatePlayer(gameTime);
                    this.Direction = SystemFunctions.DirectionsEnum.North;
                }
            }
            else if (ks.IsKeyDown(Keys.Down) || ks.IsKeyDown(Keys.S))
            {
                if (playerPosition.Y <= GameScreen.ScreenHeight - 170)
                {
                    Rectangle newPosition = new Rectangle((int)(this.playerPosition.X), (int)(this.playerPosition.Y + PLAYER_SPEED), 32, 32);

                    if (!IntersectsWithObjects(newPosition))
                    {
                        this.playerPosition.Y += PLAYER_SPEED;
                    }
                    this.playerAnim = this.playerTextures[3];
                    this.animSourcePosition = AnimatePlayer(gameTime);
                    this.Direction = SystemFunctions.DirectionsEnum.South;
                }
            }
            else
            {
                this.animSourcePosition = new Rectangle(64, 0, 32, 32);
            }

            this.DestinationPosition = new Rectangle((int)this.playerPosition.X, (int)this.playerPosition.Y, 32, 32);

            this.reactRect = new Rectangle(this.DestinationPosition.X - 10, this.DestinationPosition.Y - 10, this.DestinationPosition.Width + 10, this.DestinationPosition.Height + 10);
        }

        private bool IntersectsWithObjects(Rectangle newPosition)
        {
            bool doesIntersectWithMobs = false;

            foreach (Monsters.Monster monster in Handlers.MonstersHandler.Instance.SpawnedMobs)
            {
                if (monster.DestinationPosition.Intersects(newPosition))
                {
                    doesIntersectWithMobs = true;
                    break;
                }
            }

            if (Well.Instance.WellPosition.Intersects(newPosition)
                || Handlers.SceneryHandler.Instance.MarketPosition.Intersects(newPosition)
                || NpcSorcerer.Instance.DrawRect.Intersects(newPosition)
                || doesIntersectWithMobs)
            {
                return true;
            }

            return false;
        }

        private Rectangle AnimatePlayer(GameTime gameTime)
        {
            this.elapsedTimePlayerAnim += (float)gameTime.ElapsedGameTime.TotalMilliseconds;

            if (this.elapsedTimePlayerAnim >= PLAYER_ANIM_SPEED)
            {
                if (this.frames >= 2)
                {
                    this.frames = 0;
                }
                else
                {
                    this.frames++;
                }
                this.elapsedTimePlayerAnim = 0;
            }

            // if on frame 0 - top up position 0
            return new Rectangle(32 * this.frames, 0, 32, 32);
        }

        public Rectangle ReactRect
        {
            get { return this.reactRect; }
        }

        public void DoAction<T>(IReact obj)
        {
            throw new NotImplementedException();
        }
    }
}
