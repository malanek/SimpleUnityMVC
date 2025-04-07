using System;
using UnityEngine;

namespace MExtensions.MVC
{
    public abstract class View<M> : MonoBehaviour where M : class
    {
        private M model;
#pragma warning disable IDE1006
        /// <summary>
        /// Overrides `transform` to refer to the entire object instance, not just the view.
        /// The original view's transform is available via `viewTransform`.
        /// </summary>
        protected new Transform transform { get; private set; }

        protected Transform viewTransform { get; private set; }
#pragma warning restore IDE1006

        protected M Model
        {
            get
            {
                if (model == null)
                    throw new Exception("<color=#FF0000>MVC is not initialized!</color>");
                return model;
            }
        }

        public void InitializeMVC(M model, Transform parent)
        {
            this.model = model;
            viewTransform = GetComponent<Transform>();
            transform = parent;
            InitializeView();
        }

        protected abstract void InitializeView();
    }
}