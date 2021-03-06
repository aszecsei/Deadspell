//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public TooltipRemovedListenerComponent tooltipRemovedListener { get { return (TooltipRemovedListenerComponent)GetComponent(GameComponentsLookup.TooltipRemovedListener); } }
    public bool hasTooltipRemovedListener { get { return HasComponent(GameComponentsLookup.TooltipRemovedListener); } }

    public void AddTooltipRemovedListener(System.Collections.Generic.List<ITooltipRemovedListener> newValue) {
        var index = GameComponentsLookup.TooltipRemovedListener;
        var component = (TooltipRemovedListenerComponent)CreateComponent(index, typeof(TooltipRemovedListenerComponent));
        component.value = newValue;
        AddComponent(index, component);
    }

    public void ReplaceTooltipRemovedListener(System.Collections.Generic.List<ITooltipRemovedListener> newValue) {
        var index = GameComponentsLookup.TooltipRemovedListener;
        var component = (TooltipRemovedListenerComponent)CreateComponent(index, typeof(TooltipRemovedListenerComponent));
        component.value = newValue;
        ReplaceComponent(index, component);
    }

    public void RemoveTooltipRemovedListener() {
        RemoveComponent(GameComponentsLookup.TooltipRemovedListener);
    }
}

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentMatcherApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public sealed partial class GameMatcher {

    static Entitas.IMatcher<GameEntity> _matcherTooltipRemovedListener;

    public static Entitas.IMatcher<GameEntity> TooltipRemovedListener {
        get {
            if (_matcherTooltipRemovedListener == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.TooltipRemovedListener);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherTooltipRemovedListener = matcher;
            }

            return _matcherTooltipRemovedListener;
        }
    }
}

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.EventEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public void AddTooltipRemovedListener(ITooltipRemovedListener value) {
        var listeners = hasTooltipRemovedListener
            ? tooltipRemovedListener.value
            : new System.Collections.Generic.List<ITooltipRemovedListener>();
        listeners.Add(value);
        ReplaceTooltipRemovedListener(listeners);
    }

    public void RemoveTooltipRemovedListener(ITooltipRemovedListener value, bool removeComponentWhenEmpty = true) {
        var listeners = tooltipRemovedListener.value;
        listeners.Remove(value);
        if (removeComponentWhenEmpty && listeners.Count == 0) {
            RemoveTooltipRemovedListener();
        } else {
            ReplaceTooltipRemovedListener(listeners);
        }
    }
}
