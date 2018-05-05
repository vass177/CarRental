﻿// <copyright file="FakeDatabase.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Test_BL.FakedTest.Fakes
{
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Dummy enum for test purposes
    /// </summary>
    public enum FakeAttributeEnum
    {
        Type1,
        Type2,
        Type3
    }

    /// <summary>
    /// Simple generic database model
    /// All methods except Update are implemented
    /// with somewhat meaningful logic
    /// <type name="T">T is an EntityFramework generated class type</type>
    /// </summary>
    internal class FakeDatabase<T> : IFakeDataBase<T>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FakeDatabase{T}"/> class.
        /// Constructor for dummy database handler for testing purposes
        /// </summary>
        public FakeDatabase()
        {
            this.Objects = new List<T>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FakeDatabase{T}"/> class.
        /// Constructor for FakeDatabase,gets an IEnumerable argument
        /// and sets corresponding field to it
        /// </summary>
        /// <param name="mycollection">IEnumerable type</param>
        public FakeDatabase(IEnumerable<T> mycollection)
        {
            this.Objects = (IList<T>)mycollection;
        }

        public IList<T> Objects { get; private set; } = new List<T>();

        public IList<T> DeletedObjects { get; private set; } = new List<T>();

        public IList<T> SelectedObjects { get; private set; } = new List<T>();

        public IList<T> InsertedObjects { get; private set; } = new List<T>();

        /// <summary>
        /// Adds argument to the DeletedObjects (class property) collecton 
        /// </summary>
        /// <param name="deletableItem">object to be deleted from the collection</param>
        public void Delete(object deletableItem)
        {
            this.DeletedObjects.Add((T)deletableItem);
        }

        /// <summary>
        /// Returns all elements of Objects class property
        /// </summary>
        /// <returns>the whole Objects property</returns>
        public object GetAll()
        {
            return this.Objects;
        }

        /// <summary>
        /// Adds argument to InsertedObjects collection
        /// </summary>
        /// <param name="newItem">object, will be casted and added to InsertedObjects collectons</param>
        public void Insert(object newItem)
        {
            this.InsertedObjects.Add((T)newItem);
        }

        /// <summary>
        /// It gives back first element of the data collection
        /// No matter what it adds second argument to SelectedObjects collection
        /// </summary>
        /// <param name="attributeType">will be casted as FakeAttributeEnum</param>
        /// <param name="attributeValue">can be anything</param>
        /// <returns>object, returns first element of the Objects collection or returns null</returns>
        public object Select(object attributeType, object attributeValue)
        {
            this.SelectedObjects.Add((T)attributeValue);
            if ((FakeAttributeEnum)attributeType == (FakeAttributeEnum)0)
            {
                return this.Objects.First();
            }

            return null;
        }

        /// <summary>
        /// Returns all element of the IEnumerable Object
        /// </summary>
        /// <param name="attributeType">a FakeAttibuteEnum value</param>
        /// <param name="attributeValue">can be anythig</param>
        /// <returns>the whole Objects property or null</returns>
        public object SelectMore(object attributeType, object attributeValue)
        {
            if ((FakeAttributeEnum)attributeType == FakeAttributeEnum.Type1 ||
                (FakeAttributeEnum)attributeType == FakeAttributeEnum.Type2)
            {
                return this.Objects;
            }

            return null;
        }

        /// <summary>
        /// Does nothing
        /// </summary>
        public void Update()
        {
        }
    }
}
