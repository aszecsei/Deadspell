//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.EventSystemGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public sealed class TooltipRemovedEventSystem : Entitas.ReactiveSystem<GameEntity> {

    readonly System.Collections.Generic.List<ITooltipRemovedListener> _listenerBuffer;

    public TooltipRemovedEventSystem(Contexts contexts) : base(contexts.game) {
        _listenerBuffer = new System.Collections.Generic.List<ITooltipRemovedListener>();
    }

    protected override Entitas.ICollector<GameEntity> GetTrigger(Entitas.IContext<GameEntity> context) {
        return Entitas.CollectorContextExtension.CreateCollector(
            context, Entitas.TriggerOnEventMatcherExtension.Removed(GameMatcher.Tooltip)
        );
    }

    protected override bool Filter(GameEntity entity) {
        return !entity.hasTooltip && entity.hasTooltipRemovedListener;
    }

    protected override void Execute(System.Collections.Generic.List<GameEntity> entities) {
        foreach (var e in entities) {
            
            _listenerBuffer.Clear();
            _listenerBuffer.AddRange(e.tooltipRemovedListener.value);
            foreach (var listener in _listenerBuffer) {
                listener.OnTooltipRemoved(e);
            }
        }
    }
}
