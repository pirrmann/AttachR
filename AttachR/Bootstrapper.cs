﻿using System;
using System.Collections.Generic;
using System.Windows;
using AttachR.Components.Recent;
using AttachR.ViewModels;
using Caliburn.Micro;

namespace AttachR
{
    public class Bootstrapper : BootstrapperBase
    {
        private readonly SimpleContainer container = new SimpleContainer();
        
        public Bootstrapper()
        {
            Initialize();
        }

        protected override object GetInstance(Type serviceType, string key)
        {
            return container.GetInstance(serviceType, key) ?? base.GetInstance(serviceType, key);
        }

        protected override IEnumerable<object> GetAllInstances(Type serviceType)
        {
            return container.GetAllInstances(serviceType);
        }

        protected override void BuildUp(object instance)
        {
            container.BuildUp(instance);
        }

        protected override void Configure()
        {
            container.RegisterInstance(
                typeof(IRecentFileListPersister), 
                null, 
                new RegistryPersister());
            container.PerRequest<MainViewModel, MainViewModel>();
            container.Singleton<IEventAggregator, EventAggregator>();
        }

        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            DisplayRootViewFor<MainViewModel>();
        }
    }
}