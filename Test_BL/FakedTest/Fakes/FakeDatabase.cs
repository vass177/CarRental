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
    /// Simple generic database model with dummy implemented methods for test purposes 
    /// </summary>
    internal class FakeDatabase<T> : IDataBase
    {
        public IEnumerable<T> Objects { get; set; }
        /// <summary>
        /// Constructor for FakeDatabase,gets one argument, and does nothing with that
        /// </summary>
        /// <param name="nothing">IEnumerable type, will be handled as if it would be null</param>
        public FakeDatabase(IEnumerable<T> nothing)
        {
            Objects = null;
        }

        public void Delete(object deletableItem)
        {
            throw new NotImplementedException();
        }

        public object GetAll()
        {
            throw new NotImplementedException();
        }

        public void Insert(object newItem)
        {
            
        }
        /// <summary>
        /// Test method, it gives back first element of the dummy data collection
        /// </summary>
        /// <param name="attributeType">a FakeAttibuteEnum value</param>
        /// <param name="attributeValue">can be anything</param>
        /// <returns></returns>
        public object Select(object attributeType, object attributeValue)
        {
            if ((FakeAttributeEnum)attributeType == FakeAttributeEnum.Type1)
            {
                return Objects.First();
            }
            return null;
        }

        public object SelectMore(object attributeType, object attributeValue)
        {
            
        }

        public void Update()
        {
            
        }
    }
}
