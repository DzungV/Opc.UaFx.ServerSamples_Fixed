namespace Server
{
    using System.Threading;

    using Opc.UaFx;
    using Opc.UaFx.Server;

    public class Program
    {
        public static void Main()
        {
            var temperatureNode = new OpcDataVariableNode<double>("Temperature");

            var positionNode = new OpcDataVariableNode<sbyte>("Position");

            var statusNode = new OpcDataVariableNode<byte>("Status");

            var isActiveNode = new OpcDataVariableNode<bool>("IsActive");
            
            var nameNode = new OpcDataVariableNode<string>("Name");

            using (var server = new OpcServer("opc.tcp://localhost:4840/", temperatureNode, nameNode, positionNode, statusNode, isActiveNode)) {
                server.Start();

                while (true) {
                    
                }
            }
        }
    }
}
