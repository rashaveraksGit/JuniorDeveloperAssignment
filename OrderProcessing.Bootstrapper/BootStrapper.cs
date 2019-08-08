using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using OrderProcessing.WorkflowSteps;
using SharedModel.Repositories;
using SharedModel.Services;
using Unity;
using Unity.RegistrationByConvention;

namespace OrderProcessing.Bootstrapper
{
    public static class BootStrapper
    {

        public static UnityContainer  Initialize()
        {
            var container = new UnityContainer();

            
            container.RegisterType<IOrderProcessor, OrderProcessing.OrderProcessor>();
            container.RegisterInstance<IWorkflowStepServiceLocator>(new WorkflowStepServiceLocator(container));

            container.RegisterInstance<ICustomerStatusSettings>(new CustomerStatusSettings());
            
            container.RegisterTypes(AllClasses.FromAssemblies( Assembly.GetAssembly(typeof(CustomerRepository))), WithMappings.FromAllInterfaces, WithName.Default, WithLifetime.None);
            container.RegisterTypes(AllClasses.FromAssemblies(Assembly.GetAssembly(typeof(CommitOrderStep))).Where<Type>(t => t.Namespace.EndsWith("WorkflowSteps")), (t)=> new List<Type>() { typeof(IWorkflowStep) }, (t) => t.Name.Replace("Step", ""));
            

            return container;
        }
    }

    public class CustomerStatusSettings: ICustomerStatusSettings
    {

        public CustomerStatusSettings()
        {
            MinimumQuarantineIntervalInDays = 7; // From config settings
        }
        public int MinimumQuarantineIntervalInDays { get; set; }
    }

    public class WorkflowStepServiceLocator:IWorkflowStepServiceLocator
    {
        private readonly UnityContainer _container;

        public WorkflowStepServiceLocator(UnityContainer container)
        {
            _container = container;
       
        }

        public IWorkflowStep GetNextStep(State state)
        {
            return _container.Resolve<IWorkflowStep>(state.ToString());
        }
    }
}
