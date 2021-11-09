//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    static readonly Deadspell.Components.OpaqueComponent opaqueComponent = new Deadspell.Components.OpaqueComponent();

    public bool isOpaque {
        get { return HasComponent(GameComponentsLookup.Opaque); }
        set {
            if (value != isOpaque) {
                var index = GameComponentsLookup.Opaque;
                if (value) {
                    var componentPool = GetComponentPool(index);
                    var component = componentPool.Count > 0
                            ? componentPool.Pop()
                            : opaqueComponent;

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

    static Entitas.IMatcher<GameEntity> _matcherOpaque;

    public static Entitas.IMatcher<GameEntity> Opaque {
        get {
            if (_matcherOpaque == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.Opaque);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherOpaque = matcher;
            }

            return _matcherOpaque;
        }
    }
}