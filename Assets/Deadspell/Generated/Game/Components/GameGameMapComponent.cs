//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentContextApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameContext {

    public GameEntity gameMapEntity { get { return GetGroup(GameMatcher.GameMap).GetSingleEntity(); } }
    public Deadspell.Components.GameMapComponent gameMap { get { return gameMapEntity.gameMap; } }
    public bool hasGameMap { get { return gameMapEntity != null; } }

    public GameEntity SetGameMap(Deadspell.Map.MapData newMapData, UnityEngine.Vector2Int newPlayerStart) {
        if (hasGameMap) {
            throw new Entitas.EntitasException("Could not set GameMap!\n" + this + " already has an entity with Deadspell.Components.GameMapComponent!",
                "You should check if the context already has a gameMapEntity before setting it or use context.ReplaceGameMap().");
        }
        var entity = CreateEntity();
        entity.AddGameMap(newMapData, newPlayerStart);
        return entity;
    }

    public void ReplaceGameMap(Deadspell.Map.MapData newMapData, UnityEngine.Vector2Int newPlayerStart) {
        var entity = gameMapEntity;
        if (entity == null) {
            entity = SetGameMap(newMapData, newPlayerStart);
        } else {
            entity.ReplaceGameMap(newMapData, newPlayerStart);
        }
    }

    public void RemoveGameMap() {
        gameMapEntity.Destroy();
    }
}

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public Deadspell.Components.GameMapComponent gameMap { get { return (Deadspell.Components.GameMapComponent)GetComponent(GameComponentsLookup.GameMap); } }
    public bool hasGameMap { get { return HasComponent(GameComponentsLookup.GameMap); } }

    public void AddGameMap(Deadspell.Map.MapData newMapData, UnityEngine.Vector2Int newPlayerStart) {
        var index = GameComponentsLookup.GameMap;
        var component = (Deadspell.Components.GameMapComponent)CreateComponent(index, typeof(Deadspell.Components.GameMapComponent));
        component.MapData = newMapData;
        component.PlayerStart = newPlayerStart;
        AddComponent(index, component);
    }

    public void ReplaceGameMap(Deadspell.Map.MapData newMapData, UnityEngine.Vector2Int newPlayerStart) {
        var index = GameComponentsLookup.GameMap;
        var component = (Deadspell.Components.GameMapComponent)CreateComponent(index, typeof(Deadspell.Components.GameMapComponent));
        component.MapData = newMapData;
        component.PlayerStart = newPlayerStart;
        ReplaceComponent(index, component);
    }

    public void RemoveGameMap() {
        RemoveComponent(GameComponentsLookup.GameMap);
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

    static Entitas.IMatcher<GameEntity> _matcherGameMap;

    public static Entitas.IMatcher<GameEntity> GameMap {
        get {
            if (_matcherGameMap == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.GameMap);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherGameMap = matcher;
            }

            return _matcherGameMap;
        }
    }
}
