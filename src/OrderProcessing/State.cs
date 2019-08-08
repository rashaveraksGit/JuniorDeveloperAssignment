namespace OrderProcessing
{

   
    public enum State
    {
        New,
        Invalid,
        GetCustomer,
        CommitOrder,
        SetCustomerStatus,
        SetDiscount,
        SendConfirmationEmail,
        Completed
    }


}


