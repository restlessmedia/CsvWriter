using restlessmedia.Test;
using System;
using System.IO;
using Xunit;

namespace CsvWriter.UnitTest
{
  public class WriterWithDefaultOptionsTests
  {
    [Fact]
    public void Write_writes_values()
    {
      using (Writer writer = CreateInstance(out _))
      {
        writer.Write("1");
        writer.Write("2");
        writer.ToString().MustBe("1,2");
      }
    }

    [Fact]
    public void Write_writes_values_after_outputting_tostring()
    {
      using (Writer writer = CreateInstance(out _))
      {
        writer.Write("1");
        writer.Write("2");
        writer.ToString().MustBe("1,2");
        writer.Write("3");
        writer.ToString().MustBe("1,2,3");
      }
    }

    [Fact]
    public void WriteRow_writes_rows()
    {
      using (Writer writer = CreateInstance(out _))
      {
        writer.WriteRow("1", "2", "3");
        writer.WriteRow("4", "5", "6");
        writer.ToString().MustBe("1,2,3\r\n4,5,6\r\n");
      }
    }

    [Fact]
    public void Write_supports_manual_row_writing()
    {
      using (Writer writer = CreateInstance(out _))
      {
        writer.Write("1", "2", "3");
        writer.Write(Environment.NewLine);
        writer.Write("4", "5", "6");
        writer.ToString().MustBe("1,2,3\r\n4,5,6");
      }
    }

    [Fact]
    public void handles_null_values()
    {
      using (Writer writer = CreateInstance(out _))
      {
        writer.Write("1", null, "3");
        writer.ToString().MustBe("1,,3");
      }
    }

    private Writer CreateInstance(out MemoryStream memoryStream)
    {
      memoryStream = new MemoryStream();
      return new Writer(memoryStream);
    }
  }
}
