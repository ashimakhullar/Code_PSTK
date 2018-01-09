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
    ///  Class for testing AboutApi
    /// </summary>
    /// <remarks>
    /// This file is automatically generated by Swagger Codegen.
    /// Please update the test case below to test the API endpoint.
    /// </remarks>
    [TestFixture]
    public class AboutApiTests
    {
        private AboutApi instance;

        /// <summary>
        /// Setup before each unit test
        /// </summary>
        [SetUp]
        public void Init()
        {
            instance = new AboutApi();
        }

        /// <summary>
        /// Clean up after each unit test
        /// </summary>
        [TearDown]
        public void Cleanup()
        {

        }

        /// <summary>
        /// Test an instance of AboutApi
        /// </summary>
        [Test]
        public void InstanceTest()
        {
            // TODO uncomment below to test 'IsInstanceOfType' AboutApi
            //Assert.IsInstanceOfType(typeof(AboutApi), instance, "instance is a AboutApi");
        }

        
        /// <summary>
        /// Test OpAboutGet
        /// </summary>
        [Test]
        public void OpAboutGetTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //string acceptLanguage = null;
            //var response = instance.OpAboutGet(acceptLanguage);
            //Assert.IsInstanceOf<AboutInfo> (response, "response is AboutInfo");
        }
        
    }

}
