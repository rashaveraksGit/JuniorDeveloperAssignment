namespace OrderProcessing
{
    public interface IWorkflowStepServiceLocator
    {
        IWorkflowStep GetNextStep(State batchWorkflowState);
    }
}