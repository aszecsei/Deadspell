//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    static readonly Deadspell.Components.CurrentTurnComponent currentTurnComponent = new Deadspell.Components.CurrentTurnComponent();

    public bool isCurrentTurn {
        get { return HasComponent(GameComponentsLookup.CurrentTurn); }
        set {
            if (value != isCurrentTurn) {
                var index = GameComponentsLookup.CurrentTurn;
                if (value) {
                    var componentPool = GetComponentPool(index);
                    var component = componentPool.Count > 0
                            ? componentPool.Pop()
                            : currentTurnComponent;

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

    static Entitas.IMatcher<GameEntity> _matcherCurrentTurn;

    public static Entitas.IMatcher<GameEntity> CurrentTurn {
        get {
            if (_matcherCurrentTurn == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.CurrentTurn);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherCurrentTurn = matcher;
            }

            return _matcherCurrentTurn;
        }
    }
}
