using System;
using Serilog;


namespace SharedModel.Services
{
    public class EmailSender:IEmailSender
    {
        

        public void Send(string orderString)
        {

            Log.Information("Sending email: {@email}", orderString);
        }
    }
}
