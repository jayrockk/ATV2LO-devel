using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FTR2LO_Vail
{
    class Program
    {

        // http://www.programminghelp.com/programming/dotnet/wcf-creating-and-implementing-a-service-in-c/
        static void Main(string[] args)
        {

            string address = "http://localhost:41432/WCFService1/FTR2LO_InternalService";
            System.ServiceModel.WSHttpBinding binding = new System.ServiceModel.WSHttpBinding();
            binding.Name = "WSHttpBinding_IFTR2LO"; // not sure if this is necessary.
            System.ServiceModel.EndpointAddress endpointAddress = new System.ServiceModel.EndpointAddress(address);
            FTR2LOClient client = new FTR2LOClient(binding, endpointAddress);

            //FTR2LOClient client = new FTR2LOClient();
            bool _isconnectedtoFTR2LO = false;
            Console.Write("Is connected to FTR2LO: ");
            try
            {
                _isconnectedtoFTR2LO = client.IsConnectedToFTR2LO();
            }
            catch (Exception)
            {
                //Console.WriteLine("Error");
            }
            Console.WriteLine(_isconnectedtoFTR2LO.ToString());

            if (_isconnectedtoFTR2LO)
            {
                Console.WriteLine("Is connected to FTR: " + client.IsConnectedToFTR());
                int status = client.IPingFTR();
                Console.WriteLine("IPingFTR: " + status.ToString());
                Console.WriteLine("IPingFTRToString: " + client.IPingFTRToString(status));
            }
            if (client.State == System.ServiceModel.CommunicationState.Opened)
                client.Close();
            Console.WriteLine();
            Console.WriteLine("Press the ENTER key to terminate client.");
            Console.ReadLine();

        }
    }
}
