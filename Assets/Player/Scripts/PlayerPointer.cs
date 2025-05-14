using System;
using Common.InputService.Interfaces;
using Common.Interfaces;
using UIService.Models;
using UnityEngine;

namespace Player.Scripts
{
    [RequireComponent(typeof(Camera))]
    public sealed class PlayerPointer : MonoBehaviour, IObserver
    {
        private Camera _camera;
        
        private Moves _moves;
        private IInputHandler _inputHandler;

        private bool _canSelect = true;
        
        public void Initialize(IInputHandler inputHandler, Moves moves = null)
        {
            _moves = moves;
            _inputHandler = inputHandler;
            
            if (_moves != null)
            {
                Subscribe();
            }
                
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
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

        private void OnValidate()
        {
            _camera = GetComponent<Camera>();
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