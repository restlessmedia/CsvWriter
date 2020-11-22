using System;
using System.IO;

namespace CsvWriter
{
  public class Writer : IDisposable
  {
    public Writer(Stream stream, CsvOptions options)
    {
      _streamWriter = new StreamWriter(stream)
      {
        AutoFlush = true,
      };
      _options = options;
    }

    /// <summary>
    /// Initialises instance with default options.
    /// </summary>
    /// <param name="stream"></param>
    public Writer(Stream stream)
      : this(stream, CsvOptions.DefaultOptions) { }

    /// <summary>
    /// Writes the values.
    /// </summary>
    /// <param name="values"></param>
    public void Write(params object[] values)
    {
      foreach (object value in values)
      {
        if (
          _lastValue != null
          // not if the caller is explicitly writing a row delimiter (new line)
          && !_options.RowDelimiter.Equals(value)
          // not if the previously written value was a column delimiter
          && !_options.ColumnDelimiter.Equals(_lastValue))
        {
          Write(_options.ColumnDelimiter);
        }

        Write(value);
      }
    }

    /// <summary>
    /// Writes the values proceeded by the row delimiter.
    /// </summary>
    /// <param name="values"></param>
    public void WriteRow(params object[] values)
    {
      Write(values);
      Write(_options.RowDelimiter);
    }

    public void Dispose()
    {
      _streamWriter.Dispose();
    }

    /// <summary>
    /// Returns the entire string.
    /// </summary>
    /// <returns></returns>
    public override string ToString()
    {
      _streamWriter.BaseStream.Position = 0;
      return new StreamReader(_streamWriter.BaseStream).ReadToEnd();
    }

    private void Write(object value)
    {
      if (value == null)
      {
        value = string.Empty;
      }

      if (_options.RowDelimiter.Equals(value))
      {
        // when writing a new line, forget what was previously written
        _lastValue = null;
      }
      else
      {
        if (value != null)
        {
          string stringValue = value.ToString();

          if (_options.EscapeRowDelimiter != null)
          {
            stringValue = stringValue.Replace(_options.RowDelimiter, _options.EscapeRowDelimiter);
          }

          if (_options.Quote)
          {
            stringValue = string.Concat("\"", stringValue, "\"");
          }

          value = stringValue;
        }

        // remember the last column value written
        _lastValue = value;
      }

      _streamWriter.Write(value);
    }

    /// <summary>
    /// Used for tracking what was previously written in the last column
    /// </summary>
    private object _lastValue;

    private readonly StreamWriter _streamWriter;

    private readonly CsvOptions _options;
  }
}