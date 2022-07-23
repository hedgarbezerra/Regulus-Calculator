namespace Regulus.API.Modelos
{
    public enum Method
    {
        GET,
        POST,
        PUT,
        PATCH,
        DELETE
    }
    public class HateoasLink
    {
        public HateoasLink(string rel, Uri href, Method method)
        {
            Rel = rel;
            Href = href;
            Method = method;
        }
        public string Rel { get; set; }
        public Uri Href { get; set; }
        public Method Method { get; set; }
    }

    public class HateoasResult<T> where T : class
    {
        public T Data { get; set; }
        public List<HateoasLink> Links { get; set; }
        public HateoasResult()
        {
            Links = new List<HateoasLink>();
        }
        public HateoasResult(T data, List<HateoasLink> links)
        {
            Data = data;
            Links = links;
        }
        public HateoasResult(T data) : this()
        {
            Data = data;
        }
    }
}
