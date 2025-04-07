using System;
using UnityEngine;

namespace MExtensions.MVC
{
    /// <summary>
    /// Base class for implementing the MVC pattern in Unity.
    /// </summary>
    /// <typeparam name="M">Model type</typeparam>
    /// <typeparam name="V">View type</typeparam>
    public abstract class MVC<M, V> : MonoBehaviour
        where M : class
        where V : View<M>
    {
        private M modelComponent;
        private V viewComponent;

        /// <summary>
        /// Gets the model component.
        /// </summary>
        protected M Model
        {
            get
            {
                if (modelComponent == null)
                {
                    throw new Exception($"MVC for {typeof(M)} is not properly initialized in {gameObject.GetType()}!");
                }

                return modelComponent;
            }
        }

        /// <summary>
        /// Gets the view component.
        /// </summary>
        protected V View
        {
            get
            {
                if (viewComponent == null)
                {
                    throw new Exception($"MVC for {typeof(V)} is not properly initialized in {gameObject.GetType()}!");
                }

                return viewComponent;
            }
        }

        /// <summary>
        /// Initializes the MVC components.
        /// </summary>
        /// <param name="model">The model component.</param>
        protected void InitializeMVC(M model)
        {
            this.modelComponent = model;
            viewComponent = GetComponentInChildren<V>();
            if (viewComponent == null)
            {
                throw new Exception($"View component of type: {typeof(V)} for {gameObject.GetType()} is not found!");
            }

            viewComponent.InitializeMVC(Model, transform);
        }

        /// <summary>
        /// Disposes of the MVC components.
        /// </summary>
        protected void DisposeMVC()
        {
            modelComponent = null;
            viewComponent = null;
        }
    }
}