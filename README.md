# CsvWriter

    using (MemoryStream memoryStream = new MemoryStream())
    {
      using (Writer writer = new Writer(memoryStream))
      {
          writer.Write("1");
          writer.Write("2");
          
          // 1,2
          string result = writer.ToString();
      }
    }
