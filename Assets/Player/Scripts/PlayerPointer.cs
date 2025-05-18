using System;
using Common.InputService.Interfaces;
using Common.Interfaces;
using UIService.Models;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Player.Scripts
{
    [RequireComponent(typeof(Camera))]
    public sealed class PlayerPointer : MonoBehaviour, IObserver
    {
        private Camera _camera;
        
        private Moves _moves;
        private IInputHandler _inputHandler;

        private bool _canSelect = true;

        [Inject]
        private void Construct(IInputHandler inputHandler)
        {
            _camera = GetComponent<Camera>();
            _inputHandler = inputHandler;
        }

        public void SetMoves(Moves moves)
        {
            _moves = moves;
            Subscribe();
        }

        private void Update()
        {
            if (_inputHandler.IsPressed)
            {
                var ray = _inputHandler.GetClickRay(_camera);

                if (Physics.Raycast(ray, out var hit))
                    if (hit.collider.gameObject.TryGetComponent(out Cube.Scripts.Cube cube) && _canSelect)
                    {
                        if (!cube.IsSelected)
                        {
                            cube.Interact();
                            OnCubeSelected?.Invoke(cube);
                        }
                    }
            }
        }

        private void OnDisable()
        {
            if (_moves != null)
            {
                Unsubscribe();
            }
        }

        public void Subscribe()
        {
            _moves.OnMovesEmpty += OnMovesEmpty;
            _moves.OnMovesAdded += OnMovesAdded;
        }

        public void Unsubscribe()
        {
            _moves.OnMovesEmpty += OnMovesEmpty;
            _moves.OnMovesAdded -= OnMovesAdded;
        }

        public event Action<Cube.Scripts.Cube> OnCubeSelected;
        
        private void OnMovesAdded()
        {
            _canSelect = true;
        }

        private void OnMovesEmpty()
        {
            _canSelect = false;
        }
    }
}