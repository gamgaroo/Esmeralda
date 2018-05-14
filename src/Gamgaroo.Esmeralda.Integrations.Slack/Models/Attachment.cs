namespace Gamgaroo.Esmeralda.Integrations.Slack.Models
{
    public sealed class Attachment
    {
        public string Color { get; set; }
        public string Title { get; set; }
        public string Title_Url { get; set; }
        public string Text { get; set; }
        public Field[] Fields { get; set; }

        public class Field
        {
            public Field(string title, object value, bool isShort = true)
            {
                Title = title;
                Value = value.ToString();
                Short = isShort;
            }

            public string Title { get; set; }
            public string Value { get; set; }
            public bool Short { get; set; }
        }
    }
}