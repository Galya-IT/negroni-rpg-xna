namespace NegroniGame.Handlers
{
    using Microsoft.Xna.Framework;
    using NegroniGame.Monsters;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public sealed class MonstersHandler
    {
        // Singleton !
        private static MonstersHandler instance;

        private readonly Random randomGenerator = new Random();
        private bool isCountingDownToSpawn = false;
        private float elapsedTimeToNextSpawn;
        private int timeToNextSpawn = 0;
        private List<int> indexesForDeletion;

        private MonstersHandler()
        {
            this.SpawnedMobsNumber = 0;
            this.SpawnedMobs = new List<Monster>();
            this.MobsHit = new List<int>();
        }

        public static MonstersHandler Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new MonstersHandler();
                }
                return instance;
            }
        }

        public sbyte SpawnedMobsNumber { get; private set; }
        public List<Monster> SpawnedMobs { get; private set; }
        public List<int> MobsHit { get; set; }

        public void Update(GameTime gameTime)
        {
            // updates the position of all monsters
            foreach (var monster in SpawnedMobs)
            {
                monster.Update(gameTime);
            }

            // generates new mobs
            SpawnGenerator(gameTime);

            // Checks for dead monsters STARTS //
            this.indexesForDeletion = new List<int>();

            foreach (int mobIndex in this.MobsHit)
            {
                SpawnedMobs[mobIndex].HpPointsCurrent -= Player.Instance.WeaponDmg;
                if (SpawnedMobs[mobIndex].HpPointsCurrent <= 0)
                {
                    indexesForDeletion.Add(mobIndex);
                }
            }

            this.MobsHit = new List<int>();

            foreach (int mobIndex in indexesForDeletion)
            {
                Toolbar.SystemMsg.Instance.AllMessages.Add(new Dictionary<string, Color>() { { String.Format(">> {0} died.", this.SpawnedMobs[mobIndex].Name), Color.Cyan } });

                // adds drop on the place of the dead mob
                Drop currentDrop = new Drop(this.SpawnedMobs[mobIndex].DestinationPosition);
                Handlers.DropHandler.Instance.AddDrop(currentDrop);

                Toolbar.SystemMsg.Instance.AllMessages.Add(new Dictionary<string, Color>() {
                            { String.Format(">> {0} droped {1} {2}.", this.SpawnedMobs[mobIndex].Name, currentDrop.Amount, currentDrop.Name), Color.SpringGreen } });

                this.SpawnedMobsNumber--;
                this.SpawnedMobs.RemoveAt(mobIndex);
            }

            // Checks for dead monsters ENDS //
        }

        public void Draw(GameTime gameTime)
        {
            foreach (var monster in SpawnedMobs)
            {
                monster.Draw(gameTime);
            }
        }

        private void SpawnGenerator(GameTime gameTime)
        {
            this.elapsedTimeToNextSpawn += (float)gameTime.ElapsedGameTime.TotalSeconds;

            // generates time to next spawn
            if (this.SpawnedMobsNumber == 0 && this.isCountingDownToSpawn == false)
            {
                this.elapsedTimeToNextSpawn = 0;
                this.timeToNextSpawn = 1;
                this.isCountingDownToSpawn = true;
            }
            else if (this.SpawnedMobsNumber < GameSettings.MAX_SPAWNED_MOBS && this.isCountingDownToSpawn == false)
            {
                this.elapsedTimeToNextSpawn = 0;
                this.timeToNextSpawn = randomGenerator.Next(5, 10); ///// 5, 15 ////////////////////
                this.isCountingDownToSpawn = true;
            }

            // creates new mob
            if (this.elapsedTimeToNextSpawn >= this.timeToNextSpawn && this.isCountingDownToSpawn == true)
            {
                // generates position
                int positionX = randomGenerator.Next(1, GameScreen.ScreenWidth - 30);
                int positionY = randomGenerator.Next(1, GameScreen.ScreenHeight - 170);
                Rectangle monsterPosition = new Rectangle(positionX, positionY, 32, 32);

                // checks if the generated position is OK and excludes positions
                while (true)
                {
                    bool doesIntersectWithMobs = false;
                    bool doesIntersectWithDrop = false;

                    for (int index = 0; index < Handlers.MonstersHandler.Instance.SpawnedMobs.Count; index++)
                    {
                        if (monsterPosition.Intersects(this.SpawnedMobs[index].DestinationPosition))
                        {
                            doesIntersectWithMobs = true;
                            break;
                        }
                    }

                    for (int index = 0; index < Handlers.DropHandler.Instance.DropList.Count; index++)
                    {
                        if (Handlers.DropHandler.Instance.DropList[index] != null && monsterPosition.Intersects(Handlers.DropHandler.Instance.DropList[index].DropPosition))
                        {
                            doesIntersectWithDrop = true;
                            break;
                        }
                    }

                    if (!monsterPosition.Intersects(Player.Instance.DestinationPosition)
                    && !monsterPosition.Intersects(Well.Instance.WellPosition)
                    && !monsterPosition.Intersects(SceneryHandler.Instance.MarketPosition)
                    && !monsterPosition.Intersects(NpcSorcerer.Instance.DrawRect)
                    && !doesIntersectWithMobs
                    && !doesIntersectWithDrop)
                    {
                        break;
                    }

                    positionX = randomGenerator.Next(1, GameScreen.ScreenWidth - 30);
                    positionY = randomGenerator.Next(1, GameScreen.ScreenHeight - 170);
                    monsterPosition = new Rectangle(positionX, positionY, 32, 32);
                }

                // generates which of the mobs will spawn
                sbyte randomMobType = (sbyte)randomGenerator.Next(0, 6);

                switch (randomMobType)
                {
                    case 0: this.SpawnedMobs.Add(new RedDragon(this.SpawnedMobsNumber, monsterPosition)); break;
                    case 1: this.SpawnedMobs.Add(new Viking(this.SpawnedMobsNumber, monsterPosition)); break;
                    case 2: this.SpawnedMobs.Add(new GreenOne(this.SpawnedMobsNumber, monsterPosition)); break;
                    case 3: this.SpawnedMobs.Add(new Bug(this.SpawnedMobsNumber, monsterPosition)); break;
                    case 4: this.SpawnedMobs.Add(new Genie(this.SpawnedMobsNumber, monsterPosition)); break;
                    case 5: this.SpawnedMobs.Add(new PurpleBat(this.SpawnedMobsNumber, monsterPosition)); break;
                }

                // returns values of variables
                this.SpawnedMobsNumber++;
                this.isCountingDownToSpawn = false;
            }
        }
    }
}
