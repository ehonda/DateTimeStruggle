using System.Globalization;
using FluentAssertions;

namespace DateTimeStruggle.Tests;

public class DateTimeTests
{
    [Test]
    public void Test1()
    {
        var utcNow = DateTime.UtcNow;
        utcNow.ToUniversalTime().Should().Be(utcNow);

        var now = DateTime.Now;
        now.ToUniversalTime().Should().BeWithin(TimeSpan.FromMinutes(1)).After(utcNow);
        utcNow.ToLocalTime().Should().BeWithin(TimeSpan.FromMinutes(1)).Before(now);

        var parsed = DateTime.Parse(now.ToString("O", CultureInfo.InvariantCulture), CultureInfo.InvariantCulture);
        parsed.ToUniversalTime().Should().BeWithin(TimeSpan.FromMinutes(1)).After(utcNow);
        parsed.ToLocalTime().Should().BeWithin(TimeSpan.FromMinutes(1)).After(now);
    }

    [Test]
    public void Test2()
    {
        var utcParsed = DateTime.Parse("2022-10-25T23:00:00Z");
        var utc = new DateTime(2022, 10, 25, 23, 00, 00, DateTimeKind.Utc);

        utc.Should().Be(utcParsed);
        utc.ToUniversalTime().Should().Be(utcParsed);
        utcParsed.ToUniversalTime().Should().Be(utc);
    }
}
