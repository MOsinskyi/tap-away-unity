using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GameService.Scripts
{
  public class GameRules
  {
    private readonly List<Cube.Scripts.Cube> _cubes;
    private readonly List<Vector3> _cubePositions;

    public GameRules(List<Cube.Scripts.Cube> cubes)
    {
      _cubes = cubes;
      _cubePositions = new List<Vector3>();
    }

    public void CheckCubeIntegrity()
    {
      foreach (var cube in _cubes)
      {
        if (cube.GetComponentsInChildren<MeshRenderer>().Length > 1)
        {
          Debug.LogWarning($"{cube.gameObject.name} cannot have more than one mesh renderer");
        }

        if (_cubePositions.Count > 0)
        {
          foreach (var position in _cubePositions.Where(position => cube.transform.localPosition == position))
          {
            Debug.LogWarning($"Two cubes {cube.gameObject.name} cannot have the same position: {position}");
          }
        }

        _cubePositions.Add(cube.transform.localPosition);
      }

      CheckCubesWatchingEachOther();
    }

    private void CheckCubesWatchingEachOther()
    {
      for (var i = 0; i < _cubes.Count; i++)
      {
        for (var j = i + 1; j < _cubes.Count; j++)
        {
          if (AreCubesLookingAtEachOther(_cubes[i], _cubes[j]))
          {
            Debug.LogWarning(
              $"Cubes {_cubes[i].gameObject.name} and {_cubes[j].gameObject.name} are looking at each other");
          }
        }
      }
    }

    private bool AreCubesLookingAtEachOther(Cube.Scripts.Cube cube1, Cube.Scripts.Cube cube2)
    {
      // Get the forward directions of both cubes
      var forward1 = cube1.transform.forward;
      var forward2 = cube2.transform.forward;

      // Get the direction vector from cube1 to cube2
      var direction = cube2.transform.position - cube1.transform.position;
      direction.Normalize();

      // Get the direction vector from cube2 to cube1
      var oppositeDirection = -direction;

      // Check if cube1's forward is pointing toward cube2
      // and cube2's forward is pointing toward cube1
      var dotProduct1 = Vector3.Dot(forward1, direction);
      var dotProduct2 = Vector3.Dot(forward2, oppositeDirection);

      // If both dot products are close to 1, the cubes are looking at each other
      const float lookingThreshold = 1f; // Adjust this value based on your needs
      return dotProduct1 >= lookingThreshold && dotProduct2 >= lookingThreshold;
    }
  }
}