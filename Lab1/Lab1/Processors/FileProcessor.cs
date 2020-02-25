using System;
using System.Collections.Generic;
using CommandLine;
using CsvHelper;
using CsvHelper.TypeConversion;
using Lab1.Business;
using Lab1.CommandLineOptions;
using Lab1.Domain;
using Lab1.Logger;
using Lab1.Services;

namespace Lab1.Processing
{
    internal class FileProcessor
    {
        public void Process(string[] args)
        {
            var logger = Log4Net.Log;
            Log4Net.InitLogger();

            string inputPath = string.Empty, outputPath = string.Empty;
            FileMode mode = FileMode.Json;

            Parser.Default.ParseArguments<Options>(args)
                   .WithParsed(o =>
                   {
                       inputPath = o.InputFile;
                       outputPath = o.OutputFile;
                       mode = o.Mode;
                   });

            var students = new List<Student>();

            try
            {
                students = (List<Student>)Services.CsvReader.Read(inputPath);
            }
            catch (FormatException e)
            {
                logger.Error(e.Message);
                return;
            }
            catch (ArgumentException e)
            {
                logger.Error(e.Message);
                return;
            }
            catch (ReaderException e)
            {
                logger.Error(e.Message);
                return;
            }
            catch (TypeConverterException e)
            {
                logger.Error(e.Message);
                return;
            }
            catch (System.IO.FileNotFoundException e)
            {
                logger.Error(e.Message);
                return;
            }

            var finalReport = new FinalReport
            {
                StudentResults = students.GetStudentsResult(),
                ExamResults = students.GetExamResults(),
                Average = students.GetGroupAverage(),
            };

            switch (mode)
            {
                case FileMode.Json:
                    var jsonWriter = new JsonWriter();
                    jsonWriter.Write(outputPath, finalReport);
                    break;
                case FileMode.Excel:
                    var excelWriter = new ExcelWriter();
                    excelWriter.Write(outputPath, finalReport);
                    break;
                default:
                    break;
            }
        }
    }
}
