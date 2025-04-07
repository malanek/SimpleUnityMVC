# 🧩 SimpleUnityMVC

**SimpleUnityMVC** is a minimal, strongly-typed MVC implementation for Unity — built specifically for **game object prefabs**, not UI.  
It cleanly separates **game logic**, **data**, and **visuals**, and is designed to work seamlessly with **Zenject** and prefab factories.

## ✨ Features

-   Pure, zero-dependency C# — just clean architecture
-   Designed specifically for prefab-based game objects (NOT UI)
-   Compatible with Zenject's DI and factory-based workflow
-   Enforces strong Model–View–Controller separation
-   No runtime overhead, no reflection, no magic methods
-   Great for handling DoTween, Animator, Materials, FX in View

## 📦 Installation

### Option 1 – Unity Package Manager (UPM)

Add this line to your `manifest.json`:

```json
"com.malanek.simpleunitymvc": "https://github.com/malanek/SimpleUnityMVC.git?path=Runtime#main"
```

> ☝️ Replace with your actual GitHub repo link.

### Option 2 – Manual

Copy `MVC.cs` and `View.cs` into your project.

## 🚀 Quick Start

You’re expected to use **Zenject factories** to spawn your game objects and inject the model.

```csharp
public class PlayerModel
{
    public int Health;
}

public class PlayerView : View<PlayerModel>
{
    protected override void InitializeView()
    {
        // Run DoTween, trigger Animator, set materials, etc.
        Debug.Log($"Spawning player with HP: {Model.Health}");
    }
}

public class PlayerController : MVC<PlayerModel, PlayerView>
{
    public void TakeDamage(int amount)
    {
        Model.Health -= amount;
        // No visual logic here — delegate to View
    }
}
```

## 🔄 Zenject Factory Example

```csharp
public class PlayerFactory : IFactory<PlayerModel, PlayerController>
{
    readonly DiContainer _container;
    readonly GameObject _prefab;

    public PlayerFactory(DiContainer container, GameObject prefab)
    {
        _container = container;
        _prefab = prefab;
    }

    public PlayerController Create(PlayerModel model)
    {
        var controller = _container.InstantiatePrefabForComponent<PlayerController>(_prefab);
        controller.Initialize(model); // Always call this
        return controller;
    }
}
```

## 🧠 Architecture Overview

| Component              | Responsibility                           |
| ---------------------- | ---------------------------------------- |
| **Model**              | Pure data, no Unity references           |
| **Controller (MVC<>)** | Logic and behavior, no visuals           |
| **View**               | Animator, DoTween, Renderer, VFX, sounds |

### `MVC<M, V>`

-   Generic controller base
-   Holds the model and the view
-   Initializes view with the model

### `View<M>`

-   Abstract base for view components
-   Gets reference to the model
-   Handles all visual side-effects

### View exposes:

-   `Model`: read-only access to the model
-   `transform`: the controller's transform
-   `viewTransform`: the view GameObject’s transform

## ❌ What This Is NOT

-   Not for UI (use UI Toolkit or uGUI MVVM instead)
-   Not reactive — no observers or data binding (you handle that)
-   Not bloated — no extra layers or magic injection

## 🧪 Compatibility

-   Unity 2020.3+
-   Works with Zenject 9+
-   Tested with DoTween, Animator, custom shaders

## 📃 License

MIT License.  
Use it, clone it, modify it. Just don't write spaghetti.
