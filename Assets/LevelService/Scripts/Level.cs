using System.Collections.Generic;
using System.Linq;
using GameService.Scripts;
using UnityEngine;

namespace LevelService.Scripts
{
    public abstract class Level : MonoBehaviour
    {
        [field: SerializeField] public string LevelName { get; private set; }
        public List<Cube.Scripts.Cube> Cubes {get; private set;}

        public void Construct()
        {
            Cubes = GetComponentsInChildren<Cube.Scripts.Cube>().ToList();
            gameObject.name = LevelName;

            if (Application.isEditor)
            {
                CheckRules();
            }
        }

        private void CheckRules()
        {
            var gameRules = new GameRules(Cubes);
            gameRules.CheckCubeIntegrity();
        }
    }
}