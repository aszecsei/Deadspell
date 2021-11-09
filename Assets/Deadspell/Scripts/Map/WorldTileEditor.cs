using System;
using System.Collections.Generic;
using Sirenix.OdinInspector.Editor;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Deadspell.Map
{

    public class WorldTileThemeProcessor<T> : OdinPropertyProcessor<T> where T : WorldTileTheme
    {
        public override void ProcessMemberProperties(List<InspectorPropertyInfo> propertyInfos)
        {
            propertyInfos.Clear();
            
            propertyInfos.AddMember("Default");

            foreach (WorldTileType wtt in Enum.GetValues(typeof(WorldTileType)))
            {
                propertyInfos.AddValue(wtt.ToString(), (ref WorldTileTheme theme) => theme[wtt], (ref WorldTileTheme theme, WorldTileTheme.TileData value) =>
                {
                    theme[wtt] = value;
                });
            }
        }
    }

    [CustomEditor(typeof(WorldTile))]
    public class WorldTileEditor : Editor
    {
        private SerializedProperty _theme;
        private SerializedProperty _tileType;
        private SerializedProperty _name;
        private SerializedProperty _colorOverride;

        private void OnEnable()
        {
            _theme = serializedObject.FindProperty(nameof(WorldTile.Theme));
            _tileType = serializedObject.FindProperty(nameof(WorldTile.TileType));
            _name = serializedObject.FindProperty(nameof(WorldTile.Name));
            _colorOverride = serializedObject.FindProperty(nameof(WorldTile.ColorOverride));
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            EditorGUILayout.PropertyField(_theme);
            EditorGUILayout.PropertyField(_tileType);
            EditorGUILayout.PropertyField(_name);
            EditorGUILayout.PropertyField(_colorOverride);
            serializedObject.ApplyModifiedProperties();
        }

        public override Texture2D RenderStaticPreview(string assetPath, Object[] subAssets, int width, int height)
        {
            WorldTile tile = AssetDatabase.LoadAssetAtPath<WorldTile>(assetPath);
            if (tile.sprite != null)
            {
                Texture2D spritePreview = null;
                while (spritePreview == null)
                {
                    spritePreview = AssetPreview.GetAssetPreview(tile.Theme[tile.TileType].Sprite);
                }

                Color tint = Color.white;
                if (tile.ColorOverride != null)
                {
                    tint = tile.ColorOverride;
                }
                else if (tile.Theme[tile.TileType].Color != null)
                {
                    tint = tile.Theme[tile.TileType].Color;
                }
            
                Color[] pixels = spritePreview.GetPixels();
                for (int i = 0; i < pixels.Length; i++)
                {
                    pixels[i] = pixels[i] * tint; // Tint
                }
                spritePreview.SetPixels(pixels);
                spritePreview.Apply();
 
                Texture2D preview = new Texture2D(width, height);
                EditorUtility.CopySerialized(spritePreview, preview); // Returning the original texture causes an editor crash
                return preview;
            }

            return base.RenderStaticPreview(assetPath, subAssets, width, height);
        }
    }
}