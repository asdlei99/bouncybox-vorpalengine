﻿using System;

namespace BouncyBox.VorpalEngine.Engine.Scenes
{
    /// <summary>Represents a collection of entities that form one logical game unit.</summary>
    public interface IScene<out TSceneKey> : IDisposable
    {
        /// <summary>Gets the scene key.</summary>
        TSceneKey Key { get; }

        /// <summary>Initializes the parts of the game state owned by the scene.</summary>
        void Load();

        /// <summary>Uninitializes the parts of the game state owned by the scene.</summary>
        void Unload();
    }
}