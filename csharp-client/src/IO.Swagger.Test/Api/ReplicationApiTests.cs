/* 
 * Swagger Server
 *
 * No descripton provided (generated by Swagger Codegen https://github.com/swagger-api/swagger-codegen)
 *
 * OpenAPI spec version: 1.0.0
 * 
 * Generated by: https://github.com/swagger-api/swagger-codegen.git
 */

using System;
using System.IO;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using RestSharp;
using NUnit.Framework;

using IO.Swagger.Client;
using IO.Swagger.Api;
using IO.Swagger.Model;

namespace IO.Swagger.Test
{
    /// <summary>
    ///  Class for testing ReplicationApi
    /// </summary>
    /// <remarks>
    /// This file is automatically generated by Swagger Codegen.
    /// Please update the test case below to test the API endpoint.
    /// </remarks>
    [TestFixture]
    public class ReplicationApiTests
    {
        private ReplicationApi instance;

        /// <summary>
        /// Setup before each unit test
        /// </summary>
        [SetUp]
        public void Init()
        {
            instance = new ReplicationApi();
        }

        /// <summary>
        /// Clean up after each unit test
        /// </summary>
        [TearDown]
        public void Cleanup()
        {

        }

        /// <summary>
        /// Test an instance of ReplicationApi
        /// </summary>
        [Test]
        public void InstanceTest()
        {
            // TODO uncomment below to test 'IsInstanceOfType' ReplicationApi
            //Assert.IsInstanceOfType(typeof(ReplicationApi), instance, "instance is a ReplicationApi");
        }

        
        /// <summary>
        /// Test OpDpReplicationHistoryGet
        /// </summary>
        [Test]
        public void OpDpReplicationHistoryGetTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //string acceptLanguage = null;
            //var response = instance.OpDpReplicationHistoryGet(acceptLanguage);
            //Assert.IsInstanceOf<List<DisplayReplicationDetails>> (response, "response is List<DisplayReplicationDetails>");
        }
        
        /// <summary>
        /// Test OpIpoolReplicationGet
        /// </summary>
        [Test]
        public void OpIpoolReplicationGetTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //string acceptLanguage = null;
            //var response = instance.OpIpoolReplicationGet(acceptLanguage);
            //Assert.IsInstanceOf<List<IpPoolInfo>> (response, "response is List<IpPoolInfo>");
        }
        
        /// <summary>
        /// Test OpIpoolReplicationPoolidDelete
        /// </summary>
        [Test]
        public void OpIpoolReplicationPoolidDeleteTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //string poolId = null;
            //string acceptLanguage = null;
            //instance.OpIpoolReplicationPoolidDelete(poolId, acceptLanguage);
            
        }
        
        /// <summary>
        /// Test OpIpoolReplicationPoolidGet
        /// </summary>
        [Test]
        public void OpIpoolReplicationPoolidGetTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //string poolId = null;
            //string acceptLanguage = null;
            //var response = instance.OpIpoolReplicationPoolidGet(poolId, acceptLanguage);
            //Assert.IsInstanceOf<IpPoolInfo> (response, "response is IpPoolInfo");
        }
        
        /// <summary>
        /// Test OpIpoolReplicationPoolidPut
        /// </summary>
        [Test]
        public void OpIpoolReplicationPoolidPutTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //string poolId = null;
            //List<IpRange> body = null;
            //string acceptLanguage = null;
            //var response = instance.OpIpoolReplicationPoolidPut(poolId, body, acceptLanguage);
            //Assert.IsInstanceOf<IpPoolInfo> (response, "response is IpPoolInfo");
        }
        
        /// <summary>
        /// Test OpIpoolReplicationPost
        /// </summary>
        [Test]
        public void OpIpoolReplicationPostTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //IpPoolSpec body = null;
            //string acceptLanguage = null;
            //var response = instance.OpIpoolReplicationPost(body, acceptLanguage);
            //Assert.IsInstanceOf<List<IpPoolInfo>> (response, "response is List<IpPoolInfo>");
        }
        
        /// <summary>
        /// Test OpReplicationNetworkGet
        /// </summary>
        [Test]
        public void OpReplicationNetworkGetTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //string acceptLanguage = null;
            //var response = instance.OpReplicationNetworkGet(acceptLanguage);
            //Assert.IsInstanceOf<ReplicationNetwork> (response, "response is ReplicationNetwork");
        }
        
        /// <summary>
        /// Test OpReplicationNetworkPost
        /// </summary>
        [Test]
        public void OpReplicationNetworkPostTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //ReplicationNetwork body = null;
            //string acceptLanguage = null;
            //var response = instance.OpReplicationNetworkPost(body, acceptLanguage);
            //Assert.IsInstanceOf<ReplicationNetwork> (response, "response is ReplicationNetwork");
        }
        
        /// <summary>
        /// Test OpReplicationNetworkPut
        /// </summary>
        [Test]
        public void OpReplicationNetworkPutTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //ReplicationNetwork body = null;
            //string acceptLanguage = null;
            //var response = instance.OpReplicationNetworkPut(body, acceptLanguage);
            //Assert.IsInstanceOf<ReplicationNetwork> (response, "response is ReplicationNetwork");
        }
        
        /// <summary>
        /// Test OpReplicationPeerGet
        /// </summary>
        [Test]
        public void OpReplicationPeerGetTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //string acceptLanguage = null;
            //var response = instance.OpReplicationPeerGet(acceptLanguage);
            //Assert.IsInstanceOf<List<MapPairErToReplicationPeerInfo>> (response, "response is List<MapPairErToReplicationPeerInfo>");
        }
        
        /// <summary>
        /// Test OpReplicationPeerPeerClusterIdDatastoresGet
        /// </summary>
        [Test]
        public void OpReplicationPeerPeerClusterIdDatastoresGetTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //string peerClusterId = null;
            //string acceptLanguage = null;
            //var response = instance.OpReplicationPeerPeerClusterIdDatastoresGet(peerClusterId, acceptLanguage);
            //Assert.IsInstanceOf<List<ReplicationDatastore>> (response, "response is List<ReplicationDatastore>");
        }
        
        /// <summary>
        /// Test OpReplicationPeerPeerClusterIdDatastoresMapPut
        /// </summary>
        [Test]
        public void OpReplicationPeerPeerClusterIdDatastoresMapPutTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //string peerClusterId = null;
            //List<MapReplPlatDatastoreToReplPlatDatastore> body = null;
            //string acceptLanguage = null;
            //var response = instance.OpReplicationPeerPeerClusterIdDatastoresMapPut(peerClusterId, body, acceptLanguage);
            //Assert.IsInstanceOf<MapPairErToReplicationPeerInfo> (response, "response is MapPairErToReplicationPeerInfo");
        }
        
        /// <summary>
        /// Test OpReplicationPeerPeerClusterIdPut
        /// </summary>
        [Test]
        public void OpReplicationPeerPeerClusterIdPutTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //string peerClusterId = null;
            //Credential body = null;
            //string acceptLanguage = null;
            //instance.OpReplicationPeerPeerClusterIdPut(peerClusterId, body, acceptLanguage);
            
        }
        
        /// <summary>
        /// Test OpReplicationPeerPost
        /// </summary>
        [Test]
        public void OpReplicationPeerPostTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //ReplicationPeerSpec body = null;
            //string acceptLanguage = null;
            //var response = instance.OpReplicationPeerPost(body, acceptLanguage);
            //Assert.IsInstanceOf<MapPairErToReplicationPeerInfo> (response, "response is MapPairErToReplicationPeerInfo");
        }
        
    }

}
