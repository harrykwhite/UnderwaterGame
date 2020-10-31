namespace UnderwaterGame.Ui.UiElements
{
    using System;
    using System.Collections.Generic;
    using UnderwaterGame.Ui.UiComponents;

    public abstract class UiElement
    {
        public bool loadingUpdate;

        public List<UiComponent> components = new List<UiComponent>();

        public abstract void Init();

        public abstract void Update();

        public abstract void Draw();

        protected UiComponent AddComponent<T>() where T : UiComponent
        {
            UiComponent component = Activator.CreateInstance<T>();
            component.Init();
            components.Add(component);
            return component;
        }

        protected T GetComponent<T>() where T : UiComponent
        {
            foreach(UiComponent component in components)
            {
                if(component is T)
                {
                    return (T)component;
                }
            }
            return null;
        }
    }
}