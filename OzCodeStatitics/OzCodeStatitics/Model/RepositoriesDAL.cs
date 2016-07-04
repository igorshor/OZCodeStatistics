using System;
using System.Collections.Generic;

namespace OzCodeStatitics.Model
{
    public class RepositoriesDAL
    {
        private IDataAccessLeyer _dal;

        public RepositoriesDAL(IDataAccessLeyer dal)
        {
            this._dal = dal;
        }

        public RepositoryModel AddNew()
        {
            throw new NotImplementedException();
        }

        public void Add(RepositoryModel item)
        {
            throw new NotImplementedException();
        }

        public void Get(RepositoryModel item)
        {
            throw new NotImplementedException();
        }

        public void AddAll(List<RepositoryModel> item)
        {
            throw new NotImplementedException();
        }
    }
}

