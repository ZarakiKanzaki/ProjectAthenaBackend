using System;
using System.Collections.Generic;

namespace AthenaBackend.Common.DomainDrivenDesign
{
    public class OperationLog : ValueObject
    {
        public OperationLog(Guid? userOperationId)
        {
            UserOperationId = userOperationId;
            UserOperationDateTime = DateTime.UtcNow;
        }

        public Guid? UserOperationId { get; private set; }
        public DateTime? UserOperationDateTime { get; private set; }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return UserOperationId;
            yield return UserOperationDateTime;
        }
    }
}