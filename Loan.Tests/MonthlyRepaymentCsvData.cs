

using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace Loan.Tests
{
    public class MonthlyRepaymentCsvData
    {
        public static IEnumerable GetTestCases(string csvFileName)
        {
            var csvLines = File.ReadAllLines(csvFileName);

            var testCases = new List<TestCaseData>();

            foreach(var line in csvLines)
            {
                string[] values = line.Replace(" ", "").Split(',');

                decimal principal = decimal.Parse(values[0], CultureInfo.InvariantCulture);
                decimal interestRate = decimal.Parse(values[1], CultureInfo.InvariantCulture);
                int termInYears = int.Parse(values[2]);
                decimal exptectedRepayment = decimal.Parse(values[3], CultureInfo.InvariantCulture);

                testCases.Add(new TestCaseData(principal, interestRate, termInYears, exptectedRepayment));
            }

            return testCases;
        }
    }
}
