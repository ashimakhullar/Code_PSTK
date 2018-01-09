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
    ///  Class for testing VaaiApi
    /// </summary>
    /// <remarks>
    /// This file is automatically generated by Swagger Codegen.
    /// Please update the test case below to test the API endpoint.
    /// </remarks>
    [TestFixture]
    public class VaaiApiTests
    {
        private VaaiApi instance;

        /// <summary>
        /// Setup before each unit test
        /// </summary>
        [SetUp]
        public void Init()
        {
            instance = new VaaiApi();
        }

        /// <summary>
        /// Clean up after each unit test
        /// </summary>
        [TearDown]
        public void Cleanup()
        {

        }

        /// <summary>
        /// Test an instance of VaaiApi
        /// </summary>
        [Test]
        public void InstanceTest()
        {
            // TODO uncomment below to test 'IsInstanceOfType' VaaiApi
            //Assert.IsInstanceOfType(typeof(VaaiApi), instance, "instance is a VaaiApi");
        }

        
        /// <summary>
        /// Test OpVaaiVmCloneJobJobidGetStatus
        /// </summary>
        [Test]
        public void OpVaaiVmCloneJobJobidGetStatusTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //string jobid = null;
            //string acceptLanguage = null;
            //var response = instance.OpVaaiVmCloneJobJobidGetStatus(jobid, acceptLanguage);
            //Assert.IsInstanceOf<Job> (response, "response is Job");
        }
        
        /// <summary>
        /// Test OpVaaiVmCloneJobPost
        /// </summary>
        [Test]
        public void OpVaaiVmCloneJobPostTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //VmCloneSpec body = null;
            //string acceptLanguage = null;
            //var response = instance.OpVaaiVmCloneJobPost(body, acceptLanguage);
            //Assert.IsInstanceOf<Job> (response, "response is Job");
        }
        
        /// <summary>
        /// Test OpVaaiVmCloneJobsGet
        /// </summary>
        [Test]
        public void OpVaaiVmCloneJobsGetTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //int? maxJobs = null;
            //long? maxTimeDiffMinutes = null;
            //var response = instance.OpVaaiVmCloneJobsGet(maxJobs, maxTimeDiffMinutes);
            //Assert.IsInstanceOf<List<Job>> (response, "response is List<Job>");
        }
        
    }

}
