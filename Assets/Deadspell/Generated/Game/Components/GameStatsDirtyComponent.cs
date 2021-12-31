//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    static readonly Deadspell.Components.StatsDirtyComponent statsDirtyComponent = new Deadspell.Components.StatsDirtyComponent();

    public bool isStatsDirty {
        get { return HasComponent(GameComponentsLookup.StatsDirty); }
        set {
            if (value != isStatsDirty) {
                var index = GameComponentsLookup.StatsDirty;
                if (value) {
                    var componentPool = GetComponentPool(index);
                    var component = componentPool.Count > 0
                            ? componentPool.Pop()
                            : statsDirtyComponent;

                    AddComponent(index, component);
                } else {
                    RemoveComponent(index);
                }
            }
        }
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

    static Entitas.IMatcher<GameEntity> _matcherStatsDirty;

    public static Entitas.IMatcher<GameEntity> StatsDirty {
        get {
            if (_matcherStatsDirty == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.StatsDirty);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherStatsDirty = matcher;
            }

            return _matcherStatsDirty;
        }
    }
}
