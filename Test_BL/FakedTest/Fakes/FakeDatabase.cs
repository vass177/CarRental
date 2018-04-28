﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.DataHandling;

namespace Test_BL.FakedTest.Fakes
{
    /// <summary>
    /// Dummy enum for test purposes
    /// </summary>
    public enum FakeAttributeEnum
    {
        Type1,
        Type2
    }
    
    /// <summary>
    /// Simple generic database model
    /// All methods are implemented
    /// with somewhat meaningful logic 
    /// </summary>
    internal class FakeDatabase<T> : IDataBase
    {
        public IEnumerable<T> Objects { get; private set; }
        public IList<T> DeletedObjects { get;  private set;}
        public IList<T> SelectedObjects { get; private set; }
        public IList<T> InsertedObjects { get; private set; }
        /// <summary>
        /// Constructor for FakeDatabase,gets an IEnumerable argument
        /// and sets corresponding field to it
        /// </summary>
        /// <param name="mycollection">IEnumerable type</param>
        public FakeDatabase(IEnumerable<T> mycollection)
        {
            Objects = mycollection;
        }
        
        /// <summary>
        /// Adds argument to the DeletedObjects (class property) collecton 
        /// </summary>
        /// <param name="deletableItem">object to be deleted from the collection</param>
        public void Delete(object deletableItem)
        {
            DeletedObjects.Add((T)deletableItem);
        }
        
        /// <summary>
        /// Returns all elements of Objects class property
        /// </summary>
        /// <returns>IEnumerable</returns>
        public object GetAll()
        {
            return Objects;
        }
        
        /// <summary>
        /// Does nothing in this test environment
        /// </summary>
        /// <param name="newItem"></param>
        public void Insert(object newItem)
        {
            
        }
        
        /// <summary>
        /// It gives back first element of the dummy data collection
        /// if first argumentz of the method is a FakeAttibuteEnum type 
        /// </summary>
        /// <param name="attributeType">a FakeAttibuteEnum value</param>
        /// <param name="attributeValue">can be anything</param>
        /// <returns></returns>
        public object Select(object attributeType, object attributeValue)
        {
            if ((FakeAttributeEnum)attributeType == FakeAttributeEnum.Type1 ||
                (FakeAttributeEnum)attributeType == FakeAttributeEnum.Type2)
            {
                return Objects.First();
            }
            return null;
        }
        
        /// <summary>
        /// Returns all element of the IEnumerable Object
        /// </summary>
        /// <param name="attributeType">a FakeAttibuteEnum value</param>
        /// <param name="attributeValue">can be anythig</param>
        /// <returns></returns>
        public object SelectMore(object attributeType, object attributeValue)
        {
            if ((FakeAttributeEnum)attributeType == FakeAttributeEnum.Type1 ||
                (FakeAttributeEnum)attributeType == FakeAttributeEnum.Type2)
            {
                return Objects.ToList();
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
