﻿// Copyright (c) Traeger Industry Components GmbH. All Rights Reserved.

namespace Browsing
{
    using System;

    using Opc.UaFx;
    using Opc.UaFx.Client;

    /// <summary>
    /// This sample demonstrates how to browse nodes and their attributes provided by
    /// the OPC UA server.
    /// </summary>
    public class Program
    {
        #region ---------- Public static methods ----------

        public static void Main(string[] args)
        {
            //// If the server domain name does not match localhost just replace it
            //// e.g. with the IP address or name of the server machine.

            var client = new OpcClient("opc.tcp://localhost:4840/SampleServer");
            client.Connect();

            // The class OpcObjectTypes contains all default OpcNodeId's of all nodes typically
            // provided by default by every OPC UA server. Here we start browsing at the top most
            // node the ObjectsFolder. Beneath this node there are all further nodes in an
            // OPC UA server registered.
            // Creating an OpcBrowseNode does only prepare an OpcBrowseNodeContext which is linked
            // to the OpcClient which creates it and contains all further browsing relevant
            // contextual metadata (e.g. the view and the referenceTypeIds). The used overload of
            // CreateBrowseNode(...) browses the default view of the server while it takes care of
            // HierarchicalReferences(see ReferenceTypeIds).
            // After creating the browse node for the ObjectsFolder we traverse the whole node
            // tree in preorder.
            // In case there you have the OpcNodeId of a specific node (e.g. after browsing for a
            // specific node) it is also possible to pass that OpcNodeId to CreateBrowseNode(...)
            // to browse starting at that node.
            var node = client.BrowseNode(OpcObjectTypes.ObjectsFolder);
            Program.Browse(node);

            client.Disconnect();
            Console.ReadKey(true);
        }

        #endregion

        #region ---------- Private static methods ----------

        private static void Browse(OpcNodeInfo node)
        {
            Program.Browse(node, 0);
        }

        private static void Browse(OpcNodeInfo node, int level)
        {
            //// In general attributes and children are retrieved from the server on demand. This
            //// is done to reduce the amount of traffic and to improve the preformance when
            //// searching/browsing for specific attributes or children. After attributes or
            //// children of a node were browsed they are stored internally so that subsequent
            //// attribute and children requests are processed without any interaction with the
            //// OPC UA server.

            // Browse the DisplayName attribute of the node. It is also possible to browse
            // multiple attributes at once (see the method Attributes(...)).
            var displayName = node.Attribute(OpcAttribute.DisplayName);

            Console.WriteLine(
                    "{0}{1} ({2})",
                    new string(' ', level * 4),
                    node.NodeId.ToString(OpcNodeIdFormat.Foundation),
                    displayName.Value);

            // Browse the children of the node and continue browsing in preorder.
            foreach (var childNode in node.Children())
                Program.Browse(childNode, level + 1);
        }

        #endregion
    }
}
