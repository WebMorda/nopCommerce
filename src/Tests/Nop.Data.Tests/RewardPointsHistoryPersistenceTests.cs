﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nop.Core.Domain.Orders;
using NUnit.Framework;
using Nop.Tests;
using Nop.Core.Domain.Catalog;
using Nop.Core.Domain.Customers;

namespace Nop.Data.Tests
{
    [TestFixture]
    public class RewardPointsHistoryPersistenceTests : PersistenceTest
    {
        [Test]
        public void Can_save_and_load_rewardPointsHistory()
        {
            var rewardPointsHistory = new RewardPointsHistory()
            {
                Customer = GetTestCustomer(),
                Points = 1,
                Message = "Points for registration",
                PointsBalance = 2,
                UsedAmount = 3.1M,
                UsedAmountInCustomerCurrency = 4.1M,
                CustomerCurrencyCode = "USD",
                CreatedOnUtc = new DateTime(2010, 01, 01)
            };

            var fromDb = SaveAndLoadEntity(rewardPointsHistory);
            fromDb.ShouldNotBeNull();
            fromDb.Points.ShouldEqual(1);
            fromDb.Message.ShouldEqual("Points for registration");
            fromDb.PointsBalance.ShouldEqual(2);
            fromDb.UsedAmount.ShouldEqual(3.1M);
            fromDb.UsedAmountInCustomerCurrency.ShouldEqual(4.1M);
            fromDb.CustomerCurrencyCode.ShouldEqual("USD");
            fromDb.CreatedOnUtc.ShouldEqual(new DateTime(2010, 01, 01));

            fromDb.Customer.ShouldNotBeNull();
        }
        [Test]
        public void Can_save_and_load_rewardPointsHistory_with_order()
        {
            var rewardPointsHistory = new RewardPointsHistory()
            {
                Customer = GetTestCustomer(),
                UsedWithOrder = GetTestOrder(),
                Points = 1,
                Message = "Points for registration",
                PointsBalance = 2,
                UsedAmount = 3,
                UsedAmountInCustomerCurrency = 4,
                CustomerCurrencyCode = "USD",
                CreatedOnUtc = new DateTime(2010, 01, 01)
            };

            var fromDb = SaveAndLoadEntity(rewardPointsHistory);
            fromDb.ShouldNotBeNull();

            fromDb.UsedWithOrder.ShouldNotBeNull();
            fromDb.UsedWithOrder.CreatedOnUtc.ShouldEqual(new DateTime(2010, 01, 01));
        }
        
        protected Customer GetTestCustomer()
        {
            return new Customer
            {
                CustomerGuid = Guid.NewGuid(),
                AdminComment = "some comment here",
                Active = true,
                Deleted = false,
                CreatedOnUtc = new DateTime(2010, 01, 01)
            };
        }

        protected Order GetTestOrder()
        {
            return new Order
            {
                OrderGuid = Guid.NewGuid(),
                Customer = GetTestCustomer(),
                CreatedOnUtc = new DateTime(2010, 01, 01)
            };
        }
    }
}