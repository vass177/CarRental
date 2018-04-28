﻿using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test_BL.FakedTest.Fakes;

namespace Test_BL.FakedTest
{
    /// <summary>
    /// Test methods of a fake class that implements IDataBase 
    /// </summary>
    [TestFixture]
    class Test_IDataBase_methods
    {
        private List<FakeClass> myFakeDataSet { get; set; }
        private FakeDatabase<List<FakeClass>> myFakeDatabase;
        [OneTimeSetUp]
        static void SetUp()
        {

        } 
    }
}
