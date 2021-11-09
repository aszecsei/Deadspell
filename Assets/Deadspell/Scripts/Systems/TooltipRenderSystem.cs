using System.Collections.Generic;
using System.Text;
using Deadspell.Core;
using Entitas;
using UnityEngine;

namespace Deadspell.Systems
{
    public class TooltipRenderSystem : IExecuteSystem
    {
        private readonly GameContext _context;
        private readonly InputContext _input;
        private readonly MetaContext _meta;
        private IGroup<GameEntity> _tooltips;

        private struct TooltipData
        {
            public string Header;
            public string Content;
        }
        
        public TooltipRenderSystem(Contexts contexts)
        {
            _context = contexts.game;
            _input = contexts.input;
            _meta = contexts.meta;
            _tooltips = _context.GetGroup(GameMatcher.Tooltip);
        }
        
        public void Execute()
        {
            var mousePos = _input.mousePosition.Position;
            var mousePosGrid = new Vector2Int(Mathf.FloorToInt(mousePos.x), Mathf.FloorToInt(mousePos.y));
            var mapData = _context.gameMap.MapData;

            foreach (var e in _tooltips.GetEntities())
            {
                if (e.hasTooltip && e.tooltip.Position != mousePosGrid)
                {
                    e.RemoveTooltip();
                    _meta.tooltipService.Instance.DestroyTooltip(e);
                }
            }

            if (!mapData.IsInBounds(mousePosGrid.x, mousePosGrid.y))
            {
                return;
            }
            
            // TODO: Check if mouse is over UI element

            if (!mapData[mousePosGrid].Visible)
            {
                return;
            }

            foreach (var tc in mapData.Spatial.TileContent(mousePosGrid.x, mousePosGrid.y))
            {
                if (!tc.hasName)
                {
                    if (tc.hasTooltip)
                    {
                        tc.RemoveTooltip();
                        _meta.tooltipService.Instance.DestroyTooltip(tc);
                    }
                    
                    continue;
                }
                
                if (tc.isHidden)
                {
                    if (tc.hasTooltip)
                    {
                        tc.RemoveTooltip();
                        _meta.tooltipService.Instance.DestroyTooltip(tc);
                    }

                    continue;
                }

                // TODO: Get item display name
                TooltipData itemData = new TooltipData
                {
                    Header = $"<b>{tc.name.Name}</b>",
                    Content = "",
                };

                StringBuilder contentBuilder = new StringBuilder();

                // Comment on stats
                if (tc.hasStats)
                {
                    itemData.Header += $" <color=grey>Level: {tc.stats.Level}</color>";
                }

                if (tc.isPlayer)
                {
                    contentBuilder.Append("It's you!\n");
                }
                
                // Comment on attributes
                if (tc.hasAttributes)
                {
                    bool commented = false;
                    void CommentOn(Attribute attr, string bad, string good)
                    {
                        if (attr.Bonus < 0)
                        {
                            contentBuilder.Append(bad);
                        }
                        else if (attr.Bonus > 0)
                        {
                            contentBuilder.Append(good);
                        }

                        if (attr.Bonus != 0)
                        {
                            contentBuilder.Append(". ");
                            commented = true;
                        }
                    }

                    CommentOn(tc.attributes.Attributes.Might, "Weak", "Strong");
                    CommentOn(tc.attributes.Attributes.Agility, "Clumsy", "Agile");
                    CommentOn(tc.attributes.Attributes.Vitality, "Unhealthy", "Healthy");

                    CommentOn(tc.attributes.Attributes.Intelligence, "Stupid", "Smart");
                    CommentOn(tc.attributes.Attributes.Wits, "Dull", "Clever");
                    CommentOn(tc.attributes.Attributes.Resolve, "Uncertain", "Certain");
                    
                    CommentOn(tc.attributes.Attributes.Faith, "Wicked", "Pious");
                    CommentOn(tc.attributes.Attributes.Insight, "Ignorant", "Insightful");
                    CommentOn(tc.attributes.Attributes.Conviction, "Disloyal", "Loyal");
                    
                    CommentOn(tc.attributes.Attributes.Presence, "Ugly", "Attractive");
                    CommentOn(tc.attributes.Attributes.Manipulation, "Inept", "Sly");
                    CommentOn(tc.attributes.Attributes.Composure, "Timid", "Confident");

                    if (!commented)
                    {
                        contentBuilder.Append("Quite average.");
                    }
                }
                
                itemData.Content = contentBuilder.ToString();

                if (!tc.hasTooltipListener)
                {
                    _meta.tooltipService.Instance.RegisterListeners(tc);
                }
                if (!tc.hasTooltip)
                {
                    _meta.tooltipService.Instance.CreateTooltip(tc, mousePosGrid, itemData.Header, itemData.Content);
                }
                
                tc.ReplaceTooltip(mousePosGrid, itemData.Header, itemData.Content);
            }
        }
    }
}