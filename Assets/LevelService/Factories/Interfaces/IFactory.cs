using LevelService.Scripts;

namespace LevelService.Factories.Interfaces
{
    public interface IFactory
    {
        public Level GetLevel(int index);
    }
}