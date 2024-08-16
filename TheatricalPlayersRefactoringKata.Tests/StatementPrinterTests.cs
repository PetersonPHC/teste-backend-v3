using System.Collections.Generic;
using ApprovalTests;
using ApprovalTests.Reporters;
using Xunit;
using System;
using System.Xml.Linq;

namespace TheatricalPlayersRefactoringKata.Tests;

public class StatementPrinterTests
{
    [Fact]
    [UseReporter(typeof(DiffReporter))]
    public void TestStatementExampleLegacy()
    {
        var plays = new Dictionary<string, Play>();
        plays.Add("hamlet", new Play("Hamlet", 4024, "tragedy"));
        plays.Add("as-like", new Play("As You Like It", 2670, "comedy"));
        plays.Add("othello", new Play("Othello", 3560, "tragedy"));

        Invoice invoice = new Invoice(
            "BigCo",
            new List<Performance>
            {
                new Performance("hamlet", 55),
                new Performance("as-like", 35),
                new Performance("othello", 40),
            }
        );

        StatementPrinter statementPrinter = new StatementPrinter();
        var result = statementPrinter.Print(invoice, plays);

        Approvals.Verify(result);
    }

    [Fact]
    [UseReporter(typeof(DiffReporter))]
    public void TestTextStatementExample()
    {
        var plays = new Dictionary<string, Play>();
        plays.Add("hamlet", new Play("Hamlet", 4024, "tragedy"));
        plays.Add("as-like", new Play("As You Like It", 2670, "comedy"));
        plays.Add("othello", new Play("Othello", 3560, "tragedy"));
        plays.Add("henry-v", new Play("Henry V", 3227, "history"));
        plays.Add("john", new Play("King John", 2648, "history"));
        plays.Add("richard-iii", new Play("Richard III", 3718, "history"));

        Invoice invoice = new Invoice(
            "BigCo",
            new List<Performance>
            {
                    new Performance("hamlet", 55),
                    new Performance("as-like", 35),
                    new Performance("othello", 40),
                    new Performance("henry-v", 20),
                    new Performance("john", 39),
                    new Performance("henry-v", 20)
            }
        );

        StatementPrinter statementPrinter = new StatementPrinter();
        var result = statementPrinter.Print(invoice, plays);

        Approvals.Verify(result);
    }

    [Fact]
    [UseReporter(typeof(DiffReporter))]
    public void TestXmlStatementExample()
    {
        var statement = new XDocument(
            new XDeclaration("1.0", "utf-8", null),

            new XElement("Statement",
                new XAttribute(XNamespace.Xmlns + "xsi", "http://www.w3.org/2001/XMLSchema-instance"),
                new XAttribute(XNamespace.Xmlns + "xsd", "http://www.w3.org/2001/XMLSchema"),
                new XElement("Customer", "BigCo"),
                new XElement("Items",
                    new XElement("Item",
                        new XElement("AmountOwed", 650),
                        new XElement("EarnedCredits", 25),
                        new XElement("Seats", 55)
                    ),
                    new XElement("Item",
                        new XElement("AmountOwed", 547),
                        new XElement("EarnedCredits", 12),
                        new XElement("Seats", 35)
                    ),
                    new XElement("Item",
                        new XElement("AmountOwed", 456),
                        new XElement("EarnedCredits", 10),
                        new XElement("Seats", 40)
                    ),
                    new XElement("Item",
                        new XElement("AmountOwed", 705.4),
                        new XElement("EarnedCredits", 0),
                        new XElement("Seats", 20)
                    ),
                    new XElement("Item",
                        new XElement("AmountOwed", 931.6),
                        new XElement("EarnedCredits", 9),
                        new XElement("Seats", 39)
                    ),
                    new XElement("Item",
                        new XElement("AmountOwed", 705.4),
                        new XElement("EarnedCredits", 0),
                        new XElement("Seats", 20)
                    )
                ),
                new XElement("AmountOwed", 3995.4),
                new XElement("EarnedCredits", 56)
            )
        );
        string xmlString = statement.Declaration + "\n" + statement.ToString(SaveOptions.None);
        Approvals.Verify(xmlString);
    }

}
