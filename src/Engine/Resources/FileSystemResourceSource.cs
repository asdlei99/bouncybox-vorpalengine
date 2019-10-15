using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Collections.ObjectModel;
using System.IO;
using System.Runtime.CompilerServices;

namespace BouncyBox.VorpalEngine.Engine.Resources
{
    /// <summary>
    ///     Represents a file system source for binary resources.
    /// </summary>
    public class FileSystemResourceSource : IResourceSource
    {
        private static readonly ReadOnlyDictionary<ResourceType, (string folderName, string fileExtension)> MappingsByResourceTypes =
            new ReadOnlyDictionary<ResourceType, (string folderName, string fileExtension)>(
                new Dictionary<ResourceType, (string folderName, string fileExtension)>
                {
                    { ResourceType.Image, ("images", ".png") },
                    { ResourceType.Sound, ("sounds", ".wav") }
                });

        private readonly string _rootDirectory;

        /// <summary>
        ///     Initializes a new instance of the <see cref="FileSystemResourceSource" /> type.
        /// </summary>
        /// <param name="rootDirectory">A fully-qualified directory that contains resource subdirectories named by type.</param>
        /// <exception cref="ArgumentException">Thrown when the root directory is not fully-qualified.</exception>
        public FileSystemResourceSource(string rootDirectory)
        {
            if (!Path.IsPathFullyQualified(rootDirectory))
            {
                throw new ArgumentException("Root directory is not fully-qualified.", nameof(rootDirectory));
            }

            _rootDirectory = rootDirectory;
        }

        /// <inheritdoc />
        public (GetResourceResult result, ImmutableArray<byte>? data) GetResource(ResourceDescriptor descriptor)
        {
            string path = BuildPath(descriptor.Type, descriptor.Key);

            if (!File.Exists(path))
            {
                return (GetResourceResult.ResourceNotFound, null);
            }

            byte[] data = File.ReadAllBytes(path);
            // Unsafe code to avoid copying the array just to create an immutable array
            ImmutableArray<byte> immutableData = Unsafe.As<byte[], ImmutableArray<byte>>(ref data);

            return (GetResourceResult.Valid, immutableData);
        }

        /// <summary>
        ///     Builds a full path for the given resource type and key. The path is rooted to the provided root directory.
        /// </summary>
        /// <param name="type">The type of the resource.</param>
        /// <param name="key">The key of the resource.</param>
        /// <returns>Returns a full path to the resource.</returns>
        private string BuildPath(ResourceType type, string key)
        {
            (string folderName, string fileExtension) = MappingsByResourceTypes[type];

            return Path.Combine(_rootDirectory, folderName, key + fileExtension);
        }
    }
}