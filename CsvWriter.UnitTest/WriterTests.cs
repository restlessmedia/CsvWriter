using restlessmedia.Test;
using System;
using System.IO;
using Xunit;

namespace CsvWriter.UnitTest
{
  public class WriterTests
  {
    [Fact]
    public void Write_escapes_numbers()
    {
      using (Writer writer = CreateInstance(out _))
      {
        writer.Write("1");
        writer.ToString().MustBe("\"1\"");
      }
    }

    private Writer CreateInstance(out MemoryStream memoryStream)
    {
      memoryStream = new MemoryStream();
      return new Writer(memoryStream, new CsvOptions(",", Environment.NewLine, " ", true));
    }
  }
}
