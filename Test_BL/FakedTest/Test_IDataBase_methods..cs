﻿// <copyright file="Test_IDataBase_methods..cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Test_BL.FakedTest
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Data.DataHandling;
    using NUnit.Framework;
    using Test_BL.FakedTest.Fakes;

    /// <summary>
    /// Testing IDataBase implementor FakeDatabase
    /// </summary>
    [TestFixture]
    public class Test_IDataBase_methods
    {
        private static int id = 0;
        private static Random rnd = new Random();

        private List<Data.Car> myCarDataSet;
        private FakeDatabase<Data.Car> myFCarDb;

        private List<Data.Client> myClientDataSet;
        private FakeDatabase<Data.Client> myFClientDb;

        private List<Data.Rental> myRentalDataSet;
        private FakeDatabase<Data.Rental> myFRentalDb;

        private List<Data.Service> myServiceDataSet;
        private FakeDatabase<Data.Service> myFServiceDb;

        public static IEnumerable<object[]> DummyDataSetTestCases
        {
            get
            {
                List<Data.Car> carDS = new List<Data.Car>();
                List<Data.Client> clientDS = new List<Data.Client>();
                List<Data.Rental> rentalDS = new List<Data.Rental>();
                List<Data.Service> serviceDS = new List<Data.Service>();
                for (int i = 0; i < 3; i++)
                {
                    carDS.Add(new Data.Car
                    {
                        CarID = i,
                        CarType = "Cartype_" + i
                    });
                    clientDS.Add(new Data.Client
                    {
                        UserName = "username_" + i
                    });
                    rentalDS.Add(new Data.Rental
                    {
                        RentalID = i,
                        UserName = "username_" + i
                    });
                    serviceDS.Add(new Data.Service
                    {
                        ServiceName = "service_" + i++
                    });
                }

                FakeDatabase<Data.Car> carFDb = new FakeDatabase<Data.Car>(carDS);
                FakeDatabase<Data.Client> clientFDb = new FakeDatabase<Data.Client>(clientDS);
                FakeDatabase<Data.Rental> rentalFDb = new FakeDatabase<Data.Rental>(rentalDS);
                FakeDatabase<Data.Service> serviceFDb = new FakeDatabase<Data.Service>(serviceDS);

                List<object[]> testCases = new List<object[]>();
                testCases.Add(new object[] { carFDb, carDS });
                testCases.Add(new object[] { clientFDb, clientDS });
                testCases.Add(new object[] { rentalFDb, rentalDS });
                testCases.Add(new object[] { serviceFDb, serviceDS });
                return testCases;
            }
        }

        public static IEnumerable<object[]> DummyDataTestCases
        {
            get
            {
                IFakeDataBase<Data.Car> carFDb = new FakeDatabase<Data.Car>();
                IFakeDataBase<Data.Client> clientFDb = new FakeDatabase<Data.Client>();
                IFakeDataBase<Data.Rental> rentalFDb = new FakeDatabase<Data.Rental>();
                IFakeDataBase<Data.Service> serviceFDb = new FakeDatabase<Data.Service>();
                Data.Car c = new Data.Car { CarType = "TestCar" };
                Data.Client cl = new Data.Client { UserName = "TestName" };
                Data.Rental r = new Data.Rental { UserName = "TestName" };
                Data.Service s = new Data.Service { ServiceName = "TestService" };

                List<object[]> testCases = new List<object[]>();
                testCases.Add(new object[] { carFDb, c, c });
                testCases.Add(new object[] { clientFDb, cl, cl });
                testCases.Add(new object[] { rentalFDb, r, r });
                testCases.Add(new object[] { serviceFDb, s, s });
                return testCases;
            }
        }

        public Data.Car MyCar { get; set; }

        /// <summary>
        /// Valamiért nem elérhetőek a többi metódusból
        /// az itt beállított objektumok
        /// </summary>
        [OneTimeSetUp]
        public void SetUp()
        {
            this.myCarDataSet = new List<Data.Car>();
            this.myClientDataSet = new List<Data.Client>();
            this.myRentalDataSet = new List<Data.Rental>();
            this.myServiceDataSet = new List<Data.Service>();
            for (int i = 0; i < 5; i++)
            {
                Data.Car c = new Data.Car();
                c.CarID = id;
                c.CarType = "CarName" + c.CarID;
                this.myCarDataSet.Add(c);
                Data.Client cl = new Data.Client();
                cl.UserName = "username" + id;
                this.myClientDataSet.Add(cl);
                Data.Rental r = new Data.Rental();
                r.RentalID = id;
                r.UserName = "username" + r.RentalID;
                this.myRentalDataSet.Add(r);
                Data.Service s = new Data.Service();
                s.ServiceName = "Service_"+id++;
                this.myServiceDataSet.Add(s);
            }

            this.myFCarDb = new FakeDatabase<Data.Car>(this.myCarDataSet);
            this.myFClientDb = new FakeDatabase<Data.Client>(this.myClientDataSet);
            this.myFRentalDb = new FakeDatabase<Data.Rental>(this.myRentalDataSet);
            this.myFServiceDb = new FakeDatabase<Data.Service>(this.myServiceDataSet);
        }

        /// <summary>
        /// simple method test for all kind of fake datasets  
        /// </summary>
        /// <param name="input">IDataBase implementor FakeDatabase classes to be tested</param>
        /// <param name="expected">object list returned</param>
        [TestCaseSource("DummyDataSetTestCases")]
        public void GetAll_method_test<T>(IDataBase input, List<T> expected)
        {
            // ARRANGE+ACT+ASSERT
            Assert.That(input.GetAll(), Is.EqualTo(expected));
        }

        /// <summary>
        /// Tests insert method of a FakeDatabase object
        /// that is an IDataBase implementor
        /// </summary>
        [Test]
        public void WhenInsertedCar_ListContainsIt()
        {
            // ARRANGE
            Data.Car c = new Data.Car
            {
                CarID = int.MaxValue,
                CarCapacity = 4,
                CarType = "TestCar",
            };

            // ACT
            this.myFCarDb.Insert(c);

            // ASSERT
            Assert.That(this.myFCarDb.InsertedObjects.Contains(c));
        }

        /// <summary>
        /// Inserts an object and tests whether the inserted object occurs in the  InsertedObject list
        /// </summary>
        /// <typeparam name="T">An EntityFramework created data class</typeparam>
        /// <param name="fakeDB">IFakeDataBase<T> dummy interface reference for testing</param>
        /// <param name="input">An object to be inserted</param>
        /// <param name="expected">an object to be expected</param>
        [TestCaseSource("DummyDataTestCases")]
        public void Insert_method_test<T>(IFakeDataBase<T> fakeDB, T input, T expected)
        {
            // ARRANGE+ACT
            fakeDB.Insert(input);

            // ASSERT
            Assert.That(expected, Is.EqualTo(fakeDB.InsertedObjects.First()));
        }

        /// <summary>
        /// Inserts an object 2 times and tests if IFakeDataBase implementor 
        /// InsertedObject property has 2 elements
        /// </summary>
        /// <typeparam name="T">T an EntityFramwork generated object</typeparam>
        /// <param name="fakeDB">a IFakeDataBase implementor</param>
        /// <param name="input">an Entity Framework object inserted</param>
        /// <param name="expected">an EF object to bechecked</param>
        [TestCaseSource("DummyDataTestCases")]
        public void Insert_method_test2<T>(IFakeDataBase<T> fakeDB, T input, T expected)
        {
            // ARRANGE+ACT
            fakeDB.Insert(input);
            fakeDB.Insert(input);

            // ASSERT
            Assert.That(fakeDB.InsertedObjects.Count==2);
        }

        [TestCaseSource("DummyDataTestCases")]
        public void Delete_method_test<T>(IFakeDataBase<T> fakeDB, T input, T expected)
        {
            // ARRANGE+ACT
            fakeDB.Delete(input);

            // ASSERT
            Assert.That(expected, Is.EqualTo(fakeDB.DeletedObjects.Last()));
        }

        /// <summary>
        /// Tests Select method using enum value in the method's
        /// parameter list
        /// </summary>
        /// <typeparam name="T">an Entity Framework object</typeparam>
        /// <param name="fakeDB">FakeDatabBase<T>, fake database for method testing</param>
        /// <param name="inputDS">List<T>, fake dataset to be tested</param>
        [TestCaseSource("DummyDataSetTestCases")]
        public void Select_method_test<T>(IFakeDataBase<T> fakeDB, List<T> inputDS)
        {
            // ARRANGE
            FakeAttributeEnum inputAttributeType = (FakeAttributeEnum)0;
            T inputAttributeValue = inputDS.Last();
            T expected = inputDS.First();

            // ACT
            fakeDB.Select(inputAttributeType,inputAttributeValue);

            // ASSERT
            Assert.That(expected, Is.EqualTo(fakeDB.Objects.First()));
            Assert.That(inputAttributeValue, Is.EqualTo(fakeDB.SelectedObjects.First()));
        }

        [TestCaseSource("DummyDataSetTestCases")]
        public void SelectMore_method_test<T>(IFakeDataBase<T> fakeDB, List<T> inputDS)
        {
            // ARRANGE
            FakeAttributeEnum inputAttributeType = FakeAttributeEnum.Type2;
            T inputAttributeValue = inputDS.Last();
            List<T> expected = inputDS;

            // ACT
            fakeDB.SelectMore(inputAttributeType, inputAttributeValue);

            // ASSERT
            Assert.That(expected, Is.EqualTo(fakeDB.Objects));
        }
    }
}
