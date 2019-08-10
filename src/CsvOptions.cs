using System;

namespace CsvWriter
{
  public struct CsvOptions
  {
    public CsvOptions(string columnDelimiter, string rowDelimiter, string escapeRowDelimiter, bool quote)
    {
      ColumnDelimiter = columnDelimiter;
      RowDelimiter = rowDelimiter;
      EscapeRowDelimiter = escapeRowDelimiter;
      Quote = quote;
    }

    public static CsvOptions DefaultOptions = new CsvOptions(",", Environment.NewLine, " ", false);

    public readonly string ColumnDelimiter;

    public readonly string RowDelimiter;

    /// <summary>
    /// Escape string to use when the character used as <see cref="RowDelimiter"/> is found in values.
    /// </summary>
    public readonly string EscapeRowDelimiter;

    /// <summary>
    /// Whether to quote numbers or not to counter the leading zeros issue in excel.
    /// </summary>
    public readonly bool Quote;
  }
}