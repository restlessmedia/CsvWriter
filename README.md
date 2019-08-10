# CsvWriter

[![Image of Yaktocat](https://ci.appveyor.com/api/projects/status/4d4g7701sr840uae/branch/master?svg=true
)](https://ci.appveyor.com/project/restlessmedia/csvwriter)



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
