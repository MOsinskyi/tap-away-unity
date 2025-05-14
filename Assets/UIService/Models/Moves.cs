using System;
using LevelService.Interfaces;
using LevelService.Scripts;

namespace UIService.Models
{
    public sealed class Moves
    {
        public Moves(IFixedMoves level)
        {
            if (level.MovesCount > 0)
                Count = level.MovesCount;
            else
                throw new Exception($"Moves on level {(level as Level)?.LevelName} should contain at least one move");
        }

        public int Count { get; private set; }

        public event Action OnMovesEmpty;
        public event Action OnMoveSpent;
        public event Action OnMovesAdded;

        public void AddMoves(int amount)
        {
            if (amount > 0)
            {
                Count += amount;
                OnMovesAdded?.Invoke();
            }
        }

        public void SpentMove()
        {
            if (Count > 0)
            {
                Count--;
                OnMoveSpent?.Invoke();
            }

            if (Count == 0) OnMovesEmpty?.Invoke();
        }
    }
}