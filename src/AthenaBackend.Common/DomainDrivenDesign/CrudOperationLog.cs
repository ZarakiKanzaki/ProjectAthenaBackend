using System;
using System.Collections.Generic;

namespace AthenaBackend.Common.DomainDrivenDesign
{
    public class CrudOperationLog : ValueObject
    {
        public CrudOperationLog(Guid userCreatedId)
        {
            Creation = new OperationLog(userCreatedId);
        }

        public CrudOperationLog(OperationLog creation, Guid userUpdatedId)
        {
            Creation = creation;
            Update = new OperationLog(userUpdatedId);
        }

        public CrudOperationLog(OperationLog creation, OperationLog update, Guid userDeletedId)
        {
            var deletionLog = new OperationLog(userDeletedId);

            Creation = creation;
            Update = update ?? deletionLog;
            Deletion = deletionLog;
        }

        public OperationLog Creation { get; private set; }
        public OperationLog Update { get; private set; }
        public OperationLog Deletion { get; private set; }

        public CrudOperationLog UpdateOperation(Guid userUpdatedId) => new(Creation, userUpdatedId);
        public CrudOperationLog DeleteOperation(Guid userDeletedId) => new(Creation, Update, userDeletedId);

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Creation;
            yield return Update;
            yield return Deletion;
        }
    }
}