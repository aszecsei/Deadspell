using Deadspell.Events;
using Entitas;
using UnityEngine;

namespace Deadspell.Systems
{
    public class EmitInputSystem : IInitializeSystem, IExecuteSystem
    {
        private readonly InputContext _context;
        private InputEntity _leftMouseEntity;
        private InputEntity _rightMouseEntity;
        private InputEntity _mousePosEntity;

        public EmitInputSystem(Contexts contexts)
        {
            _context = contexts.input;
        }

        public void Initialize()
        {
            // Initialize the unique entities that will hold the mouse button data
            _context.isLeftMouse = true;
            _leftMouseEntity = _context.leftMouseEntity;

            _context.isRightMouse = true;
            _rightMouseEntity = _context.rightMouseEntity;

            _context.CreateEntity().AddMousePosition(Vector2.zero);
            _mousePosEntity = _context.mousePositionEntity;
        }

        public void Execute()
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            _mousePosEntity.ReplaceMousePosition(mousePosition);
            
            if (Input.GetMouseButtonDown(0))
            {
                _leftMouseEntity.AddMouseDown(mousePosition);
            }
            
            if (Input.GetMouseButtonUp(0))
            {
                _leftMouseEntity.AddMouseUp(mousePosition);
            }
            
            if (Input.GetMouseButtonDown(1))
            {
                _rightMouseEntity.AddMouseDown(mousePosition);
            }
            if (Input.GetMouseButtonUp(1))
            {
                _rightMouseEntity.AddMouseUp(mousePosition);
            }
        }
    }
}