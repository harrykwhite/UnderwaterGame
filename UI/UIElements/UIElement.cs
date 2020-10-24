using System;
using System.Collections.Generic;
using UnderwaterGame.UI.UIComponents;

namespace UnderwaterGame.UI.UIElements
{
    public abstract class UIElement
    {
        public bool loadingUpdate;

        public List<UIComponent> Components { get; private set; } = new List<UIComponent>();

        public abstract void Init();
        public abstract void Update();
        public abstract void Draw();

        protected UIComponent AddComponent<T>() where T : UIComponent
        {
            UIComponent component = Activator.CreateInstance<T>();
            component.Init();

            Components.Add(component);
            return component;
        }

        protected T GetComponent<T>() where T : UIComponent
        {
            foreach (UIComponent component in Components)
            {
                if (component is T)
                {
                    return (T)component;
                }
            }

            return null;
        }
    }
}