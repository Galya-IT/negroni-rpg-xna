namespace NegroniGame.Monsters
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using System;
    using System.Collections.Generic;

    public abstract class Monster : Interfaces.IMonster
    {
        # region Fields Declaration

        private const int MOB_SPEED = 1;
        private const float ANIM_DELAY = 200f;
        private const double WIDTH_OF_FULL_HEALTHBAR = 32;

        // the width of the health bar is 32, so we lose 3.2 width per 10 damage
        private const double MINUS_WIDTH_OF_HEALTH_BAR = 3.2;
        
        private int currentFrame = 0;
        private string name;
        private readonly int hpPointsInitial;
        private readonly List<Texture2D> monsterTextures;
        private Texture2D monsterAnim;
        private Rectangle monsterPosition;
        private Rectangle animSourcePosition;
        private int positionsToMove;
        private float elapsedTimeChangeAnim;
        private float elapsedTimeChangePos;
        private float elapsedTimeHit;
        private bool isInCombatState = false;
        private string changeDirection = "";
        private int damage;

        private readonly Random randomGenerator = new Random();

        # endregion

        public Monster(int numberOfMob, Rectangle initialMonsterPos, string monsterName, List<Texture2D> mobTextures, int initialHpPoints)
        {
            this.ID = numberOfMob;
            this.Name = monsterName;
            this.Attack = GameSettings.MOBS_ATTACK;
            this.Damage = this.Attack;
            this.monsterPosition = initialMonsterPos;
            this.monsterTextures = mobTextures;
            this.monsterAnim = this.monsterTextures[0];
            this.hpPointsInitial = initialHpPoints;
            this.HpPointsCurrent = hpPointsInitial;
            this.DirectionForMovement = -1;
            this.DestinationPosition = new Rectangle(this.monsterPosition.X, this.monsterPosition.Y, 32, 32);
            this.CurrentHealthBarWidth = WIDTH_OF_FULL_HEALTHBAR;
        }

        # region Properties Declaration

        public int ID { get; private set; }

        public string Name
        {
            get
            {
                return this.name;
            }
            private set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new SystemFunctions.Exceptions.InvalidNameException("The name can't be null or empty!");
                }
                this.name = value;
            }
        }
        public int Damage
        {
            get
            {
                return this.damage;
            }
            private set
            {
                if (value < 0)
                {
                    throw new SystemFunctions.Exceptions.InvalidAmountException("The amount must be positive or zero!");
                }
                this.damage = value;
            }
        }

        public int Attack { get; private set; }
        public int DirectionForMovement { get; private set; }
        public Rectangle DestinationPosition { get; private set; }
        public Rectangle LowHealthBarRect { get; private set; }
        public Rectangle FullHealthBarRect { get; private set; }
        public int HpPointsCurrent { get; set; }
        public double CurrentHealthBarWidth { get; set; }

        # endregion


        public void Update(GameTime gameTime)
        {
            // checks if the mob has to be in combat state
            if (this.isInCombatState == false &&
                    (this.HpPointsCurrent < this.hpPointsInitial
                    || Player.Instance.DestinationPosition.Intersects(new Rectangle(this.DestinationPosition.X - GameSettings.AGGRO_RANGE, this.DestinationPosition.Y - GameSettings.AGGRO_RANGE, 32 + (GameSettings.AGGRO_RANGE * 2), 32 + (GameSettings.AGGRO_RANGE * 2)))))
            {
                this.isInCombatState = true;
            }

            // chooses behaviour depending on the state
            if (this.isInCombatState == false)
            {
                MoveNormal(gameTime);
            }
            else
            {
                MoveCombat(gameTime);
            }

            // updates health bar over the mob
            if (this.HpPointsCurrent < 100)
            {
                this.CurrentHealthBarWidth = 32 - (this.hpPointsInitial - this.HpPointsCurrent) / 10 * MINUS_WIDTH_OF_HEALTH_BAR;
            }
        }

        private void MoveNormal(GameTime gameTime)
        {
            this.elapsedTimeChangePos += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (this.elapsedTimeChangePos >= GameSettings.TIME_TO_CHANGE_DIRECTION)
            {
                this.DirectionForMovement = this.randomGenerator.Next(1, 5);
                this.elapsedTimeChangePos = 0;
                this.positionsToMove = int.MinValue;
            }


            if (this.DirectionForMovement == (int)SystemFunctions.DirectionsEnum.North) // up
            {
                if (this.positionsToMove == int.MinValue)
                {
                    int maxPosition = (this.monsterPosition.Y > GameSettings.MOVE_MAX_LENGTH) ? GameSettings.MOVE_MAX_LENGTH : this.monsterPosition.Y;
                    maxPosition = (maxPosition <= 0) ? 0 : maxPosition;

                    this.positionsToMove = this.randomGenerator.Next(0, maxPosition);
                }
                else if (this.positionsToMove > 0)
                {
                    Rectangle newPosition = new Rectangle(this.monsterPosition.X, this.monsterPosition.Y - MOB_SPEED, 32, 32);

                    if (IntersectsWithObstacles(newPosition))
                    {
                        this.positionsToMove = int.MinValue;
                    }
                    else
                    {
                        this.monsterPosition.Y -= MOB_SPEED;
                        this.monsterAnim = this.monsterTextures[3];
                        this.positionsToMove -= MOB_SPEED;
                    }
                }
            }

            else if (this.DirectionForMovement == (int)SystemFunctions.DirectionsEnum.South) // down
            {
                if (this.positionsToMove == int.MinValue)
                {
                    int maxPosition = ((GameScreen.ScreenHeight - 170 - this.monsterPosition.Y) > GameSettings.MOVE_MAX_LENGTH) ? GameSettings.MOVE_MAX_LENGTH : (GameScreen.ScreenHeight - 170 - this.monsterPosition.Y);
                    maxPosition = (maxPosition < 0) ? 0 : maxPosition;

                    this.positionsToMove = this.randomGenerator.Next(0, maxPosition);
                }

                else if (this.positionsToMove > 0)
                {
                    Rectangle newPosition = new Rectangle(this.monsterPosition.X, this.monsterPosition.Y + MOB_SPEED, 32, 32);

                    if (IntersectsWithObstacles(newPosition))
                    {
                        this.positionsToMove = int.MinValue;
                    }
                    else
                    {
                        this.monsterPosition.Y += MOB_SPEED;
                        this.monsterAnim = this.monsterTextures[0];
                        this.positionsToMove -= MOB_SPEED;
                    }
                }
            }

            else if (this.DirectionForMovement == (int)SystemFunctions.DirectionsEnum.West) // left
            {
                if (this.positionsToMove == int.MinValue)
                {
                    int maxPosition = (this.monsterPosition.X > GameSettings.MOVE_MAX_LENGTH) ? GameSettings.MOVE_MAX_LENGTH : this.monsterPosition.X;
                    maxPosition = (maxPosition < 0) ? 0 : maxPosition;

                    this.positionsToMove = this.randomGenerator.Next(0, maxPosition);
                }

                else if (this.positionsToMove > 0)
                {
                    Rectangle newPosition = new Rectangle(this.monsterPosition.X - MOB_SPEED, this.monsterPosition.Y, 32, 32);

                    if (IntersectsWithObstacles(newPosition))
                    {
                        this.positionsToMove = int.MinValue;
                    }
                    else
                    {
                        this.monsterPosition.X -= MOB_SPEED;
                        this.monsterAnim = this.monsterTextures[1];
                        this.positionsToMove -= MOB_SPEED;
                    }
                }
            }

            else if (this.DirectionForMovement == (int)SystemFunctions.DirectionsEnum.East) // right
            {
                if (this.positionsToMove == int.MinValue)
                {
                    int maxPosition = ((GameScreen.ScreenWidth - 30 - this.monsterPosition.X) > GameSettings.MOVE_MAX_LENGTH) ? GameSettings.MOVE_MAX_LENGTH : (GameScreen.ScreenWidth - 30 - this.monsterPosition.X);
                    maxPosition = (maxPosition < 0) ? 0 : maxPosition;

                    this.positionsToMove = this.randomGenerator.Next(0, maxPosition);
                }

                else if (this.positionsToMove > 0)
                {
                    Rectangle newPosition = new Rectangle(this.monsterPosition.X + MOB_SPEED, this.monsterPosition.Y, 32, 32);

                    if (IntersectsWithObstacles(newPosition))
                    {
                        this.positionsToMove = int.MinValue;
                    }
                    else
                    {
                        this.monsterPosition.X += MOB_SPEED;
                        this.monsterAnim = this.monsterTextures[2];
                        this.positionsToMove -= MOB_SPEED;
                    }
                }
            }

            this.animSourcePosition = Animate(gameTime);
            this.DestinationPosition = new Rectangle(this.monsterPosition.X, this.monsterPosition.Y, 32, 32);

            this.LowHealthBarRect = new Rectangle(this.monsterPosition.X, this.monsterPosition.Y - 10, 32, 12);
            this.FullHealthBarRect = new Rectangle(this.monsterPosition.X, this.monsterPosition.Y - 10, (int)this.CurrentHealthBarWidth, 12);
        }

        public void Draw(GameTime gameTime)
        {
            new SystemFunctions.Sprite(this.monsterAnim, this.DestinationPosition, this.animSourcePosition).DrawBoxAnim();
            new SystemFunctions.Sprite(GameScreen.Instance.HealthBars[1], this.LowHealthBarRect).DrawBox();
            new SystemFunctions.Sprite(GameScreen.Instance.HealthBars[0], this.FullHealthBarRect).DrawBox();
        }

        private void MoveCombat(GameTime gameTime)
        {
            this.elapsedTimeHit += (float)gameTime.ElapsedGameTime.TotalSeconds;

            string direction = Math.Abs(Player.Instance.DestinationPosition.X - this.DestinationPosition.X)
                                        > Math.Abs(Player.Instance.DestinationPosition.Y - this.DestinationPosition.Y)
                                        ? "horizontal" : "vertical";

            if (this.changeDirection != "")
            {
                direction = this.changeDirection;
            }

            if (direction == "horizontal")
            {
                if (this.monsterPosition.X < Player.Instance.CenterOfPlayer.X || this.changeDirection == "right")
                {
                    Rectangle newPosition = new Rectangle(this.monsterPosition.X + MOB_SPEED, this.monsterPosition.Y, 32, 32);

                    if (!IntersectsWithObstacles(newPosition))
                    {
                        this.monsterPosition.X += MOB_SPEED;
                        this.monsterAnim = this.monsterTextures[2]; // moves right
                        this.DestinationPosition = new Rectangle(this.monsterPosition.X, this.monsterPosition.Y, 32, 32);
                    }
                    else if (Player.Instance.DestinationPosition.Intersects(newPosition))
                    {
                        if (this.elapsedTimeHit >= GameSettings.ATTACK_INTERVAL)
                        {
                            Player.Instance.TakeDamage(this.Damage);
                            Toolbar.SystemMsg.Instance.AllMessages.Add(new Dictionary<string, Color>() { { String.Format(">> {0} did you {1} dmg.", this.Name, this.Damage), Color.Red } });
                            this.elapsedTimeHit = 0;
                        }
                    }
                    else if (IntersectsWithObstaclesNoPlayer(newPosition))
                    {
                        this.changeDirection = "vertical";
                    }
                }
                else
                {
                    Rectangle newPosition = new Rectangle(this.monsterPosition.X - MOB_SPEED, this.monsterPosition.Y, 32, 32);

                    if (!IntersectsWithObstacles(newPosition))
                    {
                        this.monsterPosition.X -= MOB_SPEED;
                        this.monsterAnim = this.monsterTextures[1]; // moves left
                        this.DestinationPosition = new Rectangle(this.monsterPosition.X, this.monsterPosition.Y, 32, 32);
                    }
                    else if (Player.Instance.DestinationPosition.Intersects(newPosition))
                    {
                        if (this.elapsedTimeHit >= GameSettings.ATTACK_INTERVAL)
                        {
                            Player.Instance.TakeDamage(this.Damage);
                            Toolbar.SystemMsg.Instance.AllMessages.Add(new Dictionary<string, Color>() { { String.Format(">> {0} did you {1} dmg.", this.Name, this.Damage), Color.Red } });
                            this.elapsedTimeHit = 0;
                        }
                    }
                    else if (IntersectsWithObstaclesNoPlayer(newPosition))
                    {
                        this.changeDirection = "vertical";
                    }
                }
            }

            else if (direction == "vertical")
            {
                if (this.monsterPosition.Y < Player.Instance.CenterOfPlayer.Y || this.changeDirection == "down")
                {
                    Rectangle newPosition = new Rectangle(this.monsterPosition.X, this.monsterPosition.Y + MOB_SPEED, 32, 32);

                    if (!IntersectsWithObstacles(newPosition))
                    {
                        this.monsterPosition.Y += MOB_SPEED;
                        this.monsterAnim = this.monsterTextures[0]; // moves down
                        this.DestinationPosition = new Rectangle(this.monsterPosition.X, this.monsterPosition.Y + MOB_SPEED, 32, 32);
                    }
                    else if (Player.Instance.DestinationPosition.Intersects(newPosition))
                    {
                        if (this.elapsedTimeHit >= GameSettings.ATTACK_INTERVAL)
                        {
                            Player.Instance.TakeDamage(this.Damage);
                            Toolbar.SystemMsg.Instance.AllMessages.Add(new Dictionary<string, Color>() { { String.Format(">> {0} did you {1} dmg.", this.Name, this.Damage), Color.Red } });
                            this.elapsedTimeHit = 0;
                        }
                    }
                    else if (IntersectsWithObstaclesNoPlayer(newPosition))
                    {
                        this.changeDirection = "horizontal";
                    }
                }
                else
                {
                    Rectangle newPosition = new Rectangle(this.monsterPosition.X, this.monsterPosition.Y - MOB_SPEED, 32, 32);

                    if (!IntersectsWithObstacles(newPosition))
                    {
                        this.monsterPosition.Y -= MOB_SPEED;
                        this.monsterAnim = this.monsterTextures[3]; // moves up
                        this.DestinationPosition = new Rectangle(this.monsterPosition.X, this.monsterPosition.Y - MOB_SPEED, 32, 32);
                    }
                    else if (Player.Instance.DestinationPosition.Intersects(newPosition))
                    {
                        if (this.elapsedTimeHit >= GameSettings.ATTACK_INTERVAL)
                        {
                            Player.Instance.TakeDamage(this.Damage);
                            Toolbar.SystemMsg.Instance.AllMessages.Add(new Dictionary<string, Color>() { { String.Format(">> {0} did you {1} dmg.", this.Name, this.Damage), Color.Red } });
                            this.elapsedTimeHit = 0;
                        }
                    }
                    else if (IntersectsWithObstaclesNoPlayer(newPosition))
                    {
                        this.changeDirection = "horizontal";
                    }
                }
            }

            this.animSourcePosition = Animate(gameTime);

            this.LowHealthBarRect = new Rectangle(this.monsterPosition.X, this.monsterPosition.Y - 10, 32, 12);
            this.FullHealthBarRect = new Rectangle(this.monsterPosition.X, this.monsterPosition.Y - 10, (int)this.CurrentHealthBarWidth, 12);

            // Check if the obstacle is already gone and changes to normal direction
            if (this.changeDirection == "vertical")
            {
                if (this.monsterPosition.X < Player.Instance.CenterOfPlayer.X)
                {
                    Rectangle checkPosition = new Rectangle(this.monsterPosition.X + MOB_SPEED, this.monsterPosition.Y, 32, 32);

                    if (!IntersectsWithObstaclesNoPlayer(checkPosition))
                    {
                        this.changeDirection = "";
                    }
                }
                else
                {
                    Rectangle checkPosition = new Rectangle(this.monsterPosition.X - MOB_SPEED, this.monsterPosition.Y, 32, 32);

                    if (!IntersectsWithObstaclesNoPlayer(checkPosition))
                    {
                        this.changeDirection = "";
                    }
                }
            }
            else if (this.changeDirection == "horizontal")
            {
                if (this.monsterPosition.Y < Player.Instance.CenterOfPlayer.Y)
                {
                    Rectangle checkPosition = new Rectangle(this.monsterPosition.X, this.monsterPosition.Y + MOB_SPEED, 32, 32);

                    if (!IntersectsWithObstaclesNoPlayer(checkPosition))
                    {
                        this.changeDirection = "";
                    }
                }
                else
                {
                    Rectangle checkPosition = new Rectangle(this.monsterPosition.X, this.monsterPosition.Y - MOB_SPEED, 32, 32);

                    if (!IntersectsWithObstaclesNoPlayer(checkPosition))
                    {
                        this.changeDirection = "";
                    }
                }
            }
        }

        private bool IntersectsWithObstacles(Rectangle newPosition)
        {
            // checks if the new position is not well, market, player or another mob
            if (IntersectsWithObstaclesNoPlayer(newPosition) || Player.Instance.DestinationPosition.Intersects(newPosition))
            {
                return true;
            }

            return false;
        }

        private bool IntersectsWithObstaclesNoPlayer(Rectangle newPosition)
        {
            bool intersectsWithAnotherMob = false;

            // checks if the new position intersects with another mob
            foreach (Monster monster in Handlers.MonstersHandler.Instance.SpawnedMobs)
            {
                if (monster.ID != this.ID && monster.DestinationPosition.Intersects(newPosition))
                {
                    intersectsWithAnotherMob = true;
                    break;
                }
            }

            // checks if the new position is not well or market
            if (Well.Instance.WellPosition.Intersects(newPosition)
            || Handlers.SceneryHandler.Instance.MarketPosition.Intersects(newPosition)
            || NpcSorcerer.Instance.DrawRect.Intersects(newPosition)
            || intersectsWithAnotherMob == true)
            {
                return true;
            }

            return false;
        }

        private Rectangle Animate(GameTime gameTime)
        {
            this.elapsedTimeChangeAnim += (float)gameTime.ElapsedGameTime.TotalMilliseconds;

            if (this.elapsedTimeChangeAnim >= ANIM_DELAY)
            {
                if (this.currentFrame >= 2)
                {
                    this.currentFrame = 0;
                }
                else
                {
                    this.currentFrame++;
                }
                this.elapsedTimeChangeAnim = 0;
            }

            // if on frame 0 - top up position 0
            return new Rectangle(32 * currentFrame, 0, 32, 32);
        }
    }
}
