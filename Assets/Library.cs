using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace UnderwaterGame.Assets
{
    public abstract class Library<T>
    {
        public class LibraryAsset
        {
            public T Asset { get; private set; }
            public string AssetPath { get; private set; }

            public LibraryAsset(string path)
            {
                AssetPath = path;
            }

            public void Load(ContentManager content)
            {
                Asset = content.Load<T>(AssetPath);
            }
        }

        public List<LibraryAsset> Assets { get; private set; } = new List<LibraryAsset>();

        public void LoadAll(ContentManager content, Type type)
        {
            FieldInfo[] fieldInfo = GetType().GetFields();

            foreach (FieldInfo field in fieldInfo)
            {
                if (field.IsInitOnly && field.FieldType == typeof(LibraryAsset))
                {
                    LibraryAsset asset = (LibraryAsset)field.GetValue(this);
                    asset.Load(content);

                    Assets.Add(asset);
                }
            }
        }
    }
}