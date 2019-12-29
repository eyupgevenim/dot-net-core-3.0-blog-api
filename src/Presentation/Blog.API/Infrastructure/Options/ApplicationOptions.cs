namespace Blog.API.Infrastructure.Options
{
    public class ApplicationOptions
    {
        public ConnectionStringsOptions ConnectionStrings { get; set; }

        public JwtOptions Jwt { get; set; }
    }
}
