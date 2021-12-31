using System;
using System.Collections.Generic;
using Deadspell.Data;
using Deadspell.Data.Blueprints;
using Deadspell.Editor;
using Deadspell.Managers;
using Deadspell.ValueReferences;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities.Editor;
using UnityEditor;
using UnityEngine;

namespace Deadspell.Editor
{
    public class GameDataManager : OdinMenuEditorWindow
    {
        [MenuItem("Tools/Game Data Manager")]
        public static void OpenWindow()
        {
            GetWindow<GameDataManager>().Show();
        }

        public enum ManagerState
        {
            [LabelText("Universe")]
            Universe,
            [LabelText("Spawn Tables")]
            SpawnTables,
            [LabelText("Factions")]
            Factions,
            [LabelText("Languages")]
            Languages,
            [LabelText("NPC Templates")]
            NpcTemplates,
            [LabelText("NPCs")]
            NPCs,
            [LabelText("Generic NPCs")]
            GenericNPCs,
            [LabelText("Loot Tables")]
            LootTables,
            [LabelText("Items")]
            Items,
            [LabelText("Spells")]
            Spells,
            [LabelText("Props")]
            Props,
            [LabelText("Colors")]
            Colors,
            [LabelText("SFX")]
            SFX,
        }

        [OnValueChanged(nameof(StateChange))]
        [LabelText("Manager View")]
        [LabelWidth(100f)]
        [EnumToggleButtons]
        [ShowInInspector]
        private ManagerState _managerState;

        private int _enumIndex = 0;
        private bool _treeRebuild = false;

        private DrawSpawnTables _drawSpawnTables = new DrawSpawnTables();
        private DrawSelected<Faction> _drawFactions = new DrawSelected<Faction>();
        private DrawSelected<Blueprint> _drawNPCTemplates = new DrawSelected<Blueprint>();
        private DrawSelected<Language> _drawLanguages = new DrawSelected<Language>();
        private DrawSelected<Blueprint> _drawNPCs = new DrawSelected<Blueprint>();
        private DrawSelected<Blueprint> _drawGenericNPCs = new DrawSelected<Blueprint>();
        private DrawSelected<LootTable> _drawLootTables = new DrawSelected<LootTable>();
        private DrawSelected<Blueprint> _drawItems = new DrawSelected<Blueprint>();
        private DrawSelected<Spell> _drawSpells = new DrawSelected<Spell>();
        private DrawSelected<Blueprint> _drawProps = new DrawSelected<Blueprint>();
        private DrawSelected<ColorReference> _drawColors = new DrawSelected<ColorReference>();

        private DrawUniverse _drawUniverse = new DrawUniverse();
        private DrawSFX _drawSFX = new DrawSFX();
        
        private const string PATH_BASE = "Assets/Deadspell/Resources/";
        private const string SPAWN_TABLE_PATH = PATH_BASE + "Spawn Tables";
        private const string FACTION_PATH = PATH_BASE + "Factions";
        private const string NPC_TEMPLATE_PATH = PATH_BASE + "Characters/NPC Templates";
        private const string LANGUAGE_PATH = PATH_BASE + "Languages";
        private const string NPC_PATH = PATH_BASE + "Characters/NPCs";
        private const string GENERIC_NPC_PATH = PATH_BASE + "Characters/Generic NPCs";
        private const string LOOT_TABLE_PATH = PATH_BASE + "Loot Tables";
        private const string ITEM_PATH = PATH_BASE + "Items";
        private const string SPELL_PATH = PATH_BASE + "Spells";
        private const string PROP_PATH = PATH_BASE + "Props";
        private const string COLOR_PATH = PATH_BASE + "Colors";

        protected override void Initialize()
        {
            base.Initialize();
            
            _drawSpawnTables.SetPath(SPAWN_TABLE_PATH);
            _drawFactions.SetPath(FACTION_PATH);
            _drawNPCTemplates.SetPath(NPC_TEMPLATE_PATH);
            _drawLanguages.SetPath(LANGUAGE_PATH);
            _drawNPCs.SetPath(NPC_PATH);
            _drawGenericNPCs.SetPath(GENERIC_NPC_PATH);
            _drawLootTables.SetPath(LOOT_TABLE_PATH);
            _drawItems.SetPath(ITEM_PATH);
            _drawSpells.SetPath(SPELL_PATH);
            _drawProps.SetPath(PROP_PATH);
            _drawColors.SetPath(COLOR_PATH);
            
            _drawUniverse.FindMyObject();
            _drawSFX.FindMyObject();
        }

        private void StateChange()
        {
            _treeRebuild = true;
        }

        protected override void OnGUI()
        {
            if (_treeRebuild && Event.current.type == EventType.Layout)
            {
                ForceMenuTreeRebuild();
                _treeRebuild = false;
            }
            
            SirenixEditorGUI.Title("Game Data Manager", "", TextAlignment.Center, true);
            EditorGUILayout.Space();
            
            switch (_managerState)
            {
                case ManagerState.SpawnTables:
                case ManagerState.Factions:
                case ManagerState.NpcTemplates:
                case ManagerState.Languages:
                case ManagerState.NPCs:
                case ManagerState.GenericNPCs:
                case ManagerState.LootTables:
                case ManagerState.Items:
                case ManagerState.Spells:
                case ManagerState.Props:
                case ManagerState.Colors:
                    base.DrawEditor(_enumIndex);
                    break;
                default:
                    break;
            }
            base.OnGUI();
        }

        protected override void DrawEditors()
        {
            switch (_managerState)
            {
                case ManagerState.Universe:
                    DrawEditor(_enumIndex);
                    break;
                case ManagerState.SpawnTables:
                    _drawSpawnTables.SetSelected(MenuTree.Selection.SelectedValue);
                    break;
                case ManagerState.Factions:
                    _drawFactions.SetSelected(MenuTree.Selection.SelectedValue);
                    break;
                case ManagerState.NpcTemplates:
                    _drawNPCTemplates.SetSelected(MenuTree.Selection.SelectedValue);
                    break;
                case ManagerState.Languages:
                    _drawLanguages.SetSelected(MenuTree.Selection.SelectedValue);
                    break;
                case ManagerState.GenericNPCs:
                    _drawGenericNPCs.SetSelected(MenuTree.Selection.SelectedValue);
                    break;
                case ManagerState.NPCs:
                    _drawNPCs.SetSelected(MenuTree.Selection.SelectedValue);
                    break;
                case ManagerState.LootTables:
                    _drawLootTables.SetSelected(MenuTree.Selection.SelectedValue);
                    break;
                case ManagerState.Items:
                    _drawItems.SetSelected(MenuTree.Selection.SelectedValue);
                    break;
                case ManagerState.Spells:
                    _drawSpells.SetSelected(MenuTree.Selection.SelectedValue);
                    break;
                case ManagerState.Props:
                    _drawProps.SetSelected(MenuTree.Selection.SelectedValue);
                    break;
                case ManagerState.Colors:
                    _drawColors.SetSelected(MenuTree.Selection.SelectedValue);
                    break;
                case ManagerState.SFX:
                    DrawEditor(_enumIndex);
                    break;
                default:
                    break;
            }
            
            DrawEditor((int)_managerState);
        }

        protected override IEnumerable<object> GetTargets()
        {
            List<object> targets = new List<object>();
            targets.Add(_drawUniverse); // Universe
            targets.Add(_drawSpawnTables); // Spawn Tables
            targets.Add(_drawFactions); // Factions
            targets.Add(_drawLanguages); // Languages
            targets.Add(_drawNPCTemplates); // NPC Templates
            targets.Add(_drawNPCs); // NPC
            targets.Add(_drawGenericNPCs); // Generic NPC
            targets.Add(_drawLootTables); // Loot Tables
            targets.Add(_drawItems); // Items
            targets.Add(_drawSpells); // Spells
            targets.Add(_drawProps); // Props
            targets.Add(_drawColors); // Color
            targets.Add(_drawSFX); // SFX
            targets.Add(base.GetTarget());

            _enumIndex = targets.Count - 1;
            
            return targets;
        }

        protected override void DrawMenu()
        {
            switch (_managerState)
            {
                case ManagerState.SpawnTables:
                case ManagerState.Factions:
                case ManagerState.NpcTemplates:
                case ManagerState.Languages:
                case ManagerState.NPCs:
                case ManagerState.GenericNPCs:
                case ManagerState.LootTables:
                case ManagerState.Items:
                case ManagerState.Spells:
                case ManagerState.Props:
                case ManagerState.Colors:
                    base.DrawMenu();
                    break;
                default:
                    break;
            }
        }

        protected override OdinMenuTree BuildMenuTree()
        {
            OdinMenuTree tree = new OdinMenuTree();
            
            switch (_managerState)
            {
                case ManagerState.SpawnTables:
                    tree.AddAllAssetsAtPath("Spawn Tables", SPAWN_TABLE_PATH, typeof(SpawnTable), true).SortMenuItemsByName();
                    break;
                case ManagerState.Factions:
                    tree.AddAllAssetsAtPath("Factions", FACTION_PATH, typeof(Faction), true).SortMenuItemsByName();
                    break;
                case ManagerState.NpcTemplates:
                    tree.AddAllAssetsAtPath("NPC Templates", NPC_TEMPLATE_PATH, typeof(Blueprint), true).SortMenuItemsByName();
                    break;
                case ManagerState.Languages:
                    tree.AddAllAssetsAtPath("Languages", LANGUAGE_PATH, typeof(Language), true).SortMenuItemsByName();
                    break;
                case ManagerState.NPCs:
                    tree.AddAllAssetsAtPath("NPCs", NPC_PATH, typeof(Blueprint), true).SortMenuItemsByName();
                    break;
                case ManagerState.GenericNPCs:
                    tree.AddAllAssetsAtPath("Generic NPCs", GENERIC_NPC_PATH, typeof(Blueprint), true).SortMenuItemsByName();
                    break;
                case ManagerState.LootTables:
                    tree.AddAllAssetsAtPath("Loot Tables", LOOT_TABLE_PATH, typeof(LootTable), true).SortMenuItemsByName();
                    break;
                case ManagerState.Items:
                    tree.AddAllAssetsAtPath("Items", ITEM_PATH, typeof(Blueprint), true).SortMenuItemsByName();
                    break;
                case ManagerState.Spells:
                    tree.AddAllAssetsAtPath("Spells", SPELL_PATH, typeof(Spell), true).SortMenuItemsByName();
                    break;
                case ManagerState.Props:
                    tree.AddAllAssetsAtPath("Props", PROP_PATH, typeof(Blueprint), true).SortMenuItemsByName();
                    break;
                case ManagerState.Colors:
                    tree.AddAllAssetsAtPath("Colors", COLOR_PATH, typeof(ColorReference), true).SortMenuItemsByName();
                    break;
                default:
                    break;
            }

            return tree;
        }
    }

    public class DrawSelected<T> where T : ScriptableObject
    {
        [InlineEditor(InlineEditorObjectFieldModes.CompletelyHidden)]
        public T Selected;
        
        [LabelWidth(100)]
        [PropertyOrder(-1)]
        [BoxGroup("CreateNew")]
        [HorizontalGroup("CreateNew/Horizontal")]
        public string NameForNew;
        private string _path;

        [HorizontalGroup("CreateNew/Horizontal")]
        [GUIColor(0.7f, 0.7f, 1f)]
        [Button]
        public void CreateNew()
        {
            if (string.IsNullOrEmpty(NameForNew))
            {
                return;
            }

            T newItem = ScriptableObject.CreateInstance<T>();
            newItem.name = "New " + typeof(T).ToString();

            if (string.IsNullOrEmpty(_path))
            {
                _path = "Assets/Deadspell/Resources";
            }
            
            AssetDatabase.CreateAsset(newItem, _path + "\\" + NameForNew + ".asset");
            AssetDatabase.SaveAssets();

            NameForNew = "";
        }

        [HorizontalGroup("CreateNew/Horizontal")]
        [GUIColor(1f, 0.7f, 0.7f)]
        [Button]
        public void DeleteSelected()
        {
            if (Selected != null)
            {
                string path = AssetDatabase.GetAssetPath(Selected);
                AssetDatabase.DeleteAsset(path);
                AssetDatabase.SaveAssets();
            }
        }

        public void SetSelected(object item)
        {
            if (item is T so)
            {
                Selected = so;
            }
        }

        public void SetPath(string path)
        {
            _path = path;
        }
    }

    public class DrawSceneObject<T> where T : MonoBehaviour
    {
        [ShowIf("@MyObject != null")]
        [InlineEditor(InlineEditorObjectFieldModes.CompletelyHidden)]
        public T MyObject;

        public void FindMyObject()
        {
            if (MyObject == null)
            {
                MyObject = GameObject.FindObjectOfType<T>();
            }
        }

        [ShowIf("@MyObject != null")]
        [GUIColor(0.7f, 1f, 0.7f)]
        [ButtonGroup("Top Button", -1000)]
        private void SelectSceneObject()
        {
            if (MyObject != null)
            {
                Selection.activeGameObject = MyObject.gameObject;
            }
        }

        [ShowIf("@MyObject == null")]
        [Button]
        private void CreateManagerObject()
        {
            GameObject newManager = new GameObject();
            newManager.name = "New " + typeof(T).ToString();
            MyObject = newManager.AddComponent<T>();
        }
    }
}

public class DrawSpawnTables : DrawSelected<SpawnTable>
{
    [GUIColor(0.7f, 1f, 1f)]
    [ButtonGroup("Top Button", -1000)]
    private void Recalculate()
    {
        Selected.Items.RecalculateTotalWeight();
        Selected.Props.RecalculateTotalWeight();
        Selected.Characters.RecalculateTotalWeight();
    }
}

public class DrawUniverse : DrawSceneObject<UniverseManager>
{
    
}

public class DrawSFX : DrawSceneObject<SFXManager>
{
    
}