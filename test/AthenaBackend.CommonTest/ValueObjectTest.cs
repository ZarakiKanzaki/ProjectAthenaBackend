using AthenaBackend.Common.DomainDrivenDesign;
using NUnit.Framework;
using Shouldly;
using System;

namespace AthenaBackend.CommonTest
{
    public class ValueObjectTest
    {

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Equal_TwoEqualValueObject_ShouldBeTrue()
        {
            var operationLogLeft = new OperationLog(Guid.NewGuid());
            var operationLogRight = operationLogLeft;

            Equals(operationLogRight, operationLogLeft).ShouldBeTrue();
            (operationLogRight == operationLogLeft).ShouldBeTrue();
            (operationLogRight != operationLogLeft).ShouldBeFalse();
        }

        [Test]
        public void Equal_TwoDifferentValueObject_ShouldBeFalse()
        {
            var operationLogLeft = new OperationLog(Guid.NewGuid());
            var operationLogRight = new OperationLog(Guid.NewGuid());

            Equals(operationLogRight, operationLogLeft).ShouldBeFalse();
            (operationLogRight == operationLogLeft).ShouldBeFalse();
            (operationLogRight != operationLogLeft).ShouldBeTrue();
        }

        [Test]
        public void Equal_oneIsNull_ShouldBeTrue()
        {
            var operationLogLeft = new OperationLog(Guid.NewGuid());
            OperationLog operationLogRight = null;

            Equals(operationLogRight, operationLogLeft).ShouldBeFalse();
            (operationLogRight == operationLogLeft).ShouldBeFalse();
            (operationLogRight != operationLogLeft).ShouldBeTrue();
        }
    }
}
