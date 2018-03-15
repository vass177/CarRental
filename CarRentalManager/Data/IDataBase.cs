// <copyright file="IDataBase.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace CarRentalManager.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public interface IDataBase
    {
        void Insert(object newItem);

        void Delete(object deletableItem);

        void Update();

        object Select(object attributeType, object attributeValue);

        object SelectMore(object attributeType, object attributeValue);
    }
}
