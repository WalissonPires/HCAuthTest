using HotChocolate.Authorization;

namespace HCAuthTest
{

    [Authorize(Policies.AssinaturaValida)]
    public class Book
    {
        public string Title { get; set; }

        public Author Author { get; set; }
    }
}
