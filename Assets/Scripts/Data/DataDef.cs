using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WhoWhenLone
{
    public class DataDef
    {
        public static DataDef instance;

        public static DataDef GetInstance()
        {
            if (instance == null)
            {
                instance = new DataDef();
            }

            return instance;
        }

        public enum FruitType
        {
            None,
            Fruit_1,
            Fruit_2,
            Fruit_3,
            Fruit_4,
            Fruit_5,
            Fruit_6,
            Fruit_7,
            Fruit_8,
            Fruit_9,
            Fruit_10,
            Max
        };

        public enum CreateType
        {
            Create,
            Compress
        };

        public enum GameState
        {
            Ready,
            StandBy,
            InProgress,
            GameOver,
            CaculateScore,
        }

        public enum FruitState
        {
            Ready,
            StandBy,
            Drop,
            Collision,
        }

        public static int MaxFruitType = (int)FruitType.Max - 1;

        public static int MaxCreateFruitType = (int) FruitType.Fruit_5;

        public static Dictionary<FruitType, string> FruitPrefabPath = new Dictionary<FruitType, string>()
        {
            [FruitType.Fruit_1] = "Prefab/fruit_1",
            [FruitType.Fruit_2] = "Prefab/fruit_2",
            [FruitType.Fruit_3] = "Prefab/fruit_3",
            [FruitType.Fruit_4] = "Prefab/fruit_4",
            [FruitType.Fruit_5] = "Prefab/fruit_5",
            [FruitType.Fruit_6] = "Prefab/fruit_6",
            [FruitType.Fruit_7] = "Prefab/fruit_7",
            [FruitType.Fruit_8] = "Prefab/fruit_8",
            [FruitType.Fruit_9] = "Prefab/fruit_9",
            [FruitType.Fruit_10] = "Prefab/fruit_10",
        };


    }
}

