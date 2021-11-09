//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentContextApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class MetaContext {

    public MetaEntity tooltipServiceEntity { get { return GetGroup(MetaMatcher.TooltipService).GetSingleEntity(); } }
    public Deadspell.Components.Services.TooltipServiceComponent tooltipService { get { return tooltipServiceEntity.tooltipService; } }
    public bool hasTooltipService { get { return tooltipServiceEntity != null; } }

    public MetaEntity SetTooltipService(Deadspell.Services.ITooltipService newInstance) {
        if (hasTooltipService) {
            throw new Entitas.EntitasException("Could not set TooltipService!\n" + this + " already has an entity with Deadspell.Components.Services.TooltipServiceComponent!",
                "You should check if the context already has a tooltipServiceEntity before setting it or use context.ReplaceTooltipService().");
        }
        var entity = CreateEntity();
        entity.AddTooltipService(newInstance);
        return entity;
    }

    public void ReplaceTooltipService(Deadspell.Services.ITooltipService newInstance) {
        var entity = tooltipServiceEntity;
        if (entity == null) {
            entity = SetTooltipService(newInstance);
        } else {
            entity.ReplaceTooltipService(newInstance);
        }
    }

    public void RemoveTooltipService() {
        tooltipServiceEntity.Destroy();
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
public partial class MetaEntity {

    public Deadspell.Components.Services.TooltipServiceComponent tooltipService { get { return (Deadspell.Components.Services.TooltipServiceComponent)GetComponent(MetaComponentsLookup.TooltipService); } }
    public bool hasTooltipService { get { return HasComponent(MetaComponentsLookup.TooltipService); } }

    public void AddTooltipService(Deadspell.Services.ITooltipService newInstance) {
        var index = MetaComponentsLookup.TooltipService;
        var component = (Deadspell.Components.Services.TooltipServiceComponent)CreateComponent(index, typeof(Deadspell.Components.Services.TooltipServiceComponent));
        component.Instance = newInstance;
        AddComponent(index, component);
    }

    public void ReplaceTooltipService(Deadspell.Services.ITooltipService newInstance) {
        var index = MetaComponentsLookup.TooltipService;
        var component = (Deadspell.Components.Services.TooltipServiceComponent)CreateComponent(index, typeof(Deadspell.Components.Services.TooltipServiceComponent));
        component.Instance = newInstance;
        ReplaceComponent(index, component);
    }

    public void RemoveTooltipService() {
        RemoveComponent(MetaComponentsLookup.TooltipService);
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
public sealed partial class MetaMatcher {

    static Entitas.IMatcher<MetaEntity> _matcherTooltipService;

    public static Entitas.IMatcher<MetaEntity> TooltipService {
        get {
            if (_matcherTooltipService == null) {
                var matcher = (Entitas.Matcher<MetaEntity>)Entitas.Matcher<MetaEntity>.AllOf(MetaComponentsLookup.TooltipService);
                matcher.componentNames = MetaComponentsLookup.componentNames;
                _matcherTooltipService = matcher;
            }

            return _matcherTooltipService;
        }
    }
}