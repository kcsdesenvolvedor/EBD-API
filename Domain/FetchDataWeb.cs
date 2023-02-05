using EBD.API.Models;
using HtmlAgilityPack;

namespace EBD.API.Domain
{
    public class FetchDataWeb : IFetchDataWeb
    {
        public async Task<List<Lesson>> FetchLessonsFromEBD()
        {
            var lessons = new List<Lesson>();
            var html = await GetHtml();
            var htmlDoc = new HtmlDocument();

            htmlDoc.LoadHtml(html);

            var lessonsElements = htmlDoc.DocumentNode.SelectNodes("//p/*");

            foreach (var lessonElement in lessonsElements)
            {
                if (lessonElement.Name == "a" && lessonElement.FirstChild.Name != "#text")
                {
                    var lessonSplit = lessonElement.InnerText.Split(":");
                    var lesson = new Lesson()
                    {
                        LessonNumber = lessonSplit[0],
                        LessonName = lessonSplit[1]
                    };

                    lessons.Add(lesson);
                    if (lessonSplit[0].Contains("13"))
                        break;
                }
                else if(lessonElement.Name == "strong")
                {
                    var lesson = new Lesson()
                    {
                        LessonNumber = lessonElement.InnerText,
                        LessonName = lessonElement.NextSibling.InnerText
                    };

                    lessons.Add(lesson);
                    if (lessonElement.InnerText.Contains("13"))
                        break;
                }
            }

            return lessons;
        }
        private async Task<string> GetHtml()
        {
            var client = new HttpClient();
            return await client.GetStringAsync("https://escolabiblicadominical.org/licoes-biblicas-ebd-betel/");
        }
    }
}
