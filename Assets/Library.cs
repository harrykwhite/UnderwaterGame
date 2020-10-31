namespace UnderwaterGame.Assets
{
    using Microsoft.Xna.Framework.Content;
    using System;
    using System.Collections.Generic;
    using System.Reflection;

    public abstract class Library<T>
    {
        public class LibraryAsset
        {
            public T asset;

            public string assetPath;

            public LibraryAsset(string path)
            {
                assetPath = path;
            }

            public void Load(ContentManager content)
            {
                asset = content.Load<T>(assetPath);
            }
        }

        public List<LibraryAsset> assets = new List<LibraryAsset>();

        public void LoadAll(ContentManager content, Type type)
        {
            FieldInfo[] fieldInfo = GetType().GetFields();
            foreach(FieldInfo field in fieldInfo)
            {
                if(field.IsInitOnly && field.FieldType == typeof(LibraryAsset))
                {
                    LibraryAsset asset = (LibraryAsset)field.GetValue(this);
                    asset.Load(content);
                    assets.Add(asset);
                }
            }
        }
    }
}