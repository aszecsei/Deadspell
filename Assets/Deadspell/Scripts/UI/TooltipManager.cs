using System;
using System.Collections.Generic;
using Deadspell.Services;
using Entitas;
using UnityEngine;

namespace Deadspell.UI
{
    public class TooltipManager : MonoBehaviour, ITooltipService, ITooltipListener, ITooltipRemovedListener
    {
        private Dictionary<GameEntity, GameObject> _tooltipObjects = new Dictionary<GameEntity, GameObject>();
        public Canvas TooltipCanvas;
        public RectTransform TooltipContainer;
        public GameObject TooltipPrefab;

        private Queue<GameObject> _unusedTooltips = new Queue<GameObject>();

        public void RegisterListeners(IEntity entity)
        {
            var ge = (GameEntity)entity;
            ge.AddTooltipListener(this);
            ge.AddTooltipRemovedListener(this);
        }
        
        private Vector3 WorldToUISpace(Canvas parentCanvas, Vector3 worldPos)
        {
            // Convert the world for screen point so that it can be used with ScreenPointToLocalPointInRectangle
            Vector3 screenPos = Camera.main.WorldToScreenPoint(worldPos);

            // Convert the screen point to UI rectangle local point
            RectTransformUtility.ScreenPointToLocalPointInRectangle(parentCanvas.transform as RectTransform, screenPos,
                parentCanvas.worldCamera, out var movePos);
            // Convert the local point to world point
            return parentCanvas.transform.TransformPoint(movePos);
        }

        public void CreateTooltip(GameEntity entity, Vector2Int position, string header, string content)
        {
            GameObject tooltipObject;
            if (_unusedTooltips.Count == 0)
            {
                tooltipObject = Instantiate(TooltipPrefab, TooltipPrefab.transform.parent);
            }
            else
            {
                tooltipObject = _unusedTooltips.Dequeue();
            }
            tooltipObject.transform.SetAsLastSibling();

            _tooltipObjects[entity] = tooltipObject;
            Tooltip tooltip = tooltipObject.GetComponent<Tooltip>();
            TooltipContainer.position = WorldToUISpace(TooltipCanvas, new Vector3(position.x, position.y));
            tooltip.Header = header;
            tooltip.Content = content;
            tooltip.UpdateText();
            tooltipObject.name = $"Tooltip ({header})";
            tooltipObject.SetActive(true);
        }

        public void OnTooltip(GameEntity entity, Vector2Int position, string header, string content)
        {
            GameObject tooltipObject = _tooltipObjects[entity];
            Tooltip tooltip = tooltipObject.GetComponent<Tooltip>();
            TooltipContainer.position = WorldToUISpace(TooltipCanvas, new Vector3(position.x, position.y));
            tooltip.Header = header;
            tooltip.Content = content;
            tooltip.UpdateText();
            tooltipObject.name = $"Tooltip ({header})";
        }

        public void DestroyTooltip(GameEntity entity)
        {
            GameObject tooltipObject = _tooltipObjects[entity];
            tooltipObject.SetActive(false);
            _unusedTooltips.Enqueue(tooltipObject);
            _tooltipObjects.Remove(entity);
        }

        public void OnTooltipRemoved(GameEntity entity)
        {
            DestroyTooltip(entity);
        }
    }
}
