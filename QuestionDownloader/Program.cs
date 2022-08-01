using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Xml.Serialization;

namespace QuestionDownloader
{
    class Program
    {
        public static List<string> codes = new List<string> { 
            "326CABA7-0281-4FB9-99BE-6E4FC9C24BE8",
            "E5D0E8AA-45CD-4FE1-BA99-664AC1D044B7",
            "31FA2DCA-749F-43A7-88C8-310651258B29",
            "FA756C8A-C710-47F5-9B2C-920CEDF1869F",
            "375FA177-C6AF-4F8D-8AC2-C0770B488C2F",
            "D411CC4A-6BD1-4ED3-A7FA-1D611B730033",
            "0A99C14A-E226-4894-BA5D-C82C86EA366A",
            "98B95333-96A8-40DC-9EC2-7D80AE8CF01E",
            "C0A5FB71-5919-4383-A680-2E9BB9D5B4EB",
            "47ECBF50-8F33-4897-80DE-84A38EB62810",
            "B66416DF-4BE3-48C2-AFA5-9C55A7D93CF1",
            "458AE924-A7CF-4A65-8842-6EBFE9C81F9B",
            "72826FBE-7858-4EFA-8674-184819D44915",
            "A2C8182B-A99C-4266-BF3E-706F09BBC1BF",
            "2167C304-7434-4336-B264-55B7983896C2"
        };

        static void Main(string[] args)
        {
            var serializer = new XmlSerializer(typeof(Xml2CSharp.AssessmentItem));
            var questions = new Dictionary<string, string>();
            for (var part = 1; part <= 15; part++)
            {
                int qnum = 1;
                while (qnum < 1000)
                {
                    try
                    {
                        var urlPattern = BuildQuestionUrl(part, qnum);
                        var questionData = DownloadXml(urlPattern);
                        questions.Add(part + "." + qnum, questionData);
                    }
                    catch (ApplicationException)
                    {
                        Console.WriteLine($"{part.ToString("00")}: failed on {qnum.ToString("000")}");
                        break;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error {part.ToString("00")}{qnum.ToString("000")}: {ex}");
                    }

                    qnum++;
                }
            }
            Console.WriteLine("Continue? Y or N");

            var state = Console.ReadLine();
            if (state != "Y")
                return;

            using (var stream = new MemoryStream())
            using (var writer = new StreamWriter(stream, Encoding.UTF8))
            {
                writer.WriteLine("");

                foreach (var questionData in questions)
                {
                    try
                    {
                        WriteQuestionParagraph(serializer, writer, questionData);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error {questionData.Key}: {ex}");
                    }
                }

                writer.Flush();
                stream.Position = 0;
                var fileCsv = @$"{Environment.CurrentDirectory}\generated_{DateTime.Now.Ticks}.csv";
                using (var file = File.Create(fileCsv))
                {
                    stream.CopyTo(file);
                }
            }
        }

        private static string BuildQuestionUrl(int part, int qnum)
        {
            var grpid = part == 2 ? "2(1)" : part.ToString();
            var urlPattern = $"https:///Resource/DL_RES_{codes[part - 1]}/Gruppa{grpid}/_Vopros___8470_{part.ToString("00")}{qnum.ToString("000")}._.../Question.xml";
            if (part == 15)
            {
                urlPattern = $"https:///Resource/DL_RES_2167C304-7434-4336-B264-55B7983896C2/Gruppa1/_Vopros___8470_01{qnum.ToString("000")}._.../Question.xml";
            }

            return urlPattern;
        }

        private static void WriteQuestionParagraph(XmlSerializer serializer, StreamWriter writer, KeyValuePair<string, string> questionData)
        {
            var question = serializer.Deserialize(new StringReader(questionData.Value)) as Xml2CSharp.AssessmentItem;

            var data = string.Join(",", "", questionData.Key, "\"" + question.ItemBody.Text.Replace("\"", "\"\"") + "\"");
            writer.WriteLine(data);


            foreach (var choice in question.ItemBody.ChoiceInteraction.SimpleChoice)
            {
                var correct = choice.Identifier == question.ResponseDeclaration.CorrectResponse.Value;
                data = string.Join(",", correct ? "+" : "-", question.ItemBody.ChoiceInteraction.SimpleChoice.IndexOf(choice) + 1, "\"" + choice.Text.Replace("\"", "\"\"") + "\"");
                writer.WriteLine(data);
            }

            writer.WriteLine("");
        }

        private static string DownloadXml(string url)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Cookie", "JSESSIONID=node0765.node0");

                using (var response = client.GetAsync(url).Result)
                {
                    return ValidateResponse(response);
                }
            }
        }

        private static string ValidateResponse(HttpResponseMessage response)
        {
            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException((int)response.StatusCode + ": " + response.ToString());
            }

            return response.Content.ReadAsStringAsync().Result;
        }
    }
}
