//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.EventSystemGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public sealed class TooltipEventSystem : Entitas.ReactiveSystem<GameEntity> {

    readonly System.Collections.Generic.List<ITooltipListener> _listenerBuffer;

    public TooltipEventSystem(Contexts contexts) : base(contexts.game) {
        _listenerBuffer = new System.Collections.Generic.List<ITooltipListener>();
    }

    protected override Entitas.ICollector<GameEntity> GetTrigger(Entitas.IContext<GameEntity> context) {
        return Entitas.CollectorContextExtension.CreateCollector(
            context, Entitas.TriggerOnEventMatcherExtension.Added(GameMatcher.Tooltip)
        );
    }

    protected override bool Filter(GameEntity entity) {
        return entity.hasTooltip && entity.hasTooltipListener;
    }

    protected override void Execute(System.Collections.Generic.List<GameEntity> entities) {
        foreach (var e in entities) {
            var component = e.tooltip;
            _listenerBuffer.Clear();
            _listenerBuffer.AddRange(e.tooltipListener.value);
            foreach (var listener in _listenerBuffer) {
                listener.OnTooltip(e, component.Position, component.Header, component.Content);
            }
        }
    }
}
