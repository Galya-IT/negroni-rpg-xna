namespace NegroniGame.Handlers
{
    using Microsoft.Xna.Framework;
    using NegroniGame.Interfaces;
    using NegroniGame.SystemFunctions;
    using System.Collections.Generic;

    public static class NpcHelperHandler
    {
        private static bool isLocked;
        private static List<SpriteObject> spriteObjList;
        private static List<SpriteObject> spriteToAddList;
        private static List<SpriteObject> spriteToDeleteList;

        static NpcHelperHandler()
        {
            spriteObjList = new List<SpriteObject>();
            spriteToAddList = new List<SpriteObject>();
            spriteToDeleteList = new List<SpriteObject>();
            isLocked = false;
        }

        public static void DoReaction<T>(IReact obj) where T : IReact
        {
            Rectangle reactRect = obj.ReactRect;
            isLocked = true;
            foreach (var item in spriteObjList)
            {
                if (item is T && item != obj && !item.IsDeleted)
                {
                    if ((item as IReact).ReactRect.Intersects(reactRect))
                    {
                        (item as IReact).DoAction<T>(obj);
                    }
                }
            }

            isLocked = false;
        }

        public static bool DoesIntersect(Rectangle positionRect, SpriteObject obj)
        {
            isLocked = true;
            foreach (var item in spriteObjList)
            {
                if (item != obj && item.DrawRect.Intersects(new Rectangle(positionRect.X - 5, positionRect.Y - 5, positionRect.Width + 10, positionRect.Height + 10)))
                {
                    isLocked = false;
                    return true;
                }
            }

            isLocked = false;
            return false;
        }

        // Call Update method for not deleted game objects
        public static void Update(GameTime gameTime)
        {
            foreach (var obj in NpcHelperHandler.spriteObjList)
            {
                if (obj.IsDeleted)
                {
                    spriteToDeleteList.Add(obj);
                    continue;
                }

                obj.Update(gameTime);
            }
        }

        // Call Draw method for visible game objects
        public static void Draw()
        {
            // Add objects
            if (!isLocked)
            {
                foreach (var obj in spriteToAddList)
                {
                    spriteObjList.Add(obj);
                }

                spriteToAddList = new List<SpriteObject>();
            }

            isLocked = true;
            foreach (var obj in spriteObjList)
            {
                obj.Draw();
            }

            // Delete Objects
            isLocked = false;
            if (!isLocked)
            {
                foreach (var obj in spriteToDeleteList)
                {
                    spriteObjList.RemoveAll(x => x == obj);
                }

                spriteToDeleteList = new List<SpriteObject>();
            }
        }

        public static void AddSprite(SpriteObject obj)
        {
            spriteToAddList.Add(obj);
        }

        public static void RemoveSprite(SpriteObject obj)
        {
            spriteToDeleteList.Add(obj);
        }
    }
}