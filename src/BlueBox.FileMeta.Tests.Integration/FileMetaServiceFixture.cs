﻿using BlueBox.FileMeta.Api;
using BlueBox.FileMeta.Impl;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace BlueBox.FileMeta.Tests.Integration
{
    /// <summary>
    /// <para>
    /// Fixture class for the <code>FileMetaService</code> integration tests. 
    /// </para>
    /// <para>
    /// All instances of the implementations retrieved from this class are shared. For instance, if test A calls <code>GetFooService()</code> from this fixture, and test B calls the same method, the two tests get the exact same instance in memory.
    /// </para>
    /// </summary>
    public class FileMetaServiceFixture : IDisposable
    {
        private IServiceProvider serviceProvider;

        /// <summary>
        /// Constructor
        /// </summary>
        public FileMetaServiceFixture()
        {
            serviceProvider = new ServiceCollection()
                .AddSingleton<IFileMetaService, FileMetaServiceImpl>()
                .BuildServiceProvider();
        }

        /// <summary>
        /// Retrieve an instance of the <code>IFileMetaService</code> implementation.
        /// </summary>
        /// <returns></returns>
        public IFileMetaService GetFileMetaService()
        {
            return serviceProvider.GetService<IFileMetaService>();
        }

        /// <summary>
        /// Dispose of the fixture.
        /// </summary>
        public void Dispose()
        {
        }
    }
}