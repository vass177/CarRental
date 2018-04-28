using System;
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
    /// All methods except Update are implemented
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
        /// <returns>the whole Objects property</returns>
        public object GetAll()
        {
            return Objects;
        }
        
        /// <summary>
        /// Adds argument to InsertedObjects collection
        /// </summary>
        /// <param name="newItem">object, will be casted and added to InsertedObjects collectons</param>
        public void Insert(object newItem)
        {
            InsertedObjects.Add((T)newItem);
        }
        
        /// <summary>
        /// It gives back first element of the dummy data collection
        /// if first argumentz of the method is a FakeAttibuteEnum type 
        /// No matter what it adds second argument to SelectedObjects collection
        /// </summary>
        /// <param name="attributeType">a FakeAttibuteEnum value</param>
        /// <param name="attributeValue">can be anything</param>
        /// <returns>object, returns first element of the Objects collection or returns null</returns>
        public object Select(object attributeType, object attributeValue)
        {
            SelectedObjects.Add((T)attributeValue);
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
        /// <returns>the whole Objects property or null</returns>
        public object SelectMore(object attributeType, object attributeValue)
        {
            if ((FakeAttributeEnum)attributeType == FakeAttributeEnum.Type1 ||
                (FakeAttributeEnum)attributeType == FakeAttributeEnum.Type2)
            {
                return Objects;
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
