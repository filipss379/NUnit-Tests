using Loans.Domain.Applications;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Loan.Tests
{
    [Category("Product Comparison")]
    public class ProductComparerShould
    {

        private List<LoanProduct> products;
        private ProductComparer sut;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            //uruchamiane raz, przed wykonaniem pierwszej metody testowej
            // zakłąadamy, że dane tutaj nie moga być modyfikowane przez żaden test, może to popsuć pozostałe  testy

            products = new List<LoanProduct>
            {
                new LoanProduct(1, "a", 1),
                new LoanProduct(2, "b", 2),
                new LoanProduct(3, "c", 3)
            };
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            // podobnie jak one time setup uruchamia się raz na klasę, po zakończeniu ostatniego testu
        }

        [SetUp]
        public void Setup()
        {
            // uruchamiane przed wykonaniem każdej metody testowej

            sut = new ProductComparer(new LoanAmount("USD", 200_000m), products);
        }

        [TearDown]
        public void TearDown()
        {
            // Runs after each test executes
            // sut.Dispose()
        }

        [Test]
        public void ReturnCorrectNumberOfComparisons()
        {
            List<MonthlyRepaymentComparison> comparisons =
                sut.CompareMonthlyRepayments(new LoanTerm(30));

            Assert.That(comparisons, Has.Exactly(3).Items);
        }

        [Test]
        public void NotReturnDuplicateComparisons()
        {
            List<MonthlyRepaymentComparison> comparisons =
                sut.CompareMonthlyRepayments(new LoanTerm(30));

            Assert.That(comparisons, Is.Unique);
        }

        [Test]
        public void ReturnComparisonForFirstProduct()
        {
            List<MonthlyRepaymentComparison> comparisons =
                sut.CompareMonthlyRepayments(new LoanTerm(30));

            var exptectedProduct = new MonthlyRepaymentComparison("a", 1, 643.28m);
            Assert.That(comparisons, Does.Contain(exptectedProduct));
        }

        [Test]
        public void ReturnComparisonForFirstProductWithpartialKnownExpectedValues()
        {
            List<MonthlyRepaymentComparison> comparisons =
                sut.CompareMonthlyRepayments(new LoanTerm(30));

            Assert.That(comparisons, Has.Exactly(1)
                                    .Matches<MonthlyRepaymentComparison>(
                                    item => item.ProductName == "a" &&
                                    item.InterestRate == 1 &&
                                    item.MonthlyRepayment > 0));
        }
    }
}
