using Stateless.Graph;

namespace OrderProcessing
{
    using Stateless;


    public partial class OrderWorkflow
    {
        private State _state;
        private readonly StateMachine<State, Trigger> _stateMachine;

        public OrderWorkflow()
        {
            _stateMachine = new StateMachine<State, Trigger>(() => _state, s => _state = s);
            _stateMachine.Configure(State.New)
               
                .Permit(Trigger.Next, State.GetCustomer)
                .Permit(Trigger.InvalidateAndAbort, State.Invalid);


            _stateMachine.Configure(State.GetCustomer)

                .Permit(Trigger.Next,State.SetCustomerStatus)
                .Permit(Trigger.InvalidateAndAbort, State.Invalid);

            _stateMachine.Configure(State.SetCustomerStatus)

                .Permit(Trigger.Next, State.SetDiscount)
                .Permit(Trigger.InvalidateAndAbort, State.Invalid);

            _stateMachine.Configure(State.SetDiscount)

                .Permit(Trigger.Next, State.CommitOrder)
                .Permit(Trigger.InvalidateAndAbort, State.Invalid);

            _stateMachine.Configure(State.CommitOrder)

                .Permit(Trigger.Next, State.SendConfirmationEmail)
                .Permit(Trigger.InvalidateAndAbort, State.Invalid);


            _stateMachine.Configure(State.SendConfirmationEmail)

                .Permit(Trigger.Next, State.Completed)
                .Permit(Trigger.InvalidateAndAbort, State.Invalid);

            _stateMachine.Configure(State.Completed)

                .Ignore(Trigger.Next)
                .Ignore(Trigger.InvalidateAndAbort);

            _stateMachine.Configure(State.Invalid)
               
                .Ignore(Trigger.Next)
                .Ignore(Trigger.InvalidateAndAbort);


            _state = State.New;



        }

      
        public State CurrentState => _state;

        public State Next()
        {
            _stateMachine.Fire(Trigger.Next);
            return _stateMachine.State;
        }

        public void Abort()
        {
            _stateMachine.Fire(Trigger.InvalidateAndAbort);

        }

        public enum Trigger
        {
            Next,
            InvalidateAndAbort
        }


        public string ToDotGraph()
        {

           return UmlDotGraph.Format(_stateMachine.GetInfo());
        }



    }

}
