namespace Domain.Entities
{
    public class MediaUrl : Entity
    {
        public string Url { get; private set; }
        public MediaUrl()
        {

        }
        public MediaUrl(string url)
        {
            Url = url;
        }
    }
}