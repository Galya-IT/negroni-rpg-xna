namespace NegroniGame
{
    using System;
    using System.Linq;

    public static class GameSettings
    {
        public const float SECONDS_TO_DISAPPEARING = 20;        //from class Drop
        public const int PERCENT_TO_DROP_POTION = 10;           //from class Drop
        public const int REUSE_TIME = 10;                       //from class ElixirsHandler
        public const int MAX_SPAWNED_MOBS = 4;                  //from class MonstersHandler 4
        public const float SHOT_REUSE_TIME = 1000f;             //from class ShotsHandler
        public const int RECOVERY_AMOUNT = 30;                  //from struct ElixirsHP
        public const int MOBS_ATTACK = 10;                      //from class Monster
        public const float TIME_TO_CHANGE_DIRECTION = 5;        //from class Monster
        public const int MOVE_MAX_LENGTH = 80;                  //from class Monster
        public const int AGGRO_RANGE = 80;                      //from class Monster 80
        public const int ATTACK_INTERVAL = 2;                   //from class Monster
        public const int HP_POINTS_INITIAL = 200;               //from class Player
        public const int RANGE = 150;                           //from class Shot
        public const float WELL_REUSE_TIME = 15;                //from class Well
        public const int ACTIVE_TIME = 30000;                   //from class NpcHelper
        public const int HELPER_RENTING_PRICE = 250;            //from class NpcSorcerer
        public const float TIME_TO_RENT_AGAIN = 40;             //from class NpcSorcerer
    }
}
