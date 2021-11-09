using Deadspell.Core;
using Deadspell.Systems;

namespace Deadspell.Features
{
    public class RenderingSystems : Feature
    {
        public RenderingSystems(Contexts contexts) : base("Rendering Systems")
        {
            Add(new TileRenderSystem(contexts, TilemapManager.Instance.Tilemap));
            Add(new PlayerInfoRenderSystem(contexts));
            Add(new TooltipRenderSystem(contexts));
        }
    }
}