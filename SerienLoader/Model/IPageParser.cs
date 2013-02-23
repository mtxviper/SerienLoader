namespace SerienLoader.Model
{
    public interface IPageParser
    {
        string Name { get; set; }
        void AddUrls(Episode episode);
    }
}